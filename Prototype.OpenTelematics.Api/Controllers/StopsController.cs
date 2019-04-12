using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StopsController : TelematicsBaseController
    {

        private readonly TelematicsContext m_Context;

        public StopsController(TelematicsContext context)
        {
            m_Context = context;
        }

        [Route("api/stops/{stopId}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverDispatch )]
        public ActionResult<StopGeographicDetails> Get(string stopId)
        {
            if (Guid.TryParse(stopId, out var guid))
            {
                var result = m_Context.StopGeographicDetails.FirstOrDefault(c => c.Id == guid);

                return result;
            }

            return NotFound("Invalid id");
        }
    }
}