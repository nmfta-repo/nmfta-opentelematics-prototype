using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Prototype.Telematics.LogEventSimulator
{
    public class LogEventSimulator
    {
        private TelematicsContext m_context;
        private List<Vehicle> m_vehicles;
        private List<Driver> m_drivers;
        private List<SimulatedData_LogEvent> m_logEvents;
        private int m_eventIdx;
        private static Timer heartbeatTimer = new Timer(Program._heartbeatInterval);
        private bool m_keepGoing;
        private int m_maxTrucks;

        public LogEventSimulator(TelematicsContext context, bool bKeepGoing)
        {
            m_context = context;
            m_vehicles = m_context.Vehicle.ToList();
            m_drivers = m_context.Driver.ToList();
            m_logEvents = m_context.SimulatedData_LogEvent.OrderBy(x => x.createdOrder).ToList();
            m_eventIdx = 0;
            m_keepGoing = bKeepGoing;

            //the driverId will be used to index into the vehicle 
            //and driver lists, so we need to find out how many are available
            m_maxTrucks = m_logEvents.Max(x => x.driverId);
            m_maxTrucks = Math.Min(m_maxTrucks, m_drivers.Count);
            m_maxTrucks = Math.Min(m_maxTrucks, m_vehicles.Count);
        }

        public void Go()
        {
            m_logEvents = m_context.SimulatedData_LogEvent.OrderBy(x => x.createdOrder).ToList();

            //Add 5 events to start
            int max = Math.Min(5, m_logEvents.Count);
            for (int idx = 0; idx < max; idx++)
            {
                AddALogEvent(this, null);
            }
            Console.WriteLine("Press Ctrl+C to stop.");

            //Add one event at every heartbeat interval
            heartbeatTimer.Elapsed += AddALogEvent;
            heartbeatTimer.AutoReset = true;
            heartbeatTimer.Enabled = true;
            while (!Program._cancelled) { }
        }

        private SimulatedData_LogEvent GetNextLogEvent()
        {
            if (m_eventIdx >= m_logEvents.Count)
                if (m_keepGoing)
                    m_eventIdx = 0;
                else
                    return null;
            SimulatedData_LogEvent log = m_logEvents[m_eventIdx];
            m_eventIdx++;
            return log;
        }

        private void AddALogEvent(Object source, ElapsedEventArgs e)
        {
            SimulatedData_LogEvent sim_le = GetNextLogEvent();
            if (sim_le != null)
            {
                int driverIndex = sim_le.driverId;
                //just skip this one if the driver ID is > the number of 
                //drivers or vehicles we have available
                if (driverIndex > m_maxTrucks)
                    return;
                Driver driver = m_drivers[driverIndex];
                Vehicle vehicle = m_vehicles[driverIndex];

                CreateLogEvent(sim_le, vehicle, driver);
            }
        }

        private void CreateLogEvent(SimulatedData_LogEvent sim_le, Vehicle vehicle, Driver driver)
        {
            Console.WriteLine("Adding log event to driver " + driver.Id.ToString() + ": event Id: " + sim_le.Id.ToString());

            //create a log event based on our simulated data store
            LogEvent le = new LogEvent
            {
                Id = Guid.NewGuid(),
                driverId = driver.Id,
                vehicleId = vehicle.Id,
                locationId = sim_le.locationId,
                certificationCount = sim_le.certificationCount,
                coDrivers = sim_le.coDrivers,
                comment = sim_le.comment,
                dateTime = DateTimeOffset.Now,
                distanceLastValid = sim_le.distanceLastValid,
                editDateTime = sim_le.editDateTime,
                eventDataChecksum = sim_le.eventDataChecksum,
                eventType = sim_le.eventType,
                multidayBasis = sim_le.multidayBasis,
                origin = sim_le.origin,
                parentId = sim_le.parentId,
                sequence = sim_le.sequence,
                state = sim_le.state,
                verifyDateTime = sim_le.verifyDateTime,
            };
            m_context.LogEvent.Add(le);

            //add annotations, if available
            List<SimulatedData_LogEventAnnotation> annotations = m_context.SimulatedData_LogEventAnnotation.Where(x => x.logEventId == sim_le.Id).ToList();
            foreach (SimulatedData_LogEventAnnotation annotation in annotations)
            {
                LogEventAnnotation lea = new LogEventAnnotation
                {
                    comment = annotation.comment,
                    dateTime = DateTimeOffset.Now,
                    driverId = le.driverId,
                    Id = Guid.NewGuid(),
                    logEventId = le.Id
                };
                m_context.LogEventAnnotation.Add(lea);
            }
            m_context.SaveChanges();
        }

        public static void Stop(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling......");
            heartbeatTimer.Stop();
            heartbeatTimer.Dispose();
        }


    }
}
