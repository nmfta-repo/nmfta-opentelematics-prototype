using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class StopsController : TelematicsBaseController
    {

        public StopsController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider) 
            : base(context, settings, provider)
        {

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