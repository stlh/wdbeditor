# -*- coding: utf-8 -*-

import os
import logging

from google.appengine.api import users
from google.appengine.ext import db
import webapp2
import jinja2

from zxnn.gae.models import iLogItem
from zxnn.gae.models import ZxUser

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class MainPageHandler(webapp2.RequestHandler):
    def get(self):
        guser = users.get_current_user()
        if guser:
            log_url = users.create_logout_url(self.request.host_url)
        else:
            log_url = users.create_login_url(self.request.uri)
        user = ZxUser.gql("WHERE guser = :u", u = guser).get()
        items = iLogItem.gql("ORDER BY add_datetime DESC").fetch(5)
        if (guser is not None) and (user is None):
            user = ZxUser(guser = guser, nickname = guser.nickname())
        template_values = {
            'user': user,
            'log_url': log_url,
            'items': items,
        }
        template = env.get_template('template/main.html')
        self.response.out.write(template.render(template_values))

app = webapp2.WSGIApplication([
    ('/', MainPageHandler),
    ('/index\.html', MainPageHandler),
    ],
    debug=True)
