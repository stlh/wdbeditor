# -*- coding: utf-8 -*-

import os
import logging
import urllib2

from google.appengine.api import users
from google.appengine.api import urlfetch
from google.appengine.ext import db

import webapp2

import jinja2

from zxnn.gae.models import ZxUser

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class ToolsMainPageHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            log_url = users.create_login_url(self.request.uri)
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        if (guser is not None) and (user is None):
            user = ZxUser(guser = guser, nickname = guser.nickname())
        template_values = {
            'user': user,
            'log_url': log_url,
            'module': 'tools',
        }
        template = env.get_template('template/tools/main.html')
        self.response.out.write(template.render(template_values))

class PasswordGeneratorMainPageHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            log_url = users.create_login_url(self.request.uri)
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        if (guser is not None) and (user is None):
            user = ZxUser(guser = guser, nickname = guser.nickname())
        template_values = {
            'user': user,
            'log_url': log_url,
            'module': 'tools',
        }
        template = env.get_template('template/tools/password_generator.html')
        self.response.out.write(template.render(template_values))

app = webapp2.WSGIApplication([
    ('/tools/main.html', ToolsMainPageHandler),
    ('/tools/password_generator.html', PasswordGeneratorMainPageHandler),
    ],
    debug=True)
