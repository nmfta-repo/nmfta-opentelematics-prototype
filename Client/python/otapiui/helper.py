#OTAPI SDK
from otapisdk.otapisdk_client import OtapisdkClient

API_VERSION = 1
API_HOST = "{api_host}"
API_USER_NAME = "{api_user_name}"
API_PASSWORD = "{api_password}"
GOOGLE_MAPS_API_KEY = "{google_maps_key}"

def getOtapiSdkClient():
    basic_auth_user_name = API_USER_NAME
    basic_auth_password = API_PASSWORD
    client = OtapisdkClient(basic_auth_user_name, basic_auth_password)
    client.config.default_host = API_HOST
    return client
