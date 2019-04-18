using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api.Controllers
{
    public class TelematicsBaseController : ControllerBase
    {
        protected readonly TelematicsContext m_Context;
        protected readonly AppSettings m_appSettings;
        protected readonly IDataProtector m_dataProtector;

        public TelematicsBaseController(TelematicsContext context, IOptions<AppSettings> settings, IDataProtectionProvider provider)
        {
            m_Context = context;
            m_appSettings = settings.Value;

            string providerId = m_appSettings.ProviderId ?? "otapidemo.nmfta.org";
            m_dataProtector = provider.CreateProtector(providerId);
        }
    }
}