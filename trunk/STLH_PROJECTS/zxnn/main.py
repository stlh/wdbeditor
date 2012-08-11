# -*- coding: utf-8 -*-

import os

from google.appengine.api import users

import webapp2

import jinja2

env = jinja2.Environment(loader=jinja2.FileSystemLoader(os.path.dirname(__file__)))

class MainPageHandler(webapp2.RequestHandler):
    def get(self):
        user = users.get_current_user()
        if user:
            log_url = users.create_logout_url(self.request.host_url)
        else:
             log_url = users.create_login_url(self.request.uri)
        template_values = {
           'user': user,
           'log_url': log_url,
        }
        template = env.get_template('template/main.html')
        self.response.out.write(template.render(template_values))

app = webapp2.WSGIApplication([('/', MainPageHandler),
                               ('/index\.html', MainPageHandler),
                              ],
                              debug=True)
