# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class TestIfCompleteExportReadyResponse(object):

    """Implementation of the 'Test if Complete Export Ready response' model.

    TODO: type model description here.

    Attributes:
        location (string): a URL where the complete file can be retrieved. If
            it is under that of the OTAPI endpoints then the same
            authentication and authorization schemes must be enforced. If the
            files are made available for download elsewhere then they must not
            be made accessible without any authentication or authorization and
            the client is expected to be configured with the appropriate
            credentials for download independently of responses from the OTAPI
            server.

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "location":'location'
    }

    def __init__(self,
                 location=None):
        """Constructor for the TestIfCompleteExportReadyResponse class"""

        # Initialize members of the class
        self.location = location


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

        # Return an object of this model
        return cls(location)


