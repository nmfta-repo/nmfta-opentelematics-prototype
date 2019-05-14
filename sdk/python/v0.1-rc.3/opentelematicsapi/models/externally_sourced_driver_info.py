# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class ExternallySourcedDriverInfo(object):

    """Implementation of the 'Externally Sourced Driver Info' model.

    TODO: type model description here.

    Attributes:
        username (string): a username of this driver
        hours_worked (float): the hours worked on-duty by this user so far
            today (on any duty at all)
        driver_license_number (string): the driver's license number
        country (string): short code for the country of the driver's license
        region (string): short code for the country's
            region/state/province/territory of the driver's license
        driver_home_terminal (string): the home terminal of the driver

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "username":'username',
        "hours_worked":'hoursWorked',
        "driver_license_number":'driverLicenseNumber',
        "country":'country',
        "region":'region',
        "driver_home_terminal":'driverHomeTerminal'
    }

    def __init__(self,
                 username=None,
                 hours_worked=None,
                 driver_license_number=None,
                 country=None,
                 region=None,
                 driver_home_terminal=None):
        """Constructor for the ExternallySourcedDriverInfo class"""

        # Initialize members of the class
        self.username = username
        self.hours_worked = hours_worked
        self.driver_license_number = driver_license_number
        self.country = country
        self.region = region
        self.driver_home_terminal = driver_home_terminal


    @classmethod
    def from_dictionary(cls,
                        dictionary):
        """Creates an instance of this model from a dictionary

        Args:
            dictionary (dictionary): A dictionary representation of the object as
            obtained from the deserialization of the server's response. The keys
            MUST match property names in the API description.

        Returns:
            object: An instance of this structure class.

        """
        if dictionary is None:
            return None

        # Extract variables from the dictionary
        username = dictionary.get('username')
        hours_worked = dictionary.get('hoursWorked')
        driver_license_number = dictionary.get('driverLicenseNumber')
        country = dictionary.get('country')
        region = dictionary.get('region')
        driver_home_terminal = dictionary.get('driverHomeTerminal')

        # Return an object of this model
        return cls(username,
                   hours_worked,
                   driver_license_number,
                   country,
                   region,
                   driver_home_terminal)


