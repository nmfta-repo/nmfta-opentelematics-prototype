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
    [ApiController]
    public class DriverController : TelematicsBaseController
    {
        private readonly TelematicsContext m_Context;

        public DriverController(TelematicsContext context)
        {
            m_Context = context;
        }

        [Route("drivers")]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDuty)]
        public ActionResult<List<Driver>> AllDrivers()
        {
                var result = m_Context.Driver.ToList();
                return result;
        }

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

        [Route("driveravailability/{id}")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + TelematicsRoles.DriverFollow)]
        public ActionResult<DriverAvailability> Get(string id, DateTime start, DateTime stop)
        {
            var model = DriverAvailability.FromJson(System.IO.File.ReadAllText(@"DriverAvailability.json"));

            return model;
        }

        [Route("driveravailability/breakrulesandwaivers/{id}")]
        [HttpGet]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery + "," + 
                           TelematicsRoles.DriverFollow + "," + TelematicsRoles.HR)]
        public ActionResult<BreakRulesAndWaivers> GetBreakRulesAndWaivers(string id, DateTime start, DateTime stop)
        {
            var model = BreakRulesAndWaivers.FromJson(System.IO.File.ReadAllText(@"BreakRulesAndWaivers.json"));

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