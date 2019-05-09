import re
import requests
import jsons
from datetime import datetime
from django.http import HttpResponse, JsonResponse
from django.shortcuts import render
from django.views.decorators.http import require_GET, require_POST
from django.views.decorators.csrf import ensure_csrf_cookie
import jsonpickle

from .helper import *
#import helper

# Create your views here.
def index(request):
    return render(request, "otapiui/home.html",
    {
            'current_time': datetime.now(),
    }
)

def system_status(request):
    client = getOtapiSdkClient()
    result = client.use_case_check_provider_state_of_health.check_current_state_of_health()
    return render(request, "otapiui/status.html",
    {
        'current_time': datetime.now(),
        'system_status' : result
    }
)

def systemStatusJson(request):
    client = getOtapiSdkClient()
    result = client.use_case_check_provider_state_of_health.check_current_state_of_health()
    data = {
        'service_status': result.service_status,
    }
    return JsonResponse(data)

def fleetLatestLocation(request):
    return render(request, "otapiui/fleetLatestLocation.html",
    {
            'current_time': datetime.now(),
            'google_maps_api_key' : GOOGLE_MAPS_API_KEY
    }
)

def fleetLatestLocationJson(request):
    client = getOtapiSdkClient()
    result = client.use_case_driver_messaging_by_geo_location \
        .get_fleet_latest_locations(page=1,count=100)
    return JsonResponse(jsonpickle.encode(result), safe=False)

def feedFollowLogEvent(request):
    return render(request, "otapiui/feedFollowLogEvent.html",
    {
            'current_time': datetime.now(),
            # 'log_events' : result
    }    
    )

def feedFollowLogEventJson(request):
    client = getOtapiSdkClient()
    token = request.GET.get('token')
    allDrivers = getAllEntities("/v1.0/drivers","all_drivers")
    allVehicles = getAllEntities("/v1.0/vehicles","all_vehicles")
    result = client.use_case_driver_route_and_directions_done.follow_fleet_log_events(token=token)
    for item in result.feed:
        driver = next((item2 for item2 in allDrivers if item2["id"] == item.driver_id), None)
        item.driver_name = ""
        if (driver != None):
            item.driver_name = driver["username"]
        vehicle = next((item2 for item2 in allVehicles if item2["id"] == item.vehicle_id), None)
        item.license_plate = ""
        if (vehicle != None):
            item.license_plate = vehicle["licensePlate"]
        item.event_type_description = getTranslation(item.event_type)
        item.origin_description = getTranslation(item.origin)
        item.state_description = getTranslation(item.state)
    return JsonResponse(jsonpickle.encode(result), safe=False)


@require_GET
@ensure_csrf_cookie
def exportData(request):
    return render(request, "otapiui/exportData.html",
    {
            'current_time': datetime.now(),
    }
)

@require_POST
@ensure_csrf_cookie
def acceptExportRequest(request):
    return render(request, "otapiui/exportData.html",
    {
            'current_time': datetime.now(),
    }
)

