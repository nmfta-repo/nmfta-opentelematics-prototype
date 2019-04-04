using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.OpenTelematics.Models;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StatusController : TelematicsBaseController
    {
        [Route("status")]
        [HttpGet]
        public ActionResult<ProviderStatusModel> Get()
        {
            var model = new ProviderStatusModel
            {
                dateTime = DateTime.UtcNow,
                serviceStatus = ServiceStatus.SERVICESTATUS_OPERATIONAL,
                factors = new[] {"authentication service operational", "downloads available"}
            };

            return model;
        }

        [Route("incidents")]
        [HttpGet]
        public ActionResult<IncidentsModel> Incidents()
        {
            var result = new List<ProviderStatusModel>
            {
                new ProviderStatusModel
                {
                    dateTime = DateTime.Now.AddDays(-4),
                    serviceStatus = ServiceStatus.SERVICESTATUS_MAJOR_OUTAGE,
                    factors = new[] {"authentication service down"}
                },
                new ProviderStatusModel
                {
                    dateTime = DateTime.Now.AddDays(-8),
                    serviceStatus = ServiceStatus.SERVICESTATUS_DEGRADED_PERFORMANCE,
                    factors = new[] {"authentication service slow"}
                },
                new ProviderStatusModel
                {
                    dateTime = DateTime.Now.AddDays(-9),
                    serviceStatus = ServiceStatus.SERVICESTATUS_DEGRADED_PERFORMANCE,
                    factors = new[] {"downloads service spotty"}
                },
            };

            var incidentsModel = new IncidentsModel
            {
                data = result
            };

            return incidentsModel;
        }

    }
}