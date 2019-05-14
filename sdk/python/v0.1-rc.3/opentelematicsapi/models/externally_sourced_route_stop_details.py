# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class ExternallySourcedRouteStopDetails(object):

    """Implementation of the 'Externally Sourced Route Stop Details' model.

    TODO: type model description here.

    Attributes:
        stop_name (string): a name for the stop location
        stop_address (string): an optional street address
        stop_location (string): the location of the stop
        stop_deadline (string): optional time and date when the stop must be
            arrived at

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "stop_name":'stopName',
        "stop_location":'stopLocation',
        "stop_address":'stopAddress',
        "stop_deadline":'stopDeadline'
    }

    def __init__(self,
                 stop_name=None,
                 stop_location=None,
                 stop_address=None,
                 stop_deadline=None):
        """Constructor for the ExternallySourcedRouteStopDetails class"""

        # Initialize members of the class
        self.stop_name = stop_name
        self.stop_address = stop_address
        self.stop_location = stop_location
        self.stop_deadline = stop_deadline


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
        stop_name = dictionary.get('stopName')
        stop_location = dictionary.get('stopLocation')
        stop_address = dictionary.get('stopAddress')
        stop_deadline = dictionary.get('stopDeadline')

        # Return an object of this model
        return cls(stop_name,
                   stop_location,
                   stop_address,
                   stop_deadline)


class ExternallySourcedRouteStartStopDetails(ExternallySourcedRouteStopDetails):

    """Implementation of the 'Externally Sourced Route Start Stop Details' model.

    TODO: type model description here.
    NOTE: This class inherits from 'ExternallySourcedRouteStopDetails'.

    Attributes:
        start_name (string): a name for the start location
        start_address (string): an optional street address of the start
        start_location (string): the location of the start
        route_add_instructions (string): an optional comment with details
            about the route

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "start_name":'startName',
        "start_location":'startLocation',
        "stop_name":'stopName',
        "stop_location":'stopLocation',
        "start_address":'startAddress',
        "route_add_instructions":'routeAddInstructions',
        "stop_address":'stopAddress',
        "stop_deadline":'stopDeadline'
    }

    def __init__(self,
                 start_name=None,
                 start_location=None,
                 stop_name=None,
                 stop_location=None,
                 start_address=None,
                 route_add_instructions=None,
                 stop_address=None,
                 stop_deadline=None):
        """Constructor for the ExternallySourcedRouteStartStopDetails class"""

        # Initialize members of the class
        self.start_name = start_name
        self.start_address = start_address
        self.start_location = start_location
        self.route_add_instructions = route_add_instructions

        # Call the constructor for the base class
        super(ExternallySourcedRouteStartStopDetails, self).__init__(stop_name,
                                                                     stop_location,
                                                                     stop_address,
                                                                     stop_deadline)


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
        start_name = dictionary.get('startName')
        start_location = dictionary.get('startLocation')
        stop_name = dictionary.get('stopName')
        stop_location = dictionary.get('stopLocation')
        start_address = dictionary.get('startAddress')
        route_add_instructions = dictionary.get('routeAddInstructions')
        stop_address = dictionary.get('stopAddress')
        stop_deadline = dictionary.get('stopDeadline')

        # Return an object of this model
        return cls(start_name,
                   start_location,
                   stop_name,
                   stop_location,
                   start_address,
                   route_add_instructions,
                   stop_address,
                   stop_deadline)


