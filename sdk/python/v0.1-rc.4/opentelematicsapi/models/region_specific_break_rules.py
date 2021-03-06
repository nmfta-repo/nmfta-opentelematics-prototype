# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class RegionSpecificBreakRules(object):

    """Implementation of the 'Region Specific Break Rules' model.

    TODO: type model description here.

    Attributes:
        id (string): The unique identifier for the specific Entity object in
            the system.
        provider_id (string): The unique 'Provider ID' of the TSP
        server_time (string): Date and time when this object was received at
            the TSP
        driver_id (string): The id of the driver with the region specific
            break rules.
        active_from (string): The date and time the break rules take effect
        active_to (string): The date and time the break rules stop taking
            effect, if left blank then the rules apply in perpetuity
        country (string): short code for the country of the region dictating
            the specific break rules
        region (string): short code for the country's
            region/state/province/territory dictating the specific break
            rules

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "id":'id',
        "provider_id":'providerId',
        "server_time":'serverTime',
        "driver_id":'driverId',
        "active_from":'activeFrom',
        "active_to":'activeTo',
        "country":'country',
        "region":'region'
    }

    def __init__(self,
                 id=None,
                 provider_id=None,
                 server_time=None,
                 driver_id=None,
                 active_from=None,
                 active_to=None,
                 country=None,
                 region=None):
        """Constructor for the RegionSpecificBreakRules class"""

        # Initialize members of the class
        self.id = id
        self.provider_id = provider_id
        self.server_time = server_time
        self.driver_id = driver_id
        self.active_from = active_from
        self.active_to = active_to
        self.country = country
        self.region = region


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
        driver_id = dictionary.get('driverId')
        active_from = dictionary.get('activeFrom')
        active_to = dictionary.get('activeTo')
        country = dictionary.get('country')
        region = dictionary.get('region')

        # Return an object of this model
        return cls(id,
                   provider_id,
                   server_time,
                   driver_id,
                   active_from,
                   active_to,
                   country,
                   region)


