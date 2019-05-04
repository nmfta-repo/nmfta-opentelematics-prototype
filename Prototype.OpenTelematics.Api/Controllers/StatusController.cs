using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.DataAccess;
using Prototype.OpenTelematics.Models;

namespace Prototype.OpenTelematics.Api.Controllers
{
    /// <summary>
    /// Controller that process System Status queries
    /// </summary>
    [ApiController]
    [Authorize]
    public class StatusController : TelematicsBaseController
    {

        public StatusController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider) 
            : base(context, settings, provider)
        {
        }

        /// <summary>
        /// Returns Current System Status
        /// </summary>
        /// <returns>Service System Status</returns>
        [Route("status")]
        [HttpGet]
        public ActionResult<ProviderStatusModel> Get()
        {
            // get the current system status
            var incident = m_Context.CurrentServiceStatus.OrderByDescending(c => c.dateTime).FirstOrDefault();

            // assume everything is good
            if (incident == null)
            {
                return new ProviderStatusModel
                {
                    dateTime = DateTime.UtcNow,
                    serviceStatus = ServiceStatus.SERVICESTATUS_OPERATIONAL.ToString(),
                    factors = new[] { "authentication service operational", "downloads available" }
                };
            }

            // get the factors and return result
            var factors = m_Context.CurrentServiceStatusFactor.Where(c => c.eventId == incident.Id).Select(c=>c.Factor).ToArray();
            var model = new ProviderStatusModel
            {
                dateTime = incident.dateTime,
                serviceStatus = incident.Status,
                factors = factors
            };

            return model;
        }

        /// <summary>
        /// Returns incident report for the past <paramref name="days"/>
        /// </summary>
        /// <param name="days">Number of days</param>
        /// <returns>Incident Summary</returns>
        [Route("incidents")]
        [HttpGet]
        public ActionResult<IncidentsModel> Incidents(int days = 30)
        {
            // Validate input so the days is not too big or too small
            if (days > 30) days = 30;
            if (days < 1) days = 1;

            var forThePast = DateTime.Now.Date.AddDays(-days);
            var incidents = m_Context.ServiceStatusEvent.Where(c => c.dateTime >= forThePast).Select(c =>
                new ProviderStatusModel
                {
                    dateTime = c.dateTime,
                    serviceStatus = c.Status,
                    factors = m_Context.ServiceStatusEventFactor.Where(d => d.eventId == c.Id).Select(e => e.Factor)
                        .ToArray()
                });

            var incidentsModel = new IncidentsModel
            {
                data = incidents.ToList()
            };

            return incidentsModel;
        }


        /// <summary>
        /// Based on [LinguiJS formats](https://lingui.js.org/ref/catalog-formats.html); where the preferred format is gettext PO
        /// files, which are closely represented here.Unfortunately the Lingui JS raw format and JSON formats cannot be represented
        /// in API Blueprint's formal spec language.
        /// </summary>
        /// <returns>List of tokens and corresponding strings</returns>
        [Route("i18n")]
        [HttpGet]
        public ActionResult<TokenTranslationsModel> TokenTranslations()
        {
            var translations = m_Context.TokenTranslation.Select(t => t).ToList();
            var translationsModel = new TokenTranslationsModel
            {
                data = translations
            };
            return translationsModel;
        }

    }
}