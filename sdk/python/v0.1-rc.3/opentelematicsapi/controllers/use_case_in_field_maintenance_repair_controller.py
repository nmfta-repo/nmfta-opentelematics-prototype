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
from opentelematicsapi.models.follow_fleet_fault_code_events_response import FollowFleetFaultCodeEventsResponse
from opentelematicsapi.models.vehicle_location_time_history import VehicleLocationTimeHistory
from opentelematicsapi.exceptions.api_exception import APIException

class UseCaseInFieldMaintenanceRepairController(BaseController):

    """A Controller to access Endpoints in the opentelematicsapi API."""

    def __init__(self, client=None, call_back=None):
        super(UseCaseInFieldMaintenanceRepairController, self).__init__(client, call_back)
        self.logger = logging.getLogger(__name__)

    def follow_fleet_fault_code_events(self,
                                       token=None):
        """Does a GET request to /v1.0/fleet/faults/feed.

        Clients can follow a feed of Vehicle Fault Code Events as they are
        added to the TSP system; following is accomplished
        bia polling an endpoint and providing a 'token' which evolves the
        window of new entries with each query in the polling.
        **Access Controls**
        |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver
        Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
        |-------|-------------|--------------|------------|-------------|------
        ---------|------------|------------|------------|
        |Access:| **DENY**    | ALLOW        | **DENY**   | ALLOW       |
        **DENY**      | **DENY**   | **DENY**   | ALLOW      |

        Args:
            token (string, optional): a since-token, pass-in the token
                previously returned to 'follow' new Log Events; pass in a
                `null` or omit this token to start with a new token set to
                'now'.

        Returns:
            FollowFleetFaultCodeEventsResponse: Response from the API. 

        Raises:
            APIException: When an error occurs while fetching the data from
                the remote API. This exception includes the HTTP Response
                code, an error message, and the HTTP body that was received in
                the request.

        """
        try:
            self.logger.info('follow_fleet_fault_code_events called.')
    
            # Prepare query URL
            self.logger.info('Preparing query URL for follow_fleet_fault_code_events.')
            _url_path = '/v1.0/fleet/faults/feed'
            _query_builder = Configuration.get_base_uri()
            _query_builder += _url_path
            _query_parameters = {
                'token': token
            }
            _query_builder = APIHelper.append_url_with_query_parameters(_query_builder,
                _query_parameters, Configuration.array_serialization)
            _query_url = APIHelper.clean_url(_query_builder)
    
            # Prepare headers
            self.logger.info('Preparing headers for follow_fleet_fault_code_events.')
            _headers = {
                'accept': 'application/json'
            }
    
            # Prepare and execute request
            self.logger.info('Preparing and executing request for follow_fleet_fault_code_events.')
            _request = self.http_client.get(_query_url, headers=_headers)
            BasicAuth.apply(_request)
            _context = self.execute_request(_request, name = 'follow_fleet_fault_code_events')

            # Endpoint and global error handling using HTTP status codes.
            self.logger.info('Validating response for follow_fleet_fault_code_events.')
            if _context.response.status_code == 400:
                raise APIException('Error: token parameters invalid', _context)
            elif _context.response.status_code == 401:
                raise APIException('', _context)
            elif _context.response.status_code == 413:
                raise APIException('', _context)
            elif _context.response.status_code == 429:
                raise APIException('', _context)
            self.validate_response(_context)
    
            # Return appropriate type
            return APIHelper.json_deserialize(_context.response.raw_body, FollowFleetFaultCodeEventsResponse.from_dictionary)

        except Exception as e:
            self.logger.error(e, exc_info = True)
            raise

    def get_vehicle_location_history(self,
                                     vehicle_id,
                                     start_time,
                                     stop_time):
        """Does a GET request to /v1.0/vehicles/{vehicleId}/locations/.

        **Access Controls**
        |Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver
        Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
        |-------|-------------|--------------|------------|-------------|------
        ---------|------------|------------|------------|
        |Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       |
        **DENY**      | **DENY**   | **DENY**   | ALLOW      |

        Args:
            vehicle_id (string): The vehicle id to associate this route to
            start_time (string): the start-date of the search
            stop_time (string): the stop-date of the search

        Returns:
            VehicleLocationTimeHistory: Response from the API. 

        Raises:
            APIException: When an error occurs while fetching the data from
                the remote API. This exception includes the HTTP Response
                code, an error message, and the HTTP body that was received in
                the request.

        """
        try:
            self.logger.info('get_vehicle_location_history called.')
    
            # Prepare query URL
            self.logger.info('Preparing query URL for get_vehicle_location_history.')
            _url_path = '/v1.0/vehicles/{vehicleId}/locations/'
            _url_path = APIHelper.append_url_with_template_parameters(_url_path, { 
                'vehicleId': vehicle_id
            })
            _query_builder = Configuration.get_base_uri()
            _query_builder += _url_path
            _query_parameters = {
                'startTime': start_time,
                'stopTime': stop_time
            }
            _query_builder = APIHelper.append_url_with_query_parameters(_query_builder,
                _query_parameters, Configuration.array_serialization)
            _query_url = APIHelper.clean_url(_query_builder)
    
            # Prepare headers
            self.logger.info('Preparing headers for get_vehicle_location_history.')
            _headers = {
                'accept': 'application/json'
            }
    
            # Prepare and execute request
            self.logger.info('Preparing and executing request for get_vehicle_location_history.')
            _request = self.http_client.get(_query_url, headers=_headers)
            BasicAuth.apply(_request)
            _context = self.execute_request(_request, name = 'get_vehicle_location_history')

            # Endpoint and global error handling using HTTP status codes.
            self.logger.info('Validating response for get_vehicle_location_history.')
            if _context.response.status_code == 400:
                raise APIException('Error: startTime or stopTime parameters invalid', _context)
            elif _context.response.status_code == 401:
                raise APIException('', _context)
            elif _context.response.status_code == 404:
                raise APIException('Error: vehicleId Not Found', _context)
            elif _context.response.status_code == 413:
                raise APIException('', _context)
            elif _context.response.status_code == 429:
                raise APIException('', _context)
            self.validate_response(_context)
    
            # Return appropriate type
            return APIHelper.json_deserialize(_context.response.raw_body, VehicleLocationTimeHistory.from_dictionary)

        except Exception as e:
            self.logger.error(e, exc_info = True)
            raise
