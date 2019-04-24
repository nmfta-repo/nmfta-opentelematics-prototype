from datetime import datetime
from dateutil import tz

class status_model(object):
    def __init__(self, serviceStatus, dateTime : datetime, factors = None):
        if factors is None:
            self.factors = []
        else:
            self.factors = factors
        self.service_status = serviceStatus
        
        from_zone = tz.tzutc()
        to_zone = tz.tzlocal()
        utc = dateTime.replace(tzinfo=from_zone)
        self.as_of = utc.astimezone(to_zone)
        #self.factors = factors
