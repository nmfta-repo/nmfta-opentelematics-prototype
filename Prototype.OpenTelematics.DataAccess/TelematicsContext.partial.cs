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
        public enum ExportType
        {
            ExportVehicles,
            ExportStopGeographicDetails,
            ExportPerformanceThresholds,
            ExportVehicleLocationTimeHistory,
            ExportVehicleFlaggedEvents,
            ExportVehiclePerformanceEvents,
            ExportVehicleFaultCodeEvents,
            ExportStateOfHealth,
            ExportDrivers,
            ExportLogEvents,
            ExportRegionSpecificBreakRules,
            ExportSpecificWaivers,
            ExportDriverPerformanceSummary                
        }

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

        /// <summary>
        /// Receives data from a stored procedure and returns a string.
        /// It is expected that the ExportType corresponds to a stored procedure name,
        /// and that the stored proc returns the selected data in JSON format.
        /// This procedure will append together multiple rows, as the "FOR JSON"
        /// command in SQL SERVER may split the data across multiple rows!
        /// </summary>
        /// <param name="selectedDate">The day's data we are pulling</param>
        /// <param name="DataType">Enum ExportType - stored proc name</param>
        /// <returns></returns>
        public string GetJSONData(DateTime selectedDate, ExportType DataType)
        {
            var jsonResult = new StringBuilder();

            var selectedDateParameter = new SqlParameter("@SELECT_DATE", SqlDbType.Date);
            selectedDateParameter.Value = selectedDate.Date;
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Exec " + DataType.ToString() + " @SELECT_DATE";
                command.Parameters.Add(selectedDateParameter);
                
                Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        jsonResult.Append("[]");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            jsonResult.Append(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            return jsonResult.ToString();
        }

    }
}
