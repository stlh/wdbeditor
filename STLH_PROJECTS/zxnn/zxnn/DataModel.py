from datetime import datetime
import time

from google.appengine.ext import db
from google.appengine.api import users
from google.appengine.ext.blobstore import blobstore

class ZxUser(db.Model):
    guser = db.UserProperty()
    nickname = db.StringProperty()
    avatar = blobstore.BlobReferenceProperty()
    join_date = db.DateProperty(auto_now_add=True)

class iLogItem(db.Model):
    user = db.UserProperty()
    nickname = db.StringProperty()
    text = db.StringProperty()
    add_datetime = db.DateTimeProperty(auto_now_add=True)