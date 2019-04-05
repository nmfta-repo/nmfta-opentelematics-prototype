using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly TelematicsContext m_Context;

        public DriverController(TelematicsContext context)
        {
            m_Context = context;
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
        public ActionResult<DriverAvailability> Get(string id, DateTime start, DateTime stop)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return NotFound("Invalid id");
            }

            var courseLocationHistory =
                m_Context.CoarseVehicleLocationTimeHistory.Where(c => c.driverId == guid && c.dateTime >= start && c.dateTime <= stop);
            var dutyStatusLogs = m_Context.DutyStatusLog.Where(c => c.driverId == guid && c.dateTime >= start && c.dateTime <= stop);
            var vehicleFlaggedEvents =
                m_Context.VehicleFlaggedEvent.Where(c => c.driverId == guid && c.eventStart >= start && c.eventEnd <= stop);
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
        public ActionResult<BreakRulesAndWaivers> GetBreakRulesAndWaivers(string id, DateTime start, DateTime stop)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return NotFound("Invalid id");
            }

            var driverBreakRules = m_Context.DriverBreakRule.Where(c => c.driverId == guid && c.activeFrom >= start && c.activeTo <= stop);
            var driverWaivers = m_Context.DriverWaiver.Where(c => c.driverId == guid && c.waiverDay >= start && c.waiverDay <= stop);
            var model = new BreakRulesAndWaivers
            {
                BreakRules = driverBreakRules.ToArray(),
                Waivers = driverWaivers.ToArray()
            };

            return model;
        }

        [Route("driveravailability/dutystatuschanges/{id}")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<string> PatchDutyStatusChange(string id, DutyStatusChangeModel postedModel)
        {
            System.IO.File.WriteAllText(@"DutyStatusChange_REQ.json",postedModel.ToJson());
            return string.Empty;
        }
    }
}