import re
import requests
import jsons
from datetime import date, datetime, timedelta
from django.conf import settings
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
            'google_maps_api_key' : settings.GOOGLE_MAPS_API_KEY
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

def feedFollowFaultEvent(request):
    return render(request, "otapiui/feedFollowFaultEvent.html",
    {
            'current_time': datetime.now()
    }    
    )

def feedFollowFaultEventJson(request):
    client = getOtapiSdkClient()
    token = request.GET.get('token')
    allVehicles = getAllEntities("/v1.0/vehicles","all_vehicles")
    result = client.use_case_in_field_maintenance_repair.follow_fleet_fault_code_events(token=token)
    feedResult = {"token" : result.token, "feed" : []}
    for item in result.feed:
        foundFaultCode = getFaultEventCode(item.source_address, item.suspect_parameter_number, 
            item.failure_mode_identifier, item.occurences)
        if (foundFaultCode == None):
            continue
        vehicle = next((item2 for item2 in allVehicles if item2["id"] == item.vehicle_id), None)
        item.license_plate = ""
        if (vehicle != None):
            item.license_plate = vehicle["licensePlate"]
        item.sa_description = getEventTranslation("SA",item.source_address)
        item.spn_description = getEventTranslation("SPN",item.suspect_parameter_number)
        item.fmi_description = getEventTranslation("FMI",item.failure_mode_identifier)
        item.min_occurrences = foundFaultCode["Min_Occurrences"]

        feedResult["feed"].append(item)
    return JsonResponse(jsonpickle.encode(feedResult), safe=False)


@require_GET
def exportData(request):
    client = getOtapiSdkClient()
    full_export = []
    vehicle_export = []
    today = date.today()
    dates = [today + timedelta(days=-i) for i in range(1, 6)]
    for dayOf in dates:
        try:
            export = client.use_case_data_export.test_if_complete_export_ready(dayOf)
            if (export != None):
                full_export.append({"dayOf" : dayOf, "location" : export.location})
            export = client.use_case_data_export.test_if_vehicle_only_export_ready(dayOf)
            if (export != None):
                vehicle_export.append({"dayOf" : dayOf, "location" : export.location})
        except :
            print("error occurred")
    return render(request, "otapiui/exportData.html",
    {
        'current_time': datetime.now(),
        'full_export' : full_export,
        'vehicle_export' : vehicle_export
    
    }
)

