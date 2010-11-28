# coding: utf-8
# web dashboard

import logging
import os
from datetime import datetime
import time

import wsgiref.handlers

from google.appengine.ext import webapp
from google.appengine.ext import db
from google.appengine.ext.webapp import template
from google.appengine.api import users

class User(db.Model):
  guser = db.UserProperty()
  nickname = db.StringProperty()
  avatar = db.BlobProperty()
  join_date = db.DateProperty(auto_now_add=True)

class DashboardMainHandler(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if user:
      log_url = users.create_logout_url(self.request.host_url)
    else:
      self.redirect(users.create_login_url(self.request.uri))
      return
    web_user_gql = User.gql("WHERE guser = :u", u = user)
    wu = None
    nickname = user.nickname
    if web_user_gql.count() == 1:
      wu = web_user_gql.get()
      nickname = wu.nickname
    template_values = {'user': user,
                       'wu': wu,
                       'nickname': nickname,
                       'log_url': log_url,
                      }
    path = os.path.join(os.path.dirname(__file__), 'template/bd/dashboard.html')
    self.response.out.write(template.render(path, template_values))

class BindIdHandler(webapp.RequestHandler):
  def post(self):
    user = users.get_current_user()
    if user:
      log_url = users.create_logout_url(self.request.host_url)
    else:
      self.redirect(users.create_login_url(self.request.uri))
      return
    new_web_user_id = self.request.get('txtId')
    wu = User.gql('WHERE nickname = :nn', nn = new_web_user_id)
    if wu.count() > 0:
      template_values = {'user': user,
        'error_message': 'ID already exists. please choose another one.',
        'log_url': log_url,
        }
    else:
      new_web_user = User(guser = user, nickname = new_web_user_id)
      new_web_user.put()
      self.redirect('/dashboard/main.html')
class AvatarHandler(webapp.RequestHandler)
  def get(self):
    user = users.get_current_user()
    template_values = {'user': user,
      'log_url': log_url,
      }
    path = os.path.join(os.path.dirname(__file__), 'template/bd/avatar.html')
    self.response.out.write(template.render(path, template_values))

def main():
  application = webapp.WSGIApplication([('/dashboard/main.html', DashboardMainHandler),
  ('/dashboard/bind_id.html', BindIdHandler),
  ],
  debug=True)
  wsgiref.handlers.CGIHandler().run(application)

if __name__ == '__main__':
  main()
