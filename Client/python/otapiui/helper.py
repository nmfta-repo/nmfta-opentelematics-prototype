from django.core.cache import cache
import jsonpickle
import requests
import base64

#OTAPI SDK
from opentelematicsapi.opentelematicsapi_client import OpentelematicsapiClient

API_VERSION = 1
API_HOST = "{api_host}"
API_USER_NAME = "{api_user_name}"
API_PASSWORD = "{api_password}"
GOOGLE_MAPS_API_KEY = "{google_maps_key}"

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
    response = httpRequest.get("http://" + API_HOST + end_point, headers=httpRequest.headers, verify=False)
    entities = jsonpickle.decode(response.content)
    cache.set(cache_id, entities)
    return entities