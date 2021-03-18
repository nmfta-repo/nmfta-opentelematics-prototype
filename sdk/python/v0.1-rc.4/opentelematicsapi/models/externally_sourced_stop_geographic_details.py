# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class ExternallySourcedStopGeographicDetails(object):

    """Implementation of the 'Externally Sourced Stop Geographic Details' model.

    TODO: type model description here.

    Attributes:
        comment (string): an optional comment
        location (string): the location of the delivery date at this stop
        entry_area (list of string): optional geographic location polygon
            detailing the entryway area for this stop

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "location":'location',
        "comment":'comment',
        "entry_area":'entryArea'
    }

    def __init__(self,
                 location=None,
                 comment=None,
                 entry_area=None):
        """Constructor for the ExternallySourcedStopGeographicDetails class"""

        # Initialize members of the class
        self.comment = comment
        self.location = location
        self.entry_area = entry_area


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
        location = dictionary.get('location')
        comment = dictionary.get('comment')
        entry_area = dictionary.get('entryArea')

        # Return an object of this model
        return cls(location,
                   comment,
                   entry_area)

