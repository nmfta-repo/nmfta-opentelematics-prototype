using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;
using System;
using System.Linq;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    public class VehicleController : TelematicsBaseController
    {
        private readonly TelematicsContext m_Context;

        public VehicleController(TelematicsContext context)
        {
            m_Context = context;
        }

        [Route("byid/vehicles/{id}")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.VehicleQuery + "," + TelematicsRoles.VehicleFollow)]
        public ActionResult<Vehicle> Get(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var result = m_Context.Vehicle.FirstOrDefault(c => c.Id == guid);
                return result;
            }

            return NotFound("Invalid id");
        }
    }
}