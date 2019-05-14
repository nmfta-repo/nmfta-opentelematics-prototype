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
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<LocationHistory> Locations(string startTime, string stopTime, int? page, int? count)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return BadRequest("Invalid startTime");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return BadRequest("Invalid stopTime");
            page = page ?? 1;
            count = count ?? 50;
            int totalCount = 0;

            LocationHistory result = GetPageLocationHistory(startDate, stopDate, false, (int)page, (int)count, ref totalCount);
            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return result;
        }

        [Route("fleet/locations/latest")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<LocationHistory> Locations(int? page, int? count)
        {
            page = page ?? 1;
            count = count ?? 50;
            int totalCount = 0;

            LocationHistory result = GetPageLatestCourseLocationHistory(false, (int)page, (int)count, ref totalCount);
            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return result;
        }

        [Route("fleet/locations/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehicleLocation> Location(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehicleLocationTimeHistory result = m_Context.VehicleLocationTimeHistory.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return new VehicleLocation(result, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }


        [Route("fleet/flagged_events")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehicleFlaggedEvent>> FlaggedEvents(string startTime, string stopTime)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start time");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop time");

            List<VehicleFlaggedEvent> flaggedEventHistory =
                               m_Context.VehicleFlaggedEvent.Where(
                                       x => x.eventStart >= startDate &&
                                       x.eventStart <= stopDate).ToList();

            return flaggedEventHistory;
        }

        [Route("fleet/flagged_events/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehicleFaultCodeModel> FlaggedEvent(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehicleFaultCodeEvent result = m_Context.VehicleFaultCodeEvent.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return new VehicleFaultCodeModel(result, m_appSettings.ProviderId);
                else
                    return NotFound("Invalid id");
            }
            return NotFound("Invalid id");
        }


        [Route("fleet/performance_events")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehiclePerformanceEvent>> PerformanceEvents(string startTime, string stopTime)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start time");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop time");

            List<VehiclePerformanceEvent> performanceEventHistory =
                               m_Context.VehiclePerformanceEvent.Where(
                                       x => x.eventStart >= startDate &&
                                       x.eventStart <= stopDate).ToList();

            return performanceEventHistory;
        }

        [Route("fleet/performance_events/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehiclePerformanceEventModel> PerformanceEvent(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehiclePerformanceEvent data = m_Context.VehiclePerformanceEvent.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                {
                    if (data.performanceThresholdId != null)
                    {
                        VehiclePerformanceThreshold threshold = m_Context.VehiclePerformanceThreshold.FirstOrDefault(c => c.Id == data.performanceThresholdId);
                        data.thresholds = threshold;
                    }
                    return new VehiclePerformanceEventModel(data, m_appSettings.ProviderId);
                }
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }

        [Route("fleet/performance_thresholds")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehiclePerformanceThreshold>> PerformanceThresholds(string startTime, string stopTime)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start time");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop time");

            List<VehiclePerformanceThreshold> performanceThresholds =
                               m_Context.VehiclePerformanceThreshold.Where(
                                       x => x.activeTo >= startDate &&
                                       x.activeFrom <= stopDate).ToList();

            return performanceThresholds;
        }

        [Route("fleet/performance_thresholds/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehiclePerformanceThresholdModel> PerformanceThreshold(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehiclePerformanceThreshold data = m_Context.VehiclePerformanceThreshold.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new VehiclePerformanceThresholdModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }

        [Route("fleet/faults")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverDispatch)]
        public ActionResult<List<VehicleFaultCodeEvent>> VehicleFaults(string startTime, string stopTime)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return NotFound("Invalid start time");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return NotFound("Invalid stop time");

            List<VehicleFaultCodeEvent> faultEvents =
                               m_Context.VehicleFaultCodeEvent.Where(
                                       x => x.triggerDate >= startDate &&
                                       x.triggerDate <= stopDate).ToList();

            return faultEvents;
        }


        [Route("fleet/faults/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehicleFaultCodeModel> VehicleFault(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehicleFaultCodeEvent result = m_Context.VehicleFaultCodeEvent.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return new VehicleFaultCodeModel(result, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }


        [Route("fleet/flagged_faults/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<VehicleFaultCodeModel> VehicleFlaggedFault(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                VehicleFaultCodeEvent result = m_Context.VehicleFaultCodeEvent.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return new VehicleFaultCodeModel(result, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }

        private LocationHistory GetPageLocationHistory(DateTime startDate, DateTime stopDate, bool isCourse, int page, int count, ref int totalCount)
        {
            totalCount = m_Context.VehicleLocationTimeHistory.Where( 
                                    x => x.dateTime >= startDate &&
                                    x.dateTime <= stopDate)
                                    .Count();


            var data = m_Context.VehicleLocationTimeHistory.Where(
                                    x => x.dateTime >= startDate &&
                                    x.dateTime <= stopDate)
                                    .OrderBy(x => x.sequence)
                                    .Skip((page-1)*count)
                                    .Take(count)
                                    .ToList();

            LocationHistory locHistory;
            if (isCourse)
                locHistory = new CoarseVehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);
            else
                locHistory = new VehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);

            return locHistory;
        }

        private LocationHistory GetPageLatestCourseLocationHistory(bool isCourse, int page, int count, ref int totalCount)
        {
            totalCount = m_Context.VehicleLocationTimeHistory
                            .GroupBy(l => l.vehicleId, (key, g) => g.OrderByDescending(e => e.dateTime).FirstOrDefault())
                            .Count();


            var data = m_Context.VehicleLocationTimeHistory
                            .GroupBy(l => l.vehicleId, (key, g) => g.OrderByDescending(e => e.dateTime).FirstOrDefault())
                            .OrderBy(x => x.sequence)
                            .Skip((page - 1) * count)
                            .Take(count)
                            .ToList();

            LocationHistory locHistory;
            if (isCourse)
                locHistory = new CoarseVehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);
            else
                locHistory = new VehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);
            return locHistory;
        }

        private LocationHistory GetCourseLocationHistory(DateTime startDate, DateTime stopDate, bool isCourse)
        {

            var data = m_Context.VehicleLocationTimeHistory.Where(
                                                x => x.dateTime >= startDate &&
                                                x.dateTime <= stopDate)
                                                .ToList();

            LocationHistory locHistory;
            if (isCourse)
                locHistory = new CoarseVehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);
            else
                locHistory = new VehicleLocationTimeHistoryModel(data, m_appSettings.ProviderId);
            return locHistory;
        }

        [Route("fleet/faults/feed")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.DriverFollow + "," + TelematicsRoles.VehicleFollow
            + "," + TelematicsRoles.Admin)]
        public ActionResult<VehicleFaultCodeEventFollow> FeedFollowVehicleFaults(string token)
        {
            DateTimeOffset fromTime;
            DateTimeOffset toTime = DateTimeOffset.Now.ToUniversalTime();

            if (string.IsNullOrEmpty(token))
            {
                //TODO: When null, should be set to 'now'
                //for the demo, set start time = 5/1/19
                fromTime = new DateTimeOffset(2019, 05, 01, 0, 0, 0, 0, TimeSpan.FromHours(0));
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
        public ActionResult<VehicleInfoHistory> FleetVehicleInfos(string startTime, string stopTime)
        {
            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDate))
                return BadRequest("Invalid startTime");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDate))
                return BadRequest("Invalid stopTime");

            LocationHistory locHistory = GetCourseLocationHistory(startDate, stopDate, true);
            List<VehicleFaultCodeEvent> flaggedEventHistory =
                                            m_Context.VehicleFaultCodeEvent.Where(
                                                    x => x.triggerDate >= startDate &&
                                                    x.triggerDate <= stopDate &&
                                                    x.urgentFlag == true).ToList();
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
                flaggedVehicleFaultEvents = VehicleFaultCodeListToModelList(flaggedEventHistory),
                vehiclePerformanceEvents = VehiclePerformanceListToModelList(performanceEventHistory),
                vehicleFaultCodeEvents = VehicleFaultCodeListToModelList(vehicleFaultCodeEventHistory)
            };
            return result;
        }
        [Route("fleet/infos/feed")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin    
            + "," + TelematicsRoles.DriverFollow + "," + TelematicsRoles.VehicleFollow)]
        public ActionResult<VehicleFeedInfosFollow> FleetVehicleInfos(string token)
        {
            DateTimeOffset fromTime;
            DateTimeOffset toTime = DateTimeOffset.Now.ToUniversalTime();

            if (string.IsNullOrEmpty(token))
            {
                //TODO: When null, should be set to 'now'
                //for the demo, set start time = 5/1/19
                fromTime = new DateTimeOffset(2019, 05, 01, 0, 0, 0, 0, TimeSpan.FromHours(0)).ToUniversalTime();
            }
            else
            {
                string strFromTime = m_dataProtector.Unprotect(token);
                if (!DateTimeOffset.TryParse(strFromTime, out fromTime))
                    return BadRequest("token parameter invalid");
            }
            var vfif = new VehicleFeedInfosFollow();
            vfif.token = m_dataProtector.Protect(toTime.ToString());

            LocationHistory locHistory = GetCourseLocationHistory(fromTime.DateTime, toTime.DateTime, true);
            List<VehicleFaultCodeEvent> flaggedEventHistory =
                                            m_Context.VehicleFaultCodeEvent.Where(
                                                    x => x.triggerDate >= fromTime &&
                                                    x.triggerDate <= toTime &&
                                                    x.urgentFlag == true).ToList();
            List<VehiclePerformanceEvent> performanceEventHistory =
                                            m_Context.VehiclePerformanceEvent
                                                .Include(VehiclePerformanceThreshold => VehiclePerformanceThreshold.thresholds)
                                                .Where(
                                                    x => x.eventStart >= fromTime &&
                                                    x.eventStart <= toTime).ToList();
            List<VehicleFaultCodeEvent> vehicleFaultCodeEventHistory =
                                            m_Context.VehicleFaultCodeEvent.Where(
                                                    x => x.triggerDate >= fromTime &&
                                                    x.triggerDate <= toTime).ToList();
            VehicleInfoHistory history = new VehicleInfoHistory
            {
                coarseVehicleLocationTimeHistories = locHistory,
                flaggedVehicleFaultEvents = VehicleFaultCodeListToModelList(flaggedEventHistory),
                vehiclePerformanceEvents = VehiclePerformanceListToModelList(performanceEventHistory),
                vehicleFaultCodeEvents = VehicleFaultCodeListToModelList(vehicleFaultCodeEventHistory)
            };
            vfif.feed = history;
            return vfif;
        }

        private List<VehiclePerformanceEventModel> VehiclePerformanceListToModelList(List<VehiclePerformanceEvent> events)
        {
            List<VehiclePerformanceEventModel> eventList = new List<VehiclePerformanceEventModel>();
            foreach (VehiclePerformanceEvent item in events)
                eventList.Add(new VehiclePerformanceEventModel(item, m_appSettings.ProviderId));
            return eventList;
        }

    }
}