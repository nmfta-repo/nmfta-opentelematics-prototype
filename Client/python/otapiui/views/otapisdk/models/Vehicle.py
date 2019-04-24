class Vehicle:
    # details elided
    @property
    def vehicle_info(self):
        return self.company_name + " - " + \
               self.vehicle_id
    def __init__(self, company_name, vehicle_id):
         self.company_name = company_name
         self.vehicle_id = vehicle_id
    def change_vehicle_info(self, company_name, vehicle_id):
        self.company_name = company_name
        self.vehicle_id = vehicle_id