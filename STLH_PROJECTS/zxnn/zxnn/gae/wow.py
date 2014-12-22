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

class WOWMainPageHandler(webapp2.RequestHandler):
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
            'module': 'wow',
        }
        template = env.get_template('template/wow/main.html')
        self.response.out.write(template.render(template_values))

class GetRealmsByRegionHandler(webapp2.RequestHandler):
    def get(self):
        region = self.request.get('region')
        url = 'http://' + region + '/api/wow/realm/status';
        try:
            logging.info(url)
            result = urllib2.urlopen(url)
            a = result.read()
            self.response.out.write(a)
        except urllib2.URLError, e:
            return 'fail'

class GetCharacterItemsHandler(webapp2.RequestHandler):
    def get(self):
        region = self.request.get('region')
        realm = self.request.get('realm')
        character = self.request.get('character')
        #api/wow/character/test-realm/Peratryn?fields=items
        url = 'http://' + region + '/api/wow/character/' + realm + '/' + character +  '?fields=items';
        try:
            logging.info(url)
            result = urllib2.urlopen(url)
            a = result.read()
            self.response.out.write(a)
        except urllib2.URLError, e:
            return 'fail'

class GetItemHandler(webapp2.RequestHandler):
    def get(self):
        region = self.request.get('region')
        itemId = self.request.get('itemId')
        url = 'http://' + region + '/api/wow/item/' + itemId
        try:
            logging.info(url)
            result = urllib2.urlopen(url)
            a = result.read()
            self.response.out.write(a)
        except urllib2.URLError, e:
            return 'fail'

app = webapp2.WSGIApplication([
    ('/wow/main.html', WOWMainPageHandler),
    ('/wow/get_realms', GetRealmsByRegionHandler),
    ('/wow/get_character_items', GetCharacterItemsHandler),
    ('/wow/get_item', GetItemHandler),
    ],
    debug=True)
