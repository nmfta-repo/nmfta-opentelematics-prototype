﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly TelematicsContext m_Context;

        public StatusController(TelematicsContext context)
        {
            m_Context = context;
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
    }
}