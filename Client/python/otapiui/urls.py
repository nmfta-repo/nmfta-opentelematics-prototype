from django.urls import path
from otapiui import views

urlpatterns = [
    path("", views.index, name="home"),
    path("xpo", views.xpo, name="xpo"),
    path("status", views.system_status, name="system_status"),
]