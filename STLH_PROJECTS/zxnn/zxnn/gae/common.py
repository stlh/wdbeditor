# coding: utf-8

from google.appengine.api import users

def authorize(func):
    def inner(*args, **kwargs):
        guser = users.get_current_user()
        request = args[0]
        if not guser:
            request.redirect(users.create_login_url(request.request.uri))
        return func(*args, **kwargs)
    return inner
