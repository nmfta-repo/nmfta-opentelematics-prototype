# -*- coding: utf-8 -*-

"""
    opentelematicsapi

    This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
"""


class PerformanceThresholds(object):

    """Implementation of the 'Performance Thresholds' model.

    TODO: type model description here.

    Attributes:
        id (string): The unique identifier for the specific Entity object in
            the system.
        provider_id (string): The unique 'Provider ID' of the TSP
        server_time (string): Date and time when this object was received at
            the TSP
        active_from (string): The date and time these thresholds take effect
        active_to (string): The date and time these thresholds stop taking
            effect, if left blank then the rules apply in perpetuity
        rpm_over_value (float): the configured RPM threshold, above which
            engine RPM readings are considered 'over', in revolutions per
            minute
        over_speed_value (float): the configured speed threshold, above which
            speed readings are considered 'over', in km/h
        excess_speed_value (float): the configured speed threshold, above
            which the speed readings are considered 'excess', in km/h
        long_idle_value (float): the configured time threshold, beyond which
            time spent in idle will be considered 'long', in seconds
        hi_throttle_value (float): the configured throttle threshold, above
            which throttle values are considered 'hi'

    """

    # Create a mapping from Model property names to API property names
    _names = {
        "id":'id',
        "provider_id":'providerId',
        "server_time":'serverTime',
        "active_from":'activeFrom',
        "active_to":'activeTo',
        "rpm_over_value":'rpmOverValue',
        "over_speed_value":'overSpeedValue',
        "excess_speed_value":'excessSpeedValue',
        "long_idle_value":'longIdleValue',
        "hi_throttle_value":'hiThrottleValue'
    }

    def __init__(self,
                 id=None,
                 provider_id=None,
                 server_time=None,
                 active_from=None,
                 active_to=None,
                 rpm_over_value=None,
                 over_speed_value=None,
                 excess_speed_value=None,
                 long_idle_value=None,
                 hi_throttle_value=None):
        """Constructor for the PerformanceThresholds class"""

        # Initialize members of the class
        self.id = id
        self.provider_id = provider_id
        self.server_time = server_time
        self.active_from = active_from
        self.active_to = active_to
        self.rpm_over_value = rpm_over_value
        self.over_speed_value = over_speed_value
        self.excess_speed_value = excess_speed_value
        self.long_idle_value = long_idle_value
        self.hi_throttle_value = hi_throttle_value


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
        active_from = dictionary.get('activeFrom')
        active_to = dictionary.get('activeTo')
        rpm_over_value = dictionary.get('rpmOverValue')
        over_speed_value = dictionary.get('overSpeedValue')
        excess_speed_value = dictionary.get('excessSpeedValue')
        long_idle_value = dictionary.get('longIdleValue')
        hi_throttle_value = dictionary.get('hiThrottleValue')

        # Return an object of this model
        return cls(id,
                   provider_id,
                   server_time,
                   active_from,
                   active_to,
                   rpm_over_value,
                   over_speed_value,
                   excess_speed_value,
                   long_idle_value,
                   hi_throttle_value)

