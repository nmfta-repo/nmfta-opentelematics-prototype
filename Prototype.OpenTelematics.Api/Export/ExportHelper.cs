using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Api
{
    /// <summary>
    /// A stand-alone class that contains it's own DB context.
    /// Created for the purpose of generating an export file
    /// on a background thread, while the API controller can 
    /// return to the client.
    /// </summary>
    public class ExportHelper 
    {
        protected readonly TelematicsContext m_Context;

        public ExportHelper(string connString)
        {
            var options = new DbContextOptionsBuilder<TelematicsContext>()
                .UseSqlServer(connString)
                .Options;
            m_Context = new TelematicsContext(options);
        }

        /// <summary>
        /// Exports the Vehicle-related data in JSON form. Can be called
        /// with Task.Run()
        /// </summary>
        /// <param name="selectedDate">The day's data to export</param>
        /// <param name="filepath">directory path</param>
        /// <param name="fileName">file name</param>
        public void ExportVehicleData(DateTime selectedDate, string filepath, string fileName)
        {
            CreateVehicleDataExportFile(selectedDate, filepath, fileName);
            UpdateExportTable(selectedDate, filepath, fileName, "Vehicle");
        }

        /// <summary>
        /// Exports the Vehicle-related data in JSON form. Can be called
        /// with Task.Run()        
        /// </summary>
        /// <param name="selectedDate">The day's data to export</param>
        /// <param name="filepath">directory path</param>
        /// <param name="fileName">file name</param>
        public void ExportAllData(DateTime selectedDate, string filepath, string fileName)
        {
            CreateAllDataExportFile(selectedDate, filepath, fileName);
            UpdateExportTable(selectedDate, filepath, fileName, "Full");
        }
        

        private void CreateVehicleDataExportFile(DateTime selectedDate, string filepath, string fileName)
        {

            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            StreamWriter writer = new StreamWriter(filepath + fileName, false);
            writer.Write("{ ");

            string data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportStopGeographicDetails);
            writeList(data, "stopGeographicDetails", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportPerformanceThresholds);
            writeList(data, "performanceThresholds", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicles);
            writeList(data, "vehicles", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleLocationTimeHistory);
            writeList(data, "vehicleLocationTimeHistory", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleFlaggedEvents);
            writeList(data, "vehicleFlaggedEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehiclePerformanceEvents);
            writeList(data, "vehiclePerformanceEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleFaultCodeEvents);
            writeList(data, "vehicleFaultCodeEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportStateOfHealth);
            writeList(data, "stateOfHealth", writer, true);

            writer.Write(" }");
            writer.Flush();
            writer.Close();
        }


        private void CreateAllDataExportFile(DateTime selectedDate, string filepath, string fileName)
        {
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            StreamWriter writer = new StreamWriter(filepath + fileName, false);
            writer.Write("{ ");

            string data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicles);
            writeList(data, "vehicles", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleLocationTimeHistory);
            writeList(data, "vehicleLocationTimeHistory", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleFlaggedEvents);
            writeList(data, "vehicleFlaggedEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehiclePerformanceEvents);
            writeList(data, "vehiclePerformanceEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportStopGeographicDetails);
            writeList(data, "stopGeographicDetails", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportVehicleFaultCodeEvents);
            writeList(data, "vehicleFaultCodeEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportDrivers);
            writeList(data, "drivers", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportLogEvents);
            writeList(data, "logEvents", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportRegionSpecificBreakRules);
            writeList(data, "regionSpecificBreakRules", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportSpecificWaivers);
            writeList(data, "regionSpecificWaivers", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportDriverPerformanceSummary);
            writeList(data, "driverPerformanceSummaries", writer, false);

            data = m_Context.GetJSONData(selectedDate, TelematicsContext.ExportType.ExportStateOfHealth);
            writeList(data, "stateOfHealth", writer, true);

            writer.Write(" }");
            writer.Flush();
            writer.Close();
        }

        private static void writeList(string itemList, string tag, StreamWriter writer, bool lastOne)
        {
            writer.Write("\"" + tag + "\": ");
            if (!string.IsNullOrEmpty(itemList))
                writer.Write(itemList);
            else
                writer.Write("[]");
            if (!lastOne)
                writer.Write(",");
        }


        private void UpdateExportTable(DateTime selectedDate, string filepath, string fileName, string exportType)
        {
            var export = m_Context.Export.FirstOrDefault(c => c.export_date == selectedDate.Date && c.export_type == exportType);
            if (export == null)
            {
                Export exportFile = new Export
                {
                    export_date = selectedDate.Date,
                    export_type = exportType,
                    location = fileName
                };
                m_Context.Export.Add(exportFile);
                m_Context.SaveChanges();
            }
            else
            {
                export.location = fileName;
                m_Context.SaveChanges();
            }
        }

    }
}
