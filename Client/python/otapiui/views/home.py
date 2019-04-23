import re
from datetime import datetime
from django.http import HttpResponse
from django.shortcuts import render

from .library.models.Vehicle import Vehicle

# Create your views here.
def index(request):
    sefl = Vehicle("SEFL", "1236789")
    return render(request, "otapiui/home.html",
    {
            'current_time': datetime.now(),
            'Vehicle' : sefl
    }
)

def xpo(request):
    xpo = Vehicle("XPO", "4567")
    return render(request, "otapiui/home.html",
    {
            'current_time': datetime.now(),
            'Vehicle' : xpo
    }
)
