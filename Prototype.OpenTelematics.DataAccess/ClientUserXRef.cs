using System;

namespace Prototype.OpenTelematics.DataAccess
{
    public class ClientUserXRef
    {
        public Guid ClientUserXRefId { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
    }
}