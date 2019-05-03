import re
import requests
import jsons
from datetime import datetime
from django.http import HttpResponse, JsonResponse
from django.shortcuts import render
import jsonpickle

import helper

# Create your views here.
def index(request):
    return render(request, "otapiui/home.html",
    {
            'current_time': datetime.now(),
    }
)

def system_status(request):
    client = helper.getOtapiSdkClient()
    result = client.use_case_check_provider_s_state_of_health.check_current_state_of_health(1)
    return render(request, "otapiui/status.html",
    {
            'current_time': datetime.now(),
            'system_status' : result
    }
)

def fleetLocations(request):
    return render(request, "otapiui/fleetLocations.html",
    {
            'current_time': datetime.now(),
    }
)

def fleetLocationsJson(request):
    client = helper.getOtapiSdkClient()
    result = client.use_case_driver_messaging_by_geo_location \
        .retrieve_all_latest_vehicle_location_time_history_objects(version=helper.API_VERSION, page=1,count=100)
    return JsonResponse(jsonpickle.encode(result), safe=False)