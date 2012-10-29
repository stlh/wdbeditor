from datetime import datetime
import time

from google.appengine.ext import db
from google.appengine.api import users

class ZxUser(db.Model):
    guser = db.UserProperty()
    nickname = db.StringProperty()
    avatar = db.BlobProperty()
    join_date = db.DateProperty(auto_now_add=True)

class iLogItem(db.Model):
    user = db.UserProperty()
    text = db.StringProperty()
    add_datetime = db.DateTimeProperty(auto_now_add=True)