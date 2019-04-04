using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class TelematicsContext
    {
        public DbQuery<Client> Clients { get; set; }

        public void AddUserToClient(string clientId, string userId)
        {
            var clientIdParameter = new SqlParameter("@ClientId", SqlDbType.NVarChar, 450);
            clientIdParameter.Value = clientId;
            var userIdParameter = new SqlParameter("@UserId", SqlDbType.NVarChar, 450);
            userIdParameter.Value = userId;
            this.Database.ExecuteSqlCommand("Exec AddUserToClient @ClientId, @UserId", clientIdParameter, userIdParameter);
        }

        public Client GetClient(string userId)
        {
            var userIdParameter = new SqlParameter("@UserId", SqlDbType.NVarChar, 450);
            userIdParameter.Value = userId;
            //this.Database.
            this.Database.ExecuteSqlCommand("Exec GetClientForUser @UserId", userIdParameter);
            var result = this.Clients.FromSql("Exec GetClientForUser @UserId", userIdParameter);
            return result.FirstOrDefault();
        }

    }
}
