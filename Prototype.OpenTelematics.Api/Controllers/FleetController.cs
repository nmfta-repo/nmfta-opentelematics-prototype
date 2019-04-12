using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.Models;
using Microsoft.EntityFrameworkCore;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    [Authorize]
     public class FleetController : TelematicsBaseController
    {
        private readonly TelematicsContext m_Context;

        public FleetController(TelematicsContext context)
        {
            m_Context = context;
        }

        [Route("api/fleet/coarse_locations")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<LocationHistory> FleetCourseLocations(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            LocationHistory result = GetCourseLocationHistory(startDate, stopDate);

            return result;
        }

        [Route("api/fleet/vehicle_infos")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow)]
        public ActionResult<VehicleInfoHistory> FleetVehicleInfos(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            LocationHistory locHistory = GetCourseLocationHistory(startDate, stopDate);
            List<VehicleFlaggedEvent> flaggedEventHistory =
                                           m_Context.VehicleFlaggedEvent.Where(
                                                   x => x.eventStart >= startDate &&
                                                   x.eventStart <= stopDate).ToList();
            List<VehiclePerformanceEvent> performanceEventHistory =
                                            m_Context.VehiclePerformanceEvent
                                                .Include(VehiclePerformanceThreshold => VehiclePerformanceThreshold.thresholds)
                                                .Where(
                                                    x => x.eventStart >= startDate &&
                                                    x.eventStart <= stopDate).ToList();
            List<VehicleFaultCodeEvent> vehicleFaultCodeEventHistory =
                                            m_Context.VehicleFaultCodeEvent.Where(
                                                    x => x.triggerDate >= startDate &&
                                                    x.triggerDate <= stopDate).ToList();
            VehicleInfoHistory result = new VehicleInfoHistory
            {
                coarseVehicleLocationTimeHistories = locHistory,
                vehicleFlaggedEvents = flaggedEventHistory,
                vehiclePerformanceEvents = performanceEventHistory,
                vehicleFaultCodeEvents = vehicleFaultCodeEventHistory
            };
            return result;
        }

        private LocationHistory GetCourseLocationHistory(DateTime startDate, DateTime stopDate)
        {
            var data = m_Context.CoarseVehicleLocationTimeHistory.Where(
                                                 x => x.dateTime >= startDate &&
                                                      x.dateTime <= stopDate).ToList();

            var result = new LocationHistory(data);
            //TODO: How to determine timeResolution?
            result.timeResolution = TimeResolution.TIMERESOLUTION_MAX;
            return result;
        }

    }
}