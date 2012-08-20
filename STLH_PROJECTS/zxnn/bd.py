# coding: utf-8
# web dashboard

import os
import logging

import webapp2

import jinja2

from google.appengine.ext import db
from google.appengine.api import users

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class ZxUser(db.Model):
    guser = db.UserProperty()
    nickname = db.StringProperty()
    avatar = db.BlobProperty()
    join_date = db.DateProperty(auto_now_add=True)

class DashboardMainHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        web_user_gql = ZxUser.gql("WHERE guser = :u", u = guser)
        user = None
        nickname = guser.nickname()
        if web_user_gql.count() == 1:
            user = web_user_gql.get()
            nickname = user.nickname
        template_values = {'user': user,
                           'nickname': nickname,
                           'log_url': log_url,
                          }
        template = env.get_template('template/bd/dashboard.html')
        self.response.out.write(template.render(template_values))

class BindIdHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        web_user_gql = ZxUser.gql("WHERE guser = :u", u = guser)
        user = None
        nickname = guser.nickname
        if web_user_gql.count() == 1:
            user = web_user_gql.get()
            nickname = user.nickname
        template_values = {'user': user,
                           'nickname': nickname,
                           'log_url': log_url,
                           'error_message': None,
                          }
        template = env.get_template('template/bd/bind_id.html')
        self.response.out.write(template.render(template_values))
    def post(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        web_user_gql = ZxUser.gql("WHERE guser = :u", u = guser)
        user = None
        nickname = guser.nickname
        if web_user_gql.count() == 1:
            user = web_user_gql.get()
            nickname = user.nickname
        if web_user_gql.count() > 0:
            template_values = {'user': user,
                               'nickname': nickname,
                               'log_url': log_url,
                               'error_message': 'ID already exists. please choose another one.',
                              }
        else:
            new_web_user_id = self.request.get('txtId')
            new_zx_user = ZxUser(guser = guser, nickname = new_web_user_id)
            new_zx_user.put()
            self.redirect('/dashboard/main.html')

class AvatarHandler(webapp2.RequestHandler):
    def get(self):
        user = users.get_current_user()
        if user:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        template_values = {'user': user,
          'log_url': log_url,
          }
        path = os.path.join(os.path.dirname(__file__), 'template/bd/avatar.html')
        self.response.out.write(template.render(path, template_values))
    def post(self):
      avatar_img = self.request.get('avatar_img')
      u = User()
      u.avatar = db.Blob(u)
      u.put();
      user = users.get_current_user()
      if user:
          log_url = users.create_logout_url(self.request.host_url)
      else:
          self.redirect(users.create_login_url(self.request.uri))
          return
      template_values = {'user': user,
        'log_url': log_url,
        }
      path = os.path.join(os.path.dirname(__file__), 'template/bd/avatar.html')
      self.response.out.write(template.render(path, template_values))

class AvatarImageHander(webapp2.RequestHandler):
    def get(self):
        self.response.out.write('get user id')
        #user = db.get(self.request.get('user_id')
        #if user.avatar:
        #  self.response.headers['Content-Type'] = 'image/png'
        #  self.response.out.write(user.avatar)
        #else:
        #  self.response.out.write('no image found')

app = webapp2.WSGIApplication([('/dashboard/main.html', DashboardMainHandler),
                               ('/dashboard/bind_id.html', BindIdHandler),
                               ('/dashboard/avatar.html', AvatarHandler),
                               ('/public/\w+/avatar', AvatarImageHander),
                              ],
                              debug=True)
