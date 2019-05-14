from django.core.cache import cache
from django.conf import settings
import os
import json
import jsonpickle
import requests
import base64

#OTAPI SDK
from opentelematicsapi.opentelematicsapi_client import OpentelematicsapiClient
from opentelematicsapi.configuration import Configuration

API_SCHEME = "http://"
API_HOST = "{api_host}"
API_USER_NAME = "{api_user_name}"
API_PASSWORD = "{api_password}"
GOOGLE_MAPS_API_KEY = "{google_maps_key}"

# Swap to Use HTTPS
Configuration.environments[Configuration.environment][Configuration.Server.DEFAULT] = API_SCHEME + "{defaultHost}"

def getOtapiSdkClient():
    basic_auth_user_name = API_USER_NAME
    basic_auth_password = API_PASSWORD
    client = OpentelematicsapiClient(basic_auth_user_name, basic_auth_password)
    client.config.default_host = API_HOST
    return client

def getTranslationTable():
    i18n = cache.get("i18n")
    if i18n != None:
        return i18n
    client = getOtapiSdkClient()
    i18n = client.localization.get_a_translation_table(accept_language="en-US")
    cache.set("i18n",i18n)
    return i18n

def getTranslation(input):
    i18n = getTranslationTable()
    translation = next((item for item in i18n.data if item.msgid == input), None)
    if (translation == None):
        return  input
    return translation.msgstr

def getFaultCodeMap():
    faultCodeMap = cache.get("faultCodeMap")
    if (faultCodeMap != None):
        return faultCodeMap
    with open(os.path.join(settings.BASE_DIR, 'app/FaultCode.json')) as faultCode_file:
        faultCodeMap = json.load(faultCode_file)
        cache.set("faultCodeMap", faultCodeMap)
        return faultCodeMap

def getFaultEventCode(sa, spn, fmi, occurrences):
    faultCodeMap = getFaultCodeMap()
    foundFaultCode = next((item for item in faultCodeMap["fault_code"]
        if item["SA"] == sa and (item["SPN"] == -1 or item["SPN"] == spn) and \
            (item["FMI"] == -1 or item["FMI"] == fmi) and \
            (item["Min_Occurrences"] <= occurrences)) \
                , None)
    return foundFaultCode 

def getEventTranslation(map, id):
    faultCodeMap = getFaultCodeMap()
    translation = next((item for item in faultCodeMap[map] if item["id"] == id), None)
    if (translation == None):
        return  id
    return str(translation["id"]) + " - " + translation["description"]


def apply(http_request, username, password):
    username = username
    password = password
    joined = "{}:{}".format(username, password)
    encoded = base64.b64encode(str.encode(joined)).decode('iso-8859-1')
    header_value = "Basic {}".format(encoded)
    http_request.headers["Authorization"] = header_value

def getAllEntities(end_point : str, cache_id : str):
    entities = cache.get(cache_id)
    if (entities != None):
        return entities
    httpRequest = requests.session()
    httpRequest.verify = False
    apply(httpRequest, API_USER_NAME, API_PASSWORD)
    httpRequest.headers["content-type"] = "application/json"
    httpRequest.headers["charset"] = "utf-8"
    response = httpRequest.get(API_SCHEME + API_HOST + end_point, headers=httpRequest.headers, verify=False)
    entities = jsonpickle.decode(response.content)
    cache.set(cache_id, entities)
    return entities