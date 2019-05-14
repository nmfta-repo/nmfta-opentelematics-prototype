# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""

import logging
from opentelematicsapi.api_helper import APIHelper
from opentelematicsapi.configuration import Configuration
from opentelematicsapi.controllers.base_controller import BaseController
from opentelematicsapi.http.auth.basic_auth import BasicAuth
from opentelematicsapi.models.vehicle_location_time_history import VehicleLocationTimeHistory
from opentelematicsapi.exceptions.api_exception import APIException

class UseCaseDriverMessagingByGeoLocationController(BaseController):

    """A Controller to access Endpoints in the opentelematicsapi API."""

    def __init__(self, client=None, call_back=None):
        super(UseCaseDriverMessagingByGeoLocationController, self).__init__(client, call_back)
        self.logger = logging.getLogger(__name__)

    def get_fleet_latest_locations(self,
                                   page=None,
                                   count=None):
        """Does a GET request to /v1.0/fleet/locations/latest.

        Clients can retrieve the (coarse) vehicle locations (of all vehicles)
        over a given time period.
        **Access Controls**
        |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver
        Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
        |-------|-------------|--------------|------------|-------------|------
        ---------|------------|------------|------------|
        |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       |
        ALLOW         | **DENY**   | **DENY**   | ALLOW      |

        Args:
            page (float, optional): the page to select for paginated response
            count (float, optional): the number of items to return

        Returns:
            VehicleLocationTimeHistory: Response from the API. 

        Raises:
            APIException: When an error occurs while fetching the data from
                the remote API. This exception includes the HTTP Response
                code, an error message, and the HTTP body that was received in
                the request.

        """
        try:
            self.logger.info('get_fleet_latest_locations called.')
    
            # Prepare query URL
            self.logger.info('Preparing query URL for get_fleet_latest_locations.')
            _url_path = '/v1.0/fleet/locations/latest'
            _query_builder = Configuration.get_base_uri()
            _query_builder += _url_path
            _query_parameters = {
                'page': page,
                'count': count
            }
            _query_builder = APIHelper.append_url_with_query_parameters(_query_builder,
                _query_parameters, Configuration.array_serialization)
            _query_url = APIHelper.clean_url(_query_builder)
    
            # Prepare headers
            self.logger.info('Preparing headers for get_fleet_latest_locations.')
            _headers = {
                'accept': 'application/json'
            }
    
            # Prepare and execute request
            self.logger.info('Preparing and executing request for get_fleet_latest_locations.')
            _request = self.http_client.get(_query_url, headers=_headers)
            BasicAuth.apply(_request)
            _context = self.execute_request(_request, name = 'get_fleet_latest_locations')

            # Endpoint and global error handling using HTTP status codes.
            self.logger.info('Validating response for get_fleet_latest_locations.')
            if _context.response.status_code == 400:
                raise APIException('Error: page or count parameters invalid', _context)
            elif _context.response.status_code == 401:
                raise APIException('', _context)
            elif _context.response.status_code == 429:
                raise APIException('', _context)
            self.validate_response(_context)
    
            # Return appropriate type
            return APIHelper.json_deserialize(_context.response.raw_body, VehicleLocationTimeHistory.from_dictionary)

        except Exception as e:
            self.logger.error(e, exc_info = True)
            raise

    def send_message_to_a_vehicle(self,
                                  vehicle_id,
                                  body):
        """Does a POST request to /v1.0/vehicles/{vehicleId}/message.

        **Access Controls**
        |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver
        Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
        |-------|-------------|--------------|------------|-------------|------
        ---------|------------|------------|------------|
        |Access:| **DENY**    | **DENY**     | **DENY**   | **DENY**    |
        ALLOW         | **DENY**   | **DENY**   | ALLOW      |

        Args:
            vehicle_id (string): The vehicle id to send the message to
            body (ExternallySourcedVehicleDisplayMessages): TODO: type
                description here. Example: 

        Returns:
            void: Response from the API. 

        Raises:
            APIException: When an error occurs while fetching the data from
                the remote API. This exception includes the HTTP Response
                code, an error message, and the HTTP body that was received in
                the request.

        """
        try:
            self.logger.info('send_message_to_a_vehicle called.')
    
            # Prepare query URL
            self.logger.info('Preparing query URL for send_message_to_a_vehicle.')
            _url_path = '/v1.0/vehicles/{vehicleId}/message'
            _url_path = APIHelper.append_url_with_template_parameters(_url_path, { 
                'vehicleId': vehicle_id
            })
            _query_builder = Configuration.get_base_uri()
            _query_builder += _url_path
            _query_url = APIHelper.clean_url(_query_builder)
    
            # Prepare headers
            self.logger.info('Preparing headers for send_message_to_a_vehicle.')
            _headers = {
                'content-type': 'application/json; charset=utf-8'
            }
    
            # Prepare and execute request
            self.logger.info('Preparing and executing request for send_message_to_a_vehicle.')
            _request = self.http_client.post(_query_url, headers=_headers, parameters=APIHelper.json_serialize(body))
            BasicAuth.apply(_request)
            _context = self.execute_request(_request, name = 'send_message_to_a_vehicle')

            # Endpoint and global error handling using HTTP status codes.
            self.logger.info('Validating response for send_message_to_a_vehicle.')
            if _context.response.status_code == 401:
                raise APIException('', _context)
            elif _context.response.status_code == 404:
                raise APIException('Error: vehicleId not found', _context)
            elif _context.response.status_code == 429:
                raise APIException('', _context)
            self.validate_response(_context)

        except Exception as e:
            self.logger.error(e, exc_info = True)
            raise
