from django.urls import path
from otapiui import views

urlpatterns = [
    path("", views.index, name="home"),
    path("status", views.system_status, name="system_status"),
    path("fleetLatestLocation", views.fleetLatestLocation, name="fleet_latest_location"),
    path("fleetLatestLocationJson", views.fleetLatestLocationJson, name="fleetLatestLocationJson"),
]