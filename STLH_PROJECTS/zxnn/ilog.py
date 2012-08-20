# coding: utf-8

import logging
import os
from datetime import datetime
import time

import wsgiref.handlers

from google.appengine.ext import db
from google.appengine.api import users

import webapp2

import jinja2

from bd import ZxUser

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class iLogItem(db.Model):
    user = db.UserProperty()
    text = db.StringProperty()
    add_datetime = db.DateTimeProperty(auto_now_add=True)

class ilog_say(webapp2.RequestHandler):
  def get(self):
      user = users.get_current_user()
      if not user:
          self.redirect(users.create_login_url(self.request.uri))
          return
      log_text = self.request.GET['text']
      item = iLogItem(text=log_text)
      item.put()
      self.response.out.write(item.key())
  def post(self):
      user = users.get_current_user()
      if not user:
          self.redirect(users.create_login_url(self.request.uri))
          return
      log_text = self.request.get('text')
      item = iLogItem(user=user, text=log_text)
      item.put()
      items = iLogItem.gql("WHERE user = :u ORDER BY add_datetime DESC", u=user).fetch(20)
      template_values = {'items': items,
                        }
      path = os.path.join(os.path.dirname(__file__), 'template/ilog/ls.html')
      self.response.out.write(template.render(path, template_values))

class ilog_ls(webapp2.RequestHandler):
    def get(self):
      user = users.get_current_user()
      if not user:
          #self.redirect(users.create_login_url(self.request.uri))
          self.response.out.write("(null)")
          return
      count = self.request.GET['count']
      items = iLogItem.gql("WHERE user = :u ORDER BY add_datetime DESC", u=user).fetch(count)
      template_values = {'items': items,
                        }
      path = os.path.join(os.path.dirname(__file__), 'template/ilog/ls.html')
      self.response.out.write(template.render(path, template_values))

class ilog_main(webapp2.RequestHandler):
    def get(self):
        user = users.get_current_user()
        if user:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        items = iLogItem.gql("WHERE user = :u ORDER BY add_datetime DESC", u=user).fetch(7)
        template_values = {'user': user,
                           'log_url': log_url,
                           'items': items,
                          }
        path = os.path.join(os.path.dirname(__file__), 'template/ilog/main.html')
        self.response.out.write(template.render(path, template_values))

app = webapp2.WSGIApplication([('/ilog/say/', ilog_say),
                                        ('/ilog/say$', ilog_say),
                                        ('/ilog/ls/', ilog_ls),
                                        ('/ilog/main.html', ilog_main),
                                       ],
                                       debug=True)
