using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

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

        [Route("fleet/locations")]
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


        [Route("fleet/flagged_events")]
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

        [Route("fleet/performance_events")]
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

        [Route("fleet/performance_thresholds")]
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

        [Route("fleet/faults")]
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

            List<VehicleFaultCodeEvent> faultEvents =
                               m_Context.VehicleFaultCodeEvent.Where(
                                       x => x.triggerDate >= startDate &&
                                       x.triggerDate <= stopDate).ToList();

            return faultEvents;
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

        [Route("fleet/faults/feed")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.DriverFollow + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.Admin)]
        public ActionResult<VehicleFaultCodeEventFollow> FeedFollowVehcicleFaults(string token)
        {
            DateTimeOffset fromTime;
            DateTimeOffset toTime = DateTimeOffset.Now.ToUniversalTime();

            if (string.IsNullOrEmpty(token))
            {
                //set a reasonable start time
                fromTime = new DateTimeOffset(2019, 01, 01, 0, 0, 0, 0, TimeSpan.FromHours(0));
            }
            else
            {
                string strFromTime = m_dataProtector.Unprotect(token);
                if (!DateTimeOffset.TryParse(strFromTime, out fromTime))
                    return BadRequest("token parameter invalid");
            }

            var vfcf = new VehicleFaultCodeEventFollow();
            vfcf.token = m_dataProtector.Protect(toTime.ToString());
            var logs = m_Context.VehicleFaultCodeEvent
                                       .Where(x => x.triggerDate >= fromTime && x.triggerDate <= toTime)
                                       .ToList();
            vfcf.feed = VehicleFaultCodeListToModelList(logs);
            return vfcf;
        }

        private List<VehicleFaultCodeModel> VehicleFaultCodeListToModelList(List<VehicleFaultCodeEvent> events)
        {
            List<VehicleFaultCodeModel> eventList = new List<VehicleFaultCodeModel>();
            foreach (VehicleFaultCodeEvent item in events)
                eventList.Add(new VehicleFaultCodeModel(item, m_appSettings.ProviderId));
            return eventList;
        }

        [Route("fleet/infos")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.VehicleQuery
            + "," + TelematicsRoles.DriverFollow + "," + TelematicsRoles.VehicleFollow)]
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
                flaggedVehiclePerformanceEvents = flaggedEventHistory,
                vehiclePerformanceEvents = performanceEventHistory,
                vehicleFaultCodeEvents = VehicleFaultCodeListToModelList(vehicleFaultCodeEventHistory)
            };
            return result;
        }

    }
}