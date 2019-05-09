from django.urls import path
from otapiui import views

urlpatterns = [
    path("", views.index, name="home"),
    path("status", views.system_status, name="system_status"),
    path("systemStatusJson", views.systemStatusJson, name="systemStatusJson"),
    path("fleetLatestLocation", views.fleetLatestLocation, name="fleet_latest_location"),
    path("fleetLatestLocationJson", views.fleetLatestLocationJson, name="fleetLatestLocationJson"),
    path("feedFollowLogEvent", views.feedFollowLogEvent, name="feed_follow_log_event"),
    path("feedFollowLogEventJson", views.feedFollowLogEventJson, name="feed_follow_log_event_json"),
    path("exportData", views.exportData, name="export_data"),
]