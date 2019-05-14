using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.Api.Security;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;

namespace Prototype.OpenTelematics.Api.Controllers
{
    [ApiController]
    public class ExportController : TelematicsBaseController
    {
        private readonly IHostingEnvironment m_Host;

        public ExportController(TelematicsContext context, IOptions<AppSettings> settings,
            IDataProtectionProvider provider, IHostingEnvironment host)
            : base(context, settings, provider)
        {
            m_Host = host;
        }

        [Route("export/allrecords/status")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery)]
        [Authorize]
        public ActionResult<ExportModel> AllRecords(DateTime dayOf)
        {
            var export = m_Context.Export.FirstOrDefault(c => c.export_date == dayOf.Date && c.export_type == "Full");

            if (export == null)
                return StatusCode(202);

            var result = new ExportModel
            {
                location = m_appSettings.ApiBase + $"/download/{export.Id}"
            };

            return result;
        }

        [Route("export/vehiclerecords/status")]
        [HttpGet]
        [Authorize(Roles =
            TelematicsRoles.Admin + "," + TelematicsRoles.DriverQuery)]
        [Authorize]
        public ActionResult<ExportModel> AllVehicles(DateTime dayOf)
        {
            var export = m_Context.Export.FirstOrDefault(c => c.export_date == dayOf.Date && c.export_type == "Vehicle");

            if (export == null)
                return StatusCode(202);

            var result = new ExportModel
            {
                location = m_appSettings.ApiBase + $"/download/{export.Id}"
            };

            return result;
        }


        [Route("download/{id}")]
        [HttpGet]
        public ActionResult Download(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return NotFound("Invalid id");
            }

            var result = m_Context.Export.FirstOrDefault(c => c.Id == guid);
            if (result == null)
                return NotFound("Invalid id");

            var filePhysicalLocation = Path.Combine(m_appSettings.ExportFileLocation, result.location);
            if (!System.IO.File.Exists(filePhysicalLocation))
                return NotFound("Invalid id");

            var stream = new FileStream(filePhysicalLocation, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = result.location
            };
        }

    }
}