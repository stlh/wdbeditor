# coding: utf-8

import logging
import os
from datetime import datetime
import time

import wsgiref.handlers

from google.appengine.ext import webapp
from google.appengine.ext import db
from google.appengine.ext.webapp import template
from google.appengine.api import users

class Item(db.Model):
  user = db.UserProperty()
  parent_item = db.SelfReferenceProperty(collection_name='child_items')
  text = db.StringProperty()
  #status 1: todo, 2: done
  status = db.IntegerProperty(default=1)
  add_datetime = db.DateTimeProperty(auto_now_add=True)
  done_datetime = db.DateTimeProperty()
  is_deleted = db.BooleanProperty(default=False)

class item_ls(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if user:
      log_url = users.create_logout_url(self.request.host_url)
    else:
      self.redirect(users.create_login_url(self.request.uri))
      return
    todo_items = Item.gql("WHERE user = :u AND status = 1 ORDER BY add_datetime", u=user)
    done_items = Item.gql("WHERE user = :u AND status = 2 ORDER BY done_datetime DESC", u=user)[0:5]
    
    template_values = {'user': user,
                       'log_url': log_url,
                       'todo_items': todo_items,
                       'done_items': done_items
                      }
    path = os.path.join(os.path.dirname(__file__), 'template/todone/todone.html')
    self.response.out.write(template.render(path, template_values))

class add_item(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if not user:
      self.redirect(users.create_login_url(self.request.uri))
      return
    
    item_text = self.request.GET['item_text']
    new_item = Item(user=user, text=item_text)
    new_item.put()
    logging.info(new_item.key())
    self.response.out.write(new_item.text);

class delete_item(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if not user:
      self.redirect(users.create_login_url(self.request.uri))
      return
    
    item_id = self.request.GET['item_id']
    item = Item.get(item_id)
    if item.user == user:
      #item.delete()
      item.is_deleted = True
      item.put()
      self.response.out.write(item.project.key())
    else:
      self.response.out.write('x')

class get_item_list(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if not user:
      self.redirect(users.create_login_url(self.request.uri))
      return
    
    project_id = self.request.GET['item_id']
    parent_item = Item.get(parent_item_id)
    
    if p.user == user:
      path = os.path.join(os.path.dirname(__file__), 'template/todone/item_list.html')
      template_values = {'p': p}
      self.response.out.write(template.render(path, template_values))
    else:
      self.response.out.write('x')

class mask_as_done(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if not user:
      self.redirect(users.create_login_url(self.request.uri))
      return
    
    item_key = self.request.GET['item_key']
    item = Item.get(item_key)
    if item.user == user:
      item.status = 2
      item.done_datetime = datetime.now()
      item.put()

class mask_as_todo(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if not user:
      self.redirect(users.create_login_url(self.request.uri))
      return
    
    item_key = self.request.GET['item_key']
    item = Item.get(item_key)
    if item.user == user:
      item.status = 1
      item.done_datetime = None
      item.put()

def main():
  application = webapp.WSGIApplication([('/todone/', item_ls),
                                        ('/todone/add_item/', add_item),
                                        ('/todone/delete_item/', delete_item),
                                        ('/todone/get_item_list/', get_item_list),
                                        ('/todone/mask_as_done/', mask_as_done),
                                        ('/todone/mask_as_todo/', mask_as_todo),
                                       ],
                                       debug=True)
  wsgiref.handlers.CGIHandler().run(application)

if __name__ == '__main__':
  main()
