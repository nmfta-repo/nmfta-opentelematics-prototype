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


class UseCaseDataExportControllerTests(ControllerTestBase):

    @classmethod
    def setUpClass(cls):
        super(UseCaseDataExportControllerTests, cls).setUpClass()
        cls.controller = cls.api_client.use_case_data_export

    # If the file is ready the response will include a URL where the complete file can be fetched; if the file is not yet
    #ready then a `202` return code will be returned.
    #
    #**Access Controls**
    #
    #|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
    #|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
    #|Access:| **DENY**    | **DENY**     | ALLOW      | **DENY**    | **DENY**      | **DENY**   | **DENY**   | ALLOW      |
    def test_test_if_complete_export_ready_1(self):
        # Parameters for the API call
        day_of = '2019-04-05T02:04:16Z'

        # Perform the API call through the SDK function
        result = self.controller.test_if_complete_export_ready(day_of)

        # Test response code
        self.assertEquals(self.response_catcher.response.status_code, 200)

        # Test headers
        expected_headers = {}
        expected_headers['content-type'] = None

        self.assertTrue(TestHelper.match_headers(expected_headers, self.response_catcher.response.headers))

        
        # Test whether the captured response is as we expected
        self.assertIsNotNone(result)
        self.assertEqual('{  "location": "https://api.provider.com/v1.0/export/allrecords/files/d8603741fd48a71cbf8546b04c9bc9f8"}', self.response_catcher.response.raw_body)


    # If the file is ready the response will include a URL where the complete file can be fetched; if the file is not yet
    #ready then a `202` return code will be returned.
    #
    #**Access Controls**
    #
    #|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
    #|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
    #|Access:| ALLOW       | **DENY**     | **DENY**   | **DENY**    | ALLOW         | **DENY**   | **DENY**   | ALLOW      |
    def test_test_if_vehicle_only_export_ready_1(self):
        # Parameters for the API call
        day_of = '2019-04-05T02:04:16Z'

        # Perform the API call through the SDK function
        result = self.controller.test_if_vehicle_only_export_ready(day_of)

        # Test response code
        self.assertEquals(self.response_catcher.response.status_code, 200)

        # Test headers
        expected_headers = {}
        expected_headers['content-type'] = None

        self.assertTrue(TestHelper.match_headers(expected_headers, self.response_catcher.response.headers))

        
        # Test whether the captured response is as we expected
        self.assertIsNotNone(result)
        self.assertEqual('{  "location": "https://api.provider.com/v1.0/export/vehiclerecords/files/d8603741fd48a71cbf8546b04c9bc9f8"}', self.response_catcher.response.raw_body)

