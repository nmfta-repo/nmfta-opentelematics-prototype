using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;

namespace Prototype.OpenTelematics.Api.Controllers
{
    /// <summary>
    /// Controller that process and returns data for all Driver related operations
    /// </summary>
    [ApiController]
    [Authorize]
    public class DriverController : TelematicsBaseController
    {

        public DriverController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider) 
            : base(context, settings, provider)
        {

        }

        /// <summary>
        /// Returns All Driver Information
        /// <para>TODO:K Support Paging and Time Constraints</para>
        /// </summary>
        /// <returns></returns>
        [Route("drivers")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<List<Driver>> AllDrivers()
        {
                var result = m_Context.Driver.ToList();
                return result;
        }

        /// <summary>
        /// Returns specific driver information for passed in <paramref name="id"/>
        /// </summary>
        /// <param name="id">Driver Identifier</param>
        /// <returns>Driver Data</returns>
        [Route("drivers/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverModel> Get(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var data = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new DriverModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            else
                return BadRequest("Invalid id");
        }

        /// <summary>
        /// Returns all Driver Break Rules
        /// </summary>
        /// <returns>Driver Break Rules</returns>
        [Route("region_specific_breaks")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<List<DriverBreakRule>> AllBreakRules()
        {
                var result = m_Context.DriverBreakRule.ToList();
                return result;
        }

        /// <summary>
        /// Returns specific driver break rule for passed in <paramref name="id"/>
        /// </summary>
        /// <param name="id">Break Rule Identifier</param>
        /// <returns>Driver Break Rule</returns>
        [Route("region_specific_breaks/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverBreakRuleModel> GetBreakRuleById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var data = m_Context.DriverBreakRule.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new DriverBreakRuleModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            else
                return BadRequest("Invalid id");
        }

        /// <summary>
        /// Returns all Driver Waivers
        /// <para>TODO:K Support Paging and Time Constraints</para>
        /// </summary>
        /// <returns>Region Specific Waivers</returns>
        [Route("region_specific_waivers")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<List<DriverWaiver>> AllWaivers()
        {
            var result = m_Context.DriverWaiver.ToList();
            return result;
        }

        /// <summary>
        /// Returns specific waiver for passed in <paramref name="id"/>
        /// </summary>
        /// <param name="id">Region Specific Waiver Data</param>
        /// <returns></returns>
        [Route("region_specific_waivers/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverWaiverModel> GetWaiverById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var data = m_Context.DriverWaiver.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new DriverWaiverModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            else
                return BadRequest("Invalid id");
        }

        /// <summary>
        /// Returns Driver Availability factors for passed in driver <paramref name="id"/>, start date and stop time
        /// </summary>
        /// <param name="id">Driver Identifier</param>
        /// <param name="start">Start Date</param>
        /// <param name="stop">Stop Date</param>
        /// <returns>Driver Availability Factors</returns>
        [Route("drivers/{driverId}/availability_factors")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverAvailability> Get(string driverId, DateTime startTime, DateTime stopTime)
        {
            if (!Guid.TryParse(driverId, out var guid))
            {
                return BadRequest("Invalid driverId");
            }

            var driver = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
            if (driver == null)
                return NotFound("driverId Not Found");

            var courseLocationHistory =
                m_Context.VehicleLocationTimeHistory.Where(c => c.driverId == guid && c.dateTime >= startTime && c.dateTime <= stopTime);
            var dutyStatusLogs = m_Context.LogEvent.Where(c => c.driverId == guid && c.dateTime >= startTime && c.dateTime <= stopTime);
            var vehicleFlaggedEvents =
                m_Context.VehicleFlaggedEvent.Where(c => c.driverId == guid && c.eventStart >= startTime && c.eventEnd <= stopTime);
            var locationhistoryModel = new CoarseVehicleLocationTimeHistoryModel(courseLocationHistory.ToList(), m_appSettings.ProviderId);

            var model = new DriverAvailability
            {
                CoarseVehicleLocationTimeHistory = locationhistoryModel,
                LogEvents = LogEventsToLogEventModel(dutyStatusLogs.ToList()).ToArray(),
                VehicleFlaggedEvents = VehicleFlaggedEventsToVehicleFlaggedEventsModel(vehicleFlaggedEvents.ToList()).ToArray()
            };

            return model;
        }

        /// <summary>
        /// Returns Driver Break Rules and Waivers for passed in driver <paramref name="id"/>, start date and stop time
        /// </summary>
        /// <param name="id">Driver Identifier</param>
        /// <param name="start">Start Date</param>
        /// <param name="stop">Stop Date</param>
        /// <returns>Driver Break Rules and Waivers</returns>
        [Route("drivers/{driverId}/breaks_and_waivers")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + 
                           TelematicsRoles.DriverFollow + "," + TelematicsRoles.HR)]
        public ActionResult<BreakRulesAndWaivers> GetBreakRulesAndWaivers(string driverId, DateTime startTime, DateTime stopTime)
        {
            if (!Guid.TryParse(driverId, out var guid))
            {
                return BadRequest("Invalid driverId");
            }
            var driver = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
            if (driver == null)
                return NotFound("driverId Not Found");

            var driverBreakRules = m_Context.DriverBreakRule.Where(c => c.driverId == guid && c.activeFrom >= startTime && c.activeTo <= stopTime);
            var driverWaivers = m_Context.DriverWaiver.Where(c => c.driverId == guid && c.waiverDay >= startTime && c.waiverDay <= stopTime);

            var model = new BreakRulesAndWaivers
            {
                BreakRules = BreakRulesToBreakRulesModel(driverBreakRules.ToList()).ToArray(),
                Waivers = WaiversToWaiverModels(driverWaivers.ToList()).ToArray()
            };

            return model;
        }

        /// <summary>
        /// Update Duty Status Change
        /// <para>TODO:K Validate Incoming Data and Save to back-end</para>
        /// </summary>
        /// <param name="driverId">Driver Identifier</param>
        /// <param name="postedModel">Updated Duty Status Change</param>
        /// <returns>Patch Status</returns>
        [Route("/drivers/{driverId}/duty_status")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<string> PatchDutyStatusChange(string driverId, DutyStatusChangeModel postedModel)
        {
            System.IO.File.WriteAllText(@"DutyStatusChange_REQ.json",postedModel.ToJson());
            return string.Empty;
        }

        [Route("/drivers/{driverId}")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.HR)]
        public ActionResult<string> Driver(string driverId, DriverChangeModel model)
        {
            if (Guid.TryParse(driverId, out var guid))
            {
                var driver = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
                if (driver != null)
                {
                    driver.username = model.username;
                    driver.driverLicenseNumber = model.driverLicenseNumber ?? driver.driverLicenseNumber;
                    driver.driverHomeTerminal = model.driverHomeTerminal ?? driver.driverHomeTerminal;
                    driver.country = model.country ?? driver.country;
                    driver.region = model.region ?? driver.region;

                    if (model.hoursWorked > 0)
                    {
                        var driverWorkLog = m_Context.DriverWorkLog.FirstOrDefault(w => w.workDate == DateTime.Today);
                        if (driverWorkLog == null)
                        {
                            driverWorkLog = new DriverWorkLog();
                            driverWorkLog.driverId = driver.Id;
                            driverWorkLog.workDate = DateTime.Today;
                            driverWorkLog.hoursWorked = model.hoursWorked;
                            m_Context.DriverWorkLog.Add(driverWorkLog);
                        }
                        else
                            driverWorkLog.hoursWorked = model.hoursWorked;
                    }

                    m_Context.SaveChanges();
                    return Ok();
                }
                else
                    return NotFound("driverId not found");
            }
            else
                return BadRequest("Invalid driverId");
        }

        [Route("drivers/{driverId}/driverportaluser")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.HR)]
        public ActionResult<string> UpdateUser(string driverId, DriverChangeLoginModel model)
        {
            if (Guid.TryParse(driverId, out var guid))
            {
                var driver = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
                if (driver != null)
                {
                    driver.username = model.username;
                    driver.password = m_dataProtector.Protect(model.password);
                    driver.enabled = model.enabled;
                    m_Context.SaveChanges();
                    return Ok();
                }
                else
                    return NotFound("driverId not found");
            }
            else
                return BadRequest("Invalid driverId");
        }

        /// <summary>
        /// Returns specific event log information for passed in <paramref name="id"/>
        /// </summary>
        /// <param name="id">event log Identifier</param>
        /// <returns>Event log Data</returns>
        [Route("event_logs/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<LogEventModel> GetEventLogById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var data = m_Context.LogEvent.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new LogEventModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            else
                return BadRequest("Invalid id");
        }


        [Route("event_logs/feed")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.DriverFollow + "," + TelematicsRoles.DriverDispatch 
            + "," + TelematicsRoles.HR + "," + TelematicsRoles.Admin)]
        public ActionResult<LogEventFollow> FeedFollowLogEvent(string token)
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
                       
            var dsl = new LogEventFollow();
            dsl.token = m_dataProtector.Protect(toTime.ToString());
            var logs = m_Context.LogEvent
                                       .Include(l => l.location)
                                       .Include(a => a.annotations)
                                       .Where(x => x.dateTime >= fromTime && x.dateTime <= toTime)
                                       .OrderBy(c=>c.dateTime)
                                       .ToList();

            dsl.feed = LogEventsToLogEventModel(logs);
            return dsl;
        }

        private List<LogEventModel> LogEventsToLogEventModel(List<LogEvent> logs)
        {
            List<LogEventModel> logList = new List<LogEventModel>();
            foreach(LogEvent log in logs)
                logList.Add(new LogEventModel(log, m_appSettings.ProviderId));
            return logList;
        }

        private List<VehicleFlaggedEventModel> VehicleFlaggedEventsToVehicleFlaggedEventsModel(List<VehicleFlaggedEvent> events)
        {
            List<VehicleFlaggedEventModel> eventList = new List<VehicleFlaggedEventModel>();
            foreach (VehicleFlaggedEvent vfe in events)
                eventList.Add(new VehicleFlaggedEventModel(vfe, m_appSettings.ProviderId));
            return eventList;
        }

        private List<DriverBreakRuleModel> BreakRulesToBreakRulesModel(List<DriverBreakRule> breaks)
        {
            List<DriverBreakRuleModel> breakRuleModels = new List<DriverBreakRuleModel>();
            foreach (DriverBreakRule dbr in breaks)
                breakRuleModels.Add(new DriverBreakRuleModel(dbr, m_appSettings.ProviderId));
            return breakRuleModels;
        }

        private List<DriverWaiverModel> WaiversToWaiverModels(List<DriverWaiver> waivers)
        {
            List<DriverWaiverModel> waiverModels = new List<DriverWaiverModel>();
            foreach (DriverWaiver waiver in waivers)
                waiverModels.Add(new DriverWaiverModel(waiver, m_appSettings.ProviderId));
            return waiverModels;
        }

        [Route("drivers/{driverId}/performance_summaries")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.HR)]
        public ActionResult<DriverPerformanceSummaryListModel> DriverPerformanceSummaries(string driverId, string startTime, string stopTime)
        {

            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDateTime))
                return BadRequest("Invalid startTime");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDateTime))
                return BadRequest("Invalid stopTime");
            if (!Guid.TryParse(driverId, out var guid))
                return BadRequest("Invalid driverId");

            // make sure the driver exists
            var driverFound = m_Context.Driver.Any(c => c.Id == guid);
            if (!driverFound)
            {
                return NotFound("driverId Not Found");
            }

            var summaries = m_Context.DriverPerformanceSummary.Where
                (s => s.driverId == guid &&
                 s.eventStart >= startDateTime &&
                 s.eventStart <= stopDateTime)
                .ToList();

            DriverPerformanceSummaryListModel result = new DriverPerformanceSummaryListModel();
            result.performanceSummaries = DriverSummariesListToModel(summaries);
            return result;
        }

        private List<DriverPerformanceSummaryModel> DriverSummariesListToModel(List<DriverPerformanceSummary> summaries)
        {
            List<DriverPerformanceSummaryModel> summaryModel = new List<DriverPerformanceSummaryModel>();
            foreach (DriverPerformanceSummary summary in summaries)
                summaryModel.Add(new DriverPerformanceSummaryModel(summary, m_appSettings.ProviderId));
            return summaryModel;
        }


        [Route("driver_performance_summaries/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow + "," + TelematicsRoles.HR)]
        public ActionResult<DriverPerformanceSummaryModel> DriverPerformanceSummary(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                DriverPerformanceSummary data = m_Context.DriverPerformanceSummary.FirstOrDefault(c => c.Id == guid);
                if (data != null)
                    return new DriverPerformanceSummaryModel(data, m_appSettings.ProviderId);
                else
                    return NotFound("id Not Found");
            }
            return BadRequest("Invalid id");
        }
    }
}