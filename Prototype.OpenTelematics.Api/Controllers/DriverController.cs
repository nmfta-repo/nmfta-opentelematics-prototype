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
        [Route("byid/drivers/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<Driver> Get(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var result = m_Context.Driver.FirstOrDefault(c => c.Id == guid);
                return result;
            }

            return NotFound("Invalid id");
        }

        /// <summary>
        /// Returns all Driver Break Rules
        /// <para>TODO:K Support Paging and Time Constraints</para>
        /// </summary>
        /// <returns>Driver Break Rules</returns>
        [Route("regionspecificbreaksrules")]
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
        [Route("byid/regionspecificbreaksrules/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverBreakRule> GetBreakRuleById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var result = m_Context.DriverBreakRule.FirstOrDefault(c => c.Id == guid);
                return result;
            }

            return NotFound("Invalid id");
        }

        /// <summary>
        /// Returns all Driver Waivers
        /// <para>TODO:K Support Paging and Time Constraints</para>
        /// </summary>
        /// <returns>Region Specific Waivers</returns>
        [Route("regionspecificwaivers")]
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
        [Route("byid/regionspecificwaivers/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverWaiver> GetWaiverById(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var result = m_Context.DriverWaiver.FirstOrDefault(c => c.Id == guid);
                return result;
            }

            return NotFound("Invalid id");
        }

        /// <summary>
        /// Returns Driver Availability factors for passed in driver <paramref name="id"/>, start date and stop time
        /// </summary>
        /// <param name="id">Driver Identifier</param>
        /// <param name="start">Start Date</param>
        /// <param name="stop">Stop Date</param>
        /// <returns>Driver Availability Factors</returns>
        [Route("driveravailability/{id}")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverAvailability> Get(string id, DateTime startTime, DateTime stopTime)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return NotFound("Invalid id");
            }

            // TODO:K Find How Time Resolution is set
            var courseLocationHistory =
                m_Context.CoarseVehicleLocationTimeHistory.Where(c => c.driverId == guid && c.dateTime >= startTime && c.dateTime <= stopTime);
            var dutyStatusLogs = m_Context.DutyStatusLog.Where(c => c.driverId == guid && c.dateTime >= startTime && c.dateTime <= stopTime);
            var vehicleFlaggedEvents =
                m_Context.VehicleFlaggedEvent.Where(c => c.driverId == guid && c.eventStart >= startTime && c.eventEnd <= stopTime);
            var model = new DriverAvailability
            {
                CoarseVehicleLocationTimeHistory = new CoarseVehicleLocationTimeHistoryModel
                {
                    TimeResolution = "TIMERESOLUTION_NOT_MAX",
                    VehicleLocationTimeHistories = courseLocationHistory.ToArray()
                },
                DutyStatusLogs = dutyStatusLogs.ToArray(),
                VehicleFlaggedEvents = vehicleFlaggedEvents.ToArray()
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
        [Route("driveravailability/breakrulesandwaivers/{id}")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + 
                           TelematicsRoles.DriverFollow + "," + TelematicsRoles.HR)]
        public ActionResult<BreakRulesAndWaivers> GetBreakRulesAndWaivers(string id, DateTime startTime, DateTime stopTime)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return NotFound("Invalid id");
            }

            var driverBreakRules = m_Context.DriverBreakRule.Where(c => c.driverId == guid && c.activeFrom >= startTime && c.activeTo <= stopTime);
            var driverWaivers = m_Context.DriverWaiver.Where(c => c.driverId == guid && c.waiverDay >= startTime && c.waiverDay <= stopTime);
            var model = new BreakRulesAndWaivers
            {
                BreakRules = driverBreakRules.ToArray(),
                Waivers = driverWaivers.ToArray()
            };

            return model;
        }

        /// <summary>
        /// Update Duty Status Change
        /// <para>TODO:K Validate Incoming Data and Save to back-end</para>
        /// </summary>
        /// <param name="id">Driver Identifier</param>
        /// <param name="postedModel">Updated Duty Status Change</param>
        /// <returns>Patch Status</returns>
        [Route("driveravailability/dutystatuschanges/{id}")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<string> PatchDutyStatusChange(string id, DutyStatusChangeModel postedModel)
        {
            System.IO.File.WriteAllText(@"DutyStatusChange_REQ.json",postedModel.ToJson());
            return string.Empty;
        }

        [Route("api/drivers/{driverId}")]
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
                return NotFound("Invalid driverId");
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


        [Route("api/event_logs/feed")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.DriverFollow + "," + TelematicsRoles.DriverDispatch 
            + "," + TelematicsRoles.HR + "," + TelematicsRoles.Admin)]
        public ActionResult<DutyStatusLogFollow> FeedFollowDutyStatus(string token)
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
                       
            var dsl = new DutyStatusLogFollow();
            dsl.token = m_dataProtector.Protect(toTime.ToString());
            var logs = m_Context.DutyStatusLog
                                       .Include(l => l.location)
                                       .Include(a => a.annotations)
                                       .Where(x => x.dateTime >= fromTime && x.dateTime <= toTime)
                                       .ToList();

            dsl.feed = DutyStatusLogToFollowModel(logs);
            return dsl;
        }

        private List<DutyStatusLogModel> DutyStatusLogToFollowModel(List<DutyStatusLog> logs)
        {
            List<DutyStatusLogModel> logList = new List<DutyStatusLogModel>();
            foreach(DutyStatusLog log in logs)
                logList.Add(new DutyStatusLogModel(log, m_appSettings.ProviderId));
            return logList;
        }

        [Route("drivers/{driverId}/performance_summaries")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.HR)]
        public ActionResult<DriverPerformanceSummaryModel> DriverPerformanceSummaries(string driverId, string startTime, string stopTime)
        {

            if (!DateTime.TryParse(startTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime startDateTime))
                return BadRequest("Invalid start time");
            if (!DateTime.TryParse(stopTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime stopDateTime))
                return BadRequest("Invalid stop time");
            if (!Guid.TryParse(driverId, out var guid))
                return BadRequest("Invalid driver id");

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

            DriverPerformanceSummaryModel result = new DriverPerformanceSummaryModel();
            result.performanceSummaries = summaries;
            return result;
        }


        [Route("driver_performance_summaries/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverPerformanceSummary> DriverPerformanceSummary(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                DriverPerformanceSummary result = m_Context.DriverPerformanceSummary.FirstOrDefault(c => c.Id == guid);
                if (result != null)
                    return result;
                else
                    return NotFound("Invalid id");
            }
            return NotFound("Invalid id");
        }
    }
}