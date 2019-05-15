# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class StopGeographicDetails(object):

    """Implementation of the 'Stop Geographic Details' model.

    TODO: type model description here.

    Attributes:
        id (string): The unique identifier for the specific Entity object in
            the system.
        provider_id (string): The unique 'Provider ID' of the TSP.
        server_time (string): Date and time when this object was received at
            the TSP
        stop_name (string): a name for this location
        address (string): an optional street address
        comment (string): an optional comment
        location (string): the location of the delivery date at this stop
        entry_area (list of string): optional geographic location polygon
            detailing the entryway area for this stop

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "id":'id',
        "provider_id":'providerId',
        "server_time":'serverTime',
        "stop_name":'stopName',
        "location":'location',
        "address":'address',
        "comment":'comment',
        "entry_area":'entryArea'
    }

    def __init__(self,
                 id=None,
                 provider_id=None,
                 server_time=None,
                 stop_name=None,
                 location=None,
                 address=None,
                 comment=None,
                 entry_area=None):
        """Constructor for the StopGeographicDetails class"""

        # Initialize members of the class
        self.id = id
        self.provider_id = provider_id
        self.server_time = server_time
        self.stop_name = stop_name
        self.address = address
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
        id = dictionary.get('id')
        provider_id = dictionary.get('providerId')
        server_time = dictionary.get('serverTime')
        stop_name = dictionary.get('stopName')
        location = dictionary.get('location')
        address = dictionary.get('address')
        comment = dictionary.get('comment')
        entry_area = dictionary.get('entryArea')

        # Return an object of this model
        return cls(id,
                   provider_id,
                   server_time,
                   stop_name,
                   location,
                   address,
                   comment,
                   entry_area)

