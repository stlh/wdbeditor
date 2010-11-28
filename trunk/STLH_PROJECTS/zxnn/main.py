# -*- coding: utf-8 -*-

import os

import wsgiref.handlers

from google.appengine.ext import webapp
from google.appengine.ext.webapp import template
from google.appengine.api import users

class MainPageHandler(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if user:
      log_url = users.create_logout_url(self.request.host_url)
    else:
      log_url = users.create_login_url(self.request.uri)
    template_values = {'user': user,
                       'log_url': log_url,}
    path = os.path.join(os.path.dirname(__file__), 'template/main.html')
    self.response.out.write(template.render(path, template_values))

def main():
  application = webapp.WSGIApplication([('/', MainPageHandler),
      ('/index\.html', MainPageHandler),
    ],
    debug=False)
  wsgiref.handlers.CGIHandler().run(application)

if __name__ == '__main__':
  main()
