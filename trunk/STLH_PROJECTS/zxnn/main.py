# -*- coding: utf-8 -*-

import os

from google.appengine.api import users
from google.appengine.ext import db

import webapp2

import jinja2

from ilog import iLogItem

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class MainPageHandler(webapp2.RequestHandler):
    def get(self):
        user = users.get_current_user()
        nickname = None
        if user:
            log_url = users.create_logout_url(self.request.host_url)
            nickname = user.nickname()
        else:
            log_url = users.create_login_url(self.request.uri)
        items = iLogItem.gql("ORDER BY add_datetime DESC").fetch(5)
        template_values = {
           'user': user,
           'log_url': log_url,
           'nickname': nickname,
           'items': items,
        }
        template = env.get_template('template/main.html')
        self.response.out.write(template.render(template_values))

app = webapp2.WSGIApplication([('/', MainPageHandler),
                               ('/index\.html', MainPageHandler),
                              ],
                              debug=True)
