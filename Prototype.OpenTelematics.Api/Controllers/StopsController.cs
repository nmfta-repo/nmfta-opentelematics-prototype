using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;

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

        [Route("stops/{stopId}")]
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

        /// <summary>
        /// Save Stop 
        /// </summary>
        /// <param name="vehicleId">Vehicle Identifier</param>
        /// <param name="routeId">Route Identifier</param>
        /// <param name="postedModel">Stop Model</param>
        /// <returns>Put Status</returns>
        [Route("vehicles/{vehicleId}/routes/{routeId}")]
        [HttpPut]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDispatch)]
        public IActionResult PutVehicleStop(string vehicleId, string routeId, PutStopModel postedModel)
        {
            if (postedModel == null)
            {
                return StatusCode(204);
            }

            if (!Guid.TryParse(vehicleId, out var vehicleGuidId))
            {
                return NotFound("Invalid Vehicle id");
            }

            if (!Guid.TryParse(routeId, out var routeGuidId))
            {
                return NotFound("Invalid Route id");
            }

            // extract latitude and longitude
            var coordinates = Helper.TryParseLocation(postedModel.stopLocation);
            if (!string.IsNullOrWhiteSpace(postedModel.stopLocation) && (coordinates.latitude == null || coordinates.longitude == null))
            {
                return BadRequest("Invalid Stop Location");
            }

            // create the instance
            var driverStopLog = new StopGeographicDetails
            {
                Id = Guid.NewGuid(),
                routeId = routeGuidId,
                address = postedModel.stopAddress,
                stopName = postedModel.stopName,
                latitude = coordinates.latitude,
                longitude = coordinates.longitude,
                stopDeadline = postedModel.stopDeadline
            };

            var vehicleStopXRef = new VehicleStopXRef
            {
                Id = Guid.NewGuid(),
                vehicleId = vehicleGuidId,
                stopId = driverStopLog.Id,
                createdOn = DateTimeOffset.Now
            };

            // add and save
            m_Context.StopGeographicDetails.Add(driverStopLog);
            m_Context.VehicleStopXRef.Add(vehicleStopXRef);
            m_Context.SaveChanges();

            // return result
            return new OkObjectResult(new { driverStopLog.routeId, stopId = driverStopLog.Id });
        }

        /// <summary>
        /// Save Driver Stop Log
        /// </summary>
        /// <param name="stopId">Stop Identifier</param>
        /// <param name="postedModel">Stop Model</param>
        /// <returns>Patch Status</returns>
        [Route("stops/{stopId}")]
        [HttpPatch]
        [Authorize(Roles = TelematicsRoles.Admin + "," + TelematicsRoles.DriverDispatch)]
        public IActionResult PatchStop(string stopId, PatchStopModel postedModel)
        {
            if (postedModel == null)
            {
                return StatusCode(204);
            }

            if (!Guid.TryParse(stopId, out var stopGuidId))
            {
                return NotFound("Invalid Stop id");
            }

            var stopGeographicDetails = m_Context.StopGeographicDetails.FirstOrDefault(c => c.Id == stopGuidId);
            if (stopGeographicDetails == null)
            {
                return NotFound("Invalid Stop id");
            }

            if (string.IsNullOrWhiteSpace(postedModel.location))
            {
                return BadRequest("Invalid Stop Location");
            }

            // extract latitude and longitude
            var coordinates = Helper.TryParseLocation(postedModel.location);
            if (coordinates.latitude == null || coordinates.longitude == null)
            {
                return BadRequest("Invalid Stop Location");
            }

            stopGeographicDetails.latitude = coordinates.latitude;
            stopGeographicDetails.longitude = coordinates.longitude;
            stopGeographicDetails.comment = postedModel.comment;

            if (postedModel.entryArea != null)
            {
                stopGeographicDetails.entryArea = JsonConvert.SerializeObject(postedModel.entryArea);
            }

            // add and save
            m_Context.SaveChanges();

            // return result
            return new OkObjectResult(new { stopId = stopGuidId });
        }
    }
}