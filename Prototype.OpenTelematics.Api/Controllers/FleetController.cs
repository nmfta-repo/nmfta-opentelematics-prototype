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
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    [Authorize]
     public class FleetController : TelematicsBaseController
    {

        public FleetController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider) 
            : base(context, settings, provider)
        {

        }

        [Route("api/fleet/locations")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<LocationHistory> Locations(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            LocationHistory result = GetCourseLocationHistory(startDate, stopDate);

            return result;
        }


        [Route("api/fleet/flagged_events")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehicleFlaggedEvent>> FlaggedEvents(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            List<VehicleFlaggedEvent> flaggedEventHistory =
                               m_Context.VehicleFlaggedEvent.Where(
                                       x => x.eventStart >= startDate &&
                                       x.eventStart <= stopDate).ToList();

            return flaggedEventHistory;
        }

        [Route("api/fleet/performance_events")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehiclePerformanceEvent>> PerformanceEvents(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            List<VehiclePerformanceEvent> performanceEventHistory =
                               m_Context.VehiclePerformanceEvent.Where(
                                       x => x.eventStart >= startDate &&
                                       x.eventStart <= stopDate).ToList();

            return performanceEventHistory;
        }

        [Route("api/fleet/performance_thresholds")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehiclePerformanceThreshold>> PerformanceThresholds(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            List<VehiclePerformanceThreshold> performanceThresholds =
                               m_Context.VehiclePerformanceThreshold.Where(
                                       x => x.activeFrom >= startDate &&
                                       x.activeFrom <= stopDate).ToList();

            return performanceThresholds;
        }

        [Route("api/fleet/faults")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehicleFaultCodeEvent>> VehicleFaults(string start, string stop)
        {
            if (!DateTime.TryParse(start, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start date");
            if (!DateTime.TryParse(stop, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop date");

            List<VehicleFaultCodeEvent> performanceThresholds =
                               m_Context.VehicleFaultCodeEvent.Where(
                                       x => x.triggerDate >= startDate &&
                                       x.triggerDate <= stopDate).ToList();

            return performanceThresholds;
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