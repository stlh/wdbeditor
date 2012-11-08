# coding: utf-8
# web dashboard

import os
import logging

import webapp2

import jinja2

from google.appengine.ext import db
from google.appengine.ext import blobstore
from google.appengine.ext.webapp import blobstore_handlers
from google.appengine.api import users

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

from zxnn.DataModel import ZxUser

class DashboardMainHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        if user is None:
            self.redirect('/dashboard/bind_id.html')
            return
        template_values = {'user': user,
                           'log_url': log_url,
                           'module': 'dashboard',
                          }
        template = env.get_template('template/bd/main.html')
        self.response.out.write(template.render(template_values))

class BindIdHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        if not user:
            user = ZxUser(nickname = guser.nickname())
        template_values = {'user': user,
                           'log_url': log_url,
                           'module': 'dashboard',
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
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        if user is not None:
            if (guser is not None) and (user is None):
                user = ZxUser(guser = guser, nickname = guser.nickname())
            template_values = {'user': user,
                               'log_url': log_url,
                               'error_message': 'ID already exists. please choose another one.',
                               'module': 'dashboard',
                              }
            template = env.get_template('template/bd/bind_id.html')
            self.response.out.write(template.render(template_values))
        else:
            new_web_user_id = self.request.get('txtId')
            new_zx_user = ZxUser(guser = guser, nickname = new_web_user_id)
            new_zx_user.put()
            self.redirect('/dashboard/main.html')

class AvatarHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        user = ZxUser.gql("WHERE guser = :u", u=guser).get()
        upload_url = blobstore.create_upload_url('/upload_avatar')
        template_values = {'user': user,
          'log_url': log_url,
          'upload_url': upload_url,
          'module': 'avatar',
        }
        template = env.get_template('template/bd/avatar.html')
        self.response.out.write(template.render(template_values))

class AvatarUploadHandler(blobstore_handlers.BlobstoreUploadHandler):
    def post(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            self.redirect(users.create_login_url(self.request.uri))
            return
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        upload_files = self.get_uploads('avatar_file')  # 'file' is file upload field in the form
        avatar_info = upload_files[0]
        user.avatar = avatar_info.key()
        user.put()
        self.redirect('/dashboard/avatar.html')

class AvatarDownloadHander(blobstore_handlers.BlobstoreDownloadHandler):
    def get(self):
        nickname = self.request.get("nickname")
        user = ZxUser.gql("WHERE nickname = :u", u = nickname).get()
        blob_info = blobstore.BlobInfo.get(user.avatar.key())
        self.send_blob(blob_info)

app = webapp2.WSGIApplication([('/dashboard/main.html', DashboardMainHandler),
                               ('/dashboard/bind_id.html', BindIdHandler),
                               ('/dashboard/avatar.html', AvatarHandler),
                               ('/upload_avatar', AvatarUploadHandler),
                               ('/public/avatar', AvatarDownloadHander),
                              ],
                              debug=True)
