import re
import requests
import jsons
from datetime import datetime
from django.http import HttpResponse
from django.shortcuts import render

from .otapisdk.models.status_model import status_model
# Create your views here.
def system_status(request):
    #http = HTTPSession()
    r = requests.get('https://localhost:44359/status',auth=('krishna', 'Test@12345'), verify=False)
    result = r.json()
    system_status = jsons.load(result, status_model)
    return render(request, "otapiui/status.html",
    {
            'current_time': datetime.now(),
            'system_status' : system_status
    }
)