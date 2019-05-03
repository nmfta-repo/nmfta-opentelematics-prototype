#OTAPI SDK
from otapisdk.otapisdk_client import OtapisdkClient

API_VERSION = 1
API_HOST = "localhost:61386"

def getOtapiSdkClient():
    basic_auth_user_name = '{user_name}' # The username to use with basic authentication
    basic_auth_password = '{password}' # The password to use with basic authentication
    client = OtapisdkClient(basic_auth_user_name, basic_auth_password)
    client.config.default_host = "{api_host}"
    return client
