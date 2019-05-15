# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""

import jsonpickle
import dateutil.parser
from .controller_test_base import ControllerTestBase
from ..test_helper import TestHelper
from opentelematicsapi.api_helper import APIHelper


class UseCaseDriverMessagingByGeoLocationControllerTests(ControllerTestBase):

    @classmethod
    def setUpClass(cls):
        super(UseCaseDriverMessagingByGeoLocationControllerTests, cls).setUpClass()
        cls.controller = cls.api_client.use_case_driver_messaging_by_geo_location

    # Clients can retrieve the (coarse) vehicle locations (of all vehicles) over a given time period.
    #
    #**Access Controls**
    #
    #|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
    #|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
    #|Access:| ALLOW       | ALLOW        | ALLOW      | ALLOW       | ALLOW         | **DENY**   | **DENY**   | ALLOW      |
    def test_get_fleet_latest_locations_1(self):
        # Parameters for the API call
        page = None
        count = None

        # Perform the API call through the SDK function
        result = self.controller.get_fleet_latest_locations(page, count)

        # Test response code
        self.assertEquals(self.response_catcher.response.status_code, 200)

        # Test headers
        expected_headers = {}
        expected_headers['content-type'] = None
        expected_headers['x-total-count'] = None

        self.assertTrue(TestHelper.match_headers(expected_headers, self.response_catcher.response.headers))

        
        # Test whether the captured response is as we expected
        self.assertIsNotNone(result)
        self.assertEqual('{  "data": [    {      "id": "C4CA4238A0B923820DCC509A6F75849B",      "providerId": "api.provider.com",      "serverTime": "2019-04-05T02:04:16Z",      "vehicleId": "21232F297A57A5A743894A0E4A801FC3",      "dateTime": "2019-04-05T02:04:16Z",      "location": "37.4224764 -122.0842499"    }  ],  "timeResolution": "TIMERESOLUTION_MAX"}', self.response_catcher.response.raw_body)


