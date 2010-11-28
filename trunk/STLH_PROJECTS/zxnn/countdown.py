# coding: utf-8

import logging
import os
from datetime import datetime
import time

import wsgiref.handlers

from google.appengine.ext import webapp
from google.appengine.ext import db
from google.appengine.ext.webapp import template
from google.appengine.api import users

class CountdownItem(db.Model):
  user = db.UserProperty()
  title = db.StringProperty()
  due_date =  db.DateTimeProperty()

class CountdownPageHandler(webapp.RequestHandler):
  def get(self):
    user = users.get_current_user()
    if user:
      log_url = users.create_logout_url(self.request.host_url)
    else:
      self.redirect(users.create_login_url(self.request.uri))
      return
    items = CountdownItem.gql("WHERE user = :u ORDER BY due_date", u=user)
    
    template_values = {'user': user,
                       'log_url': log_url,
                       'items': items
                      }
    path = os.path.join(os.path.dirname(__file__), 'template/countdown/countdown.html')
    self.response.out.write(template.render(path, template_values))

def main():
  application = webapp.WSGIApplication([('/countdown/', CountdownPageHandler),
                                       ],
                                       debug=True)
  wsgiref.handlers.CGIHandler().run(application)

if __name__ == '__main__':
  main()
