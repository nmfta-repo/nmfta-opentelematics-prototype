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
    result = client.use_case_check_provider_s_state_of_health.check_current_state_of_health(1)
    return render(request, "otapiui/status.html",
    {
        'current_time': datetime.now(),
        'system_status' : result
    }
)

def systemStatusJson(request):
    client = getOtapiSdkClient()
    result = client.use_case_check_provider_s_state_of_health.check_current_state_of_health(1)
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
        .retrieve_all_latest_vehicle_location_time_history_objects(version=API_VERSION, page=1,count=100)
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
