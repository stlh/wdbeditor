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

class Environ(webapp.RequestHandler):
  def get(self):
    for name in os.environ.keys():
      self.response.out.write('%s - %s<br />\n' % (name, os.environ[name]))

def main():
  application = webapp.WSGIApplication([('/tester/environ', Environ),
                                       ],
                                       debug=True)
  wsgiref.handlers.CGIHandler().run(application)

if __name__ == '__main__':
  main()
