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


class UseCaseDriverAvailabilityControllerTests(ControllerTestBase):

    @classmethod
    def setUpClass(cls):
        super(UseCaseDriverAvailabilityControllerTests, cls).setUpClass()
        cls.controller = cls.api_client.use_case_driver_availability

    # Clients can request all the factors contributing to driver availability for a given driver, over a given time period.
    #
    #**Access Controls**
    #
    #|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
    #|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
    #|Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | **DENY**   | ALLOW      |
    def test_get_driver_availability_factors_1(self):
        # Parameters for the API call
        driver_id = '63A9F0EA7BB98050796B649E85481845'
        start_time = '2019-04-05T02:04:16Z'
        stop_time = '2019-04-05T02:04:16Z'

        # Perform the API call through the SDK function
        result = self.controller.get_driver_availability_factors(driver_id, start_time, stop_time)

        # Test response code
        self.assertEquals(self.response_catcher.response.status_code, 200)

        # Test headers
        expected_headers = {}
        expected_headers['content-type'] = None

        self.assertTrue(TestHelper.match_headers(expected_headers, self.response_catcher.response.headers))

        
        # Test whether the captured response is as we expected
        self.assertIsNotNone(result)
        self.assertEqual('{  "logEvents": [    {      "id": "C4CA4238A0B923820DCC509A6F75849B",      "providerId": "api.provider.com",      "serverTime": "2019-04-05T02:04:16Z",      "annotations": [        {          "providerId": "api.provider.com",          "driverId": "63A9F0EA7BB98050796B649E85481845",          "comment": "note: something noteworthy",          "dateTime": "2019-04-05T02:04:16Z"        }      ],      "coDrivers": [        "A87FF679A2F3E71D9181A67B7542122C",        "E4DA3B7FBBCE2345D7772B0674A318D5"      ],      "dateTime": "2019-04-05T02:04:16Z",      "vehicleId": "21232F297A57A5A743894A0E4A801FC3",      "driverId": "63A9F0EA7BB98050796B649E85481845",      "distanceLastValid": 117,      "editDateTime": "",      "location": {        "latitude": 37.4224764,        "longitude": -122.0842499,        "identifiedPlace": "New York",        "identifiedState": "NY",        "distanceFrom": 5000,        "directionFrom": "NNE"      },      "origin": "ORIGIN_AUTOMATIC",      "parentId": "D6AB4B1A2E51C28CB32BFE8982D42259",      "sequence": 23,      "state": "STATE_ACTIVE",      "eventType": "EVENTTYPE_DUTY_OFF",      "certificationCount": 0,      "verifyDateTime": "",      "multidayBasis": 0,      "comment": "fake Log Event for testing",      "eventDataChecksum": ""    }  ],  "vehicleFlaggedEvents": [    {      "id": "C4CA4238A0B923820DCC509A6F75849B",      "providerId": "api.provider.com",      "serverTime": "2019-04-05T02:04:16Z",      "eventStart": "2019-04-05T02:04:16Z",      "eventEnd": "2019-04-05T02:04:16Z",      "vehicleId": "21232F297A57A5A743894A0E4A801FC3",      "eventComment": "event type XXXX, (other details)",      "trigger": "FLAGGEDTYPE_ROLL_STABILITY",      "gpsSpeed": 0,      "gpsHeading": 0,      "gpsQuality": "GPSQUALITY_FINELOCK",      "ecmSpeed": 0,      "engineRPM": 0,      "accelerationPercent": 0,      "seatBelts": true,      "cruiseStatus": {        "ccSwitch": false,        "ccSetSwitch": false,        "ccCoastSwitch": false,        "ccClutchSwitch": false,        "ccCruiseSwitch": false,        "ccResumeSwitch": false,        "ccAccelerationSwitch": false,        "ccBrakeSwitch": false,        "ccSpeed": 1      },      "parkingBrake": false,      "ignitionStatus": {        "ignitionAccessory": false,        "ignitionRunContact": false,        "ignitionCrankContact": false,        "ignitionAidContact": false      },      "forwardVehicleSpeed": 1,      "forwardVehicleDistance": 100,      "forwardVehicleElapsed": 2,      "odometer": 0    }  ],  "coarseVehicleLocationTimeHistory": {    "data": [      {        "id": "C4CA4238A0B923820DCC509A6F75849B",        "providerId": "api.provider.com",        "serverTime": "2019-04-05T02:04:16Z",        "vehicleId": "21232F297A57A5A743894A0E4A801FC3",        "dateTime": "2019-04-05T02:04:16Z",        "location": "37.4224764 -122.0842499"      }    ],    "timeResolution": "TIMERESOLUTION_NOT_MAX"  }}', self.response_catcher.response.raw_body)


    # Clients can request any region-specific waivers and break-rules for a given driver that are applicable within a given
    #time period.
    #
    #**Access Controls**
    #
    #|Role:  |Vehicle Query|Vehicle Follow|Driver Query|Driver Follow|Driver Dispatch|Driver Duty |HR          |Admin       |
    #|-------|-------------|--------------|------------|-------------|---------------|------------|------------|------------|
    #|Access:| **DENY**    | **DENY**     | ALLOW      | ALLOW       | **DENY**      | **DENY**   | ALLOW      | ALLOW      |
    def test_get_driver_breaks_and_waivers_1(self):
        # Parameters for the API call
        driver_id = '63A9F0EA7BB98050796B649E85481845'
        start_time = '2019-04-05T02:04:16Z'
        stop_time = '2019-04-05T02:04:16Z'

        # Perform the API call through the SDK function
        result = self.controller.get_driver_breaks_and_waivers(driver_id, start_time, stop_time)

        # Test response code
        self.assertEquals(self.response_catcher.response.status_code, 200)

        # Test headers
        expected_headers = {}
        expected_headers['content-type'] = None

        self.assertTrue(TestHelper.match_headers(expected_headers, self.response_catcher.response.headers))

        
        # Test whether the captured response is as we expected
        self.assertIsNotNone(result)
        self.assertEqual('{  "breakRules": [    {      "id": "C4CA4238A0B923820DCC509A6F75849B",      "providerId": "api.provider.com",      "serverTime": "2019-04-05T02:04:16Z",      "driverId": "63A9F0EA7BB98050796B649E85481845",      "activeFrom": "2019-04-05T02:04:16Z",      "activeTo": "2019-04-05T02:04:16Z",      "country": "CA",      "region": "ON"    }  ],  "waivers": [    {      "id": "C4CA4238A0B923820DCC509A6F75849B",      "providerId": "api.provider.com",      "serverTime": "2019-04-05T02:04:16Z",      "driverId": "63A9F0EA7BB98050796B649E85481845",      "country": "CA",      "region": "ON",      "waiverDay": "2019-04-05T02:04:16Z"    }  ]}', self.response_catcher.response.raw_body)


