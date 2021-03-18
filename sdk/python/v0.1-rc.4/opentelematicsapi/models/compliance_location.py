# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class ComplianceLocation(object):

    """Implementation of the 'Compliance Location' model.

    TODO: type model description here.

    Attributes:
        latitude (float): the latitude of this location
        longitude (float): the longitude of this location
        identified_place (string): place name of the identified geo-location
        identified_state (string): state/province abbreviate of identified
            geo-location
        distance_from (float): distance from the identified geo-location, in
            m
        direction_from (string): cardinal direction from the identified
            geo-location

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "latitude":'latitude',
        "longitude":'longitude',
        "identified_place":'identifiedPlace',
        "identified_state":'identifiedState',
        "distance_from":'distanceFrom',
        "direction_from":'directionFrom'
    }

    def __init__(self,
                 latitude=None,
                 longitude=None,
                 identified_place=None,
                 identified_state=None,
                 distance_from=None,
                 direction_from=None):
        """Constructor for the ComplianceLocation class"""

        # Initialize members of the class
        self.latitude = latitude
        self.longitude = longitude
        self.identified_place = identified_place
        self.identified_state = identified_state
        self.distance_from = distance_from
        self.direction_from = direction_from


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
        latitude = dictionary.get('latitude')
        longitude = dictionary.get('longitude')
        identified_place = dictionary.get('identifiedPlace')
        identified_state = dictionary.get('identifiedState')
        distance_from = dictionary.get('distanceFrom')
        direction_from = dictionary.get('directionFrom')

        # Return an object of this model
        return cls(latitude,
                   longitude,
                   identified_place,
                   identified_state,
                   distance_from,
                   direction_from)

