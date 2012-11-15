# coding: utf-8

import logging
import os
from datetime import datetime
import time

from google.appengine.api import users
from google.appengine.ext import db

import webapp2

import jinja2

from zxnn.DataModel import Item
from zxnn.DataModel import ZxUser

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class ls_item(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        todo_items = Item.gql("WHERE user = :u AND status = 1 ORDER BY add_datetime", u = guser)
        done_items = Item.gql("WHERE user = :u AND status = 2 ORDER BY done_datetime DESC", u = guser)[0:5]
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        template_values = {
            'user': user,
            'log_url': log_url,
            'todo_items': todo_items,
            'done_items': done_items,
            'module': 'todone',
        }
        template = env.get_template('template/todone/todone.html')
        self.response.out.write(template.render(template_values))

class add_item(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if not guser:
            self.redirect(users.create_login_url(self.request.uri))
            return
        
        item_text = self.request.GET['item_text']
        new_item = Item(user=guser, text=item_text)
        new_item.put()
        self.response.out.write(new_item.text);

class delete_item(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if not guser:
            self.redirect(users.create_login_url(self.request.uri))
            return
        
        item_id = self.request.GET['item_id']
        item = Item.get(item_id)
        if item.user == guser:
            #item.delete()
            item.is_deleted = True
            item.put()
            self.response.out.write(item.project.key())
        else:
            self.response.out.write('x')

class get_item_list(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if not guser:
            self.redirect(users.create_login_url(self.request.uri))
            return
        
        project_id = self.request.GET['item_id']
        parent_item = Item.get(parent_item_id)
        
        if p.user == guser:
            template = env.get_template('template/todone/item_list.html')
            template_values = {'p': p}
            self.response.out.write(template.render(template_values))
        else:
            self.response.out.write('x')

class mask_as_done(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if not guser:
            self.redirect(users.create_login_url(self.request.uri))
            return
        
        item_key = self.request.GET['item_key']
        item = Item.get(item_key)
        if item.user == guser:
            item.status = 2
            item.done_datetime = datetime.now()
            item.put()

class mask_as_todo(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if not guser:
            self.redirect(users.create_login_url(self.request.uri))
            return
        
        item_key = self.request.GET['item_key']
        item = Item.get(item_key)
        if item.user == guser:
            item.status = 1
            item.done_datetime = None
            item.put()

app = webapp2.WSGIApplication([
    ('/todone/', ls_item),
    ('/todone/add_item', add_item),
    ('/todone/delete_item', delete_item),
    ('/todone/get_item_list', get_item_list),
    ('/todone/mask_as_done', mask_as_done),
    ('/todone/mask_as_todo', mask_as_todo),
    ],
    debug=True)
