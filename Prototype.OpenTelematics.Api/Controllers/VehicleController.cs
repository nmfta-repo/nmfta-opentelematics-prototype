﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    public class VehicleController : TelematicsBaseController
    {

        public VehicleController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider) 
            : base(context, settings, provider)
        {

        }

        [Route("api/vehicles/{vehicleId}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow)]
        public ActionResult<VehicleModel> Get(string vehicleId)
        {
            if (Guid.TryParse(vehicleId, out var guid))
            {
                Vehicle result = m_Context.Vehicle.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return new VehicleModel(result, m_appSettings.ProviderId);
                else
                    return NotFound("Invalid id");
            }
            return NotFound("Invalid id");
        }

        [Route("api/vehicles/{vehicleId}/flagged_events")]
        [HttpGet]
        [Authorize(Roles =
        TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery)]
        public ActionResult<List<VehicleFlaggedEvent>> FlaggedEvents(string vehicleId, string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");
            if (!Guid.TryParse(vehicleId, out var guid))
                return NotFound("Invalid vehicle id");

            var vehicle = m_Context.Vehicle.FirstOrDefault(c => c.Id == guid);
            if (vehicle != null)
            {
                var result = m_Context.VehicleFlaggedEvent.Where(x => x.vehicleId == guid &&
                                                                 x.eventStart >= startDate &&
                                                                 x.eventEnd <= stopDate).ToList();
                return result;
            }
            else
                return NotFound("Invalid vehicle id");
        }

        [Route("api/vehicles/{vehicleId}/coarse_locations")]
        [HttpGet]
        [Authorize(Roles =
        TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
             + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<LocationHistory> VehicleCourseLocations(string vehicleId, string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");
            if (!Guid.TryParse(vehicleId, out var guid))
                return NotFound("Invalid vehicle id");

            var data = m_Context.CoarseVehicleLocationTimeHistory.Where(
                                                 x => x.dateTime >= startDate &&
                                                      x.dateTime <= stopDate && 
                                                      x.vehicleId == guid).ToList();

            var result = new LocationHistory(data);
            //TODO: How to determine timeResolution?
            result.timeResolution = TimeResolution.TIMERESOLUTION_MAX;
            return result;
        }

        [Route("api/vehicles/{vehicleId}/performance_events")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<List<VehiclePerformanceEvent>> VehiclePerformanceEvents(string vehicleId, string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");
            if (!Guid.TryParse(vehicleId, out var guid))
                return NotFound("Invalid vehicle id");

            var data = m_Context.VehiclePerformanceEvent
                                            .Include(e => e.thresholds)
                                            .Where(x => x.eventStart >= startDate &&
                                                   x.eventStart <= stopDate &&
                                                   x.vehicleId == guid).ToList();

            return data;
        }


        [Route("api/vehicles/{vehicleId}/fault_code_events")]
        [HttpGet]
        [Authorize(Roles =
        TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<List<VehicleFaultCodeEvent>> VehicleFaultCodeEvents(string vehicleId, string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");
            if (!Guid.TryParse(vehicleId, out var guid))
                return NotFound("Invalid vehicle id");

            var data = m_Context.VehicleFaultCodeEvent.Where(
                                                 x => x.triggerDate >= startDate &&
                                                      x.triggerDate <= stopDate &&
                                                      x.vehicleId == guid).ToList();

            return data;
        }
    }
}