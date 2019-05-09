using Microsoft.EntityFrameworkCore;
using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Telematics.DataSimulator
{
    public class Trip
    {
        string NORTH_SOUTH = "NS";
        string EAST_WEST = "EW";
        string FORWARD = "forward";
        string BACKWARD = "backward";

        Driver m_driver;
        Vehicle m_vehicle;
        List<SimulatedData_HwyDataPoints> m_route;
        TelematicsContext m_context;
        int m_heartbeat;

        public Trip(Driver driver, Vehicle vehicle, List<SimulatedData_HwyDataPoints> route, int heartbeat, string connString)
        {
            m_driver = driver;
            m_vehicle = vehicle;
            m_route = route;
            m_heartbeat = heartbeat;
            var options = new DbContextOptionsBuilder<TelematicsContext>()
                .UseSqlServer(connString)
                .Options;
            m_context = new TelematicsContext(options);
        }

        public void Go(int driverNumber)
        {
            //first, pause for a few seconds, to stagger the drivers starting
            Thread.Sleep(driverNumber * 7 * 1000);

            //Ok, let's go!
            while (!Program._cancelled)  
            {
                SortRoute(FORWARD);
                foreach (SimulatedData_HwyDataPoints point in m_route)
                {
                    if (Program._cancelled)
                        break;
                    AddDataPoint(point);
                    Thread.Sleep(m_heartbeat * 1000);
                    //await Task.Delay(m_heartbeat * 1000);

                }
                //if we reach the end, turnaround and head back
                SortRoute(BACKWARD);
                foreach (SimulatedData_HwyDataPoints point in m_route)
                {
                    if (Program._cancelled)
                        break;
                    AddDataPoint(point);
                    Thread.Sleep(m_heartbeat * 1000);
                    //await Task.Delay(m_heartbeat * 1000);
                }
            }
        }

        private void AddDataPoint(SimulatedData_HwyDataPoints point)
        {
            VehicleLocationTimeHistory loc = new VehicleLocationTimeHistory();
            loc.dateTime = DateTimeOffset.UtcNow;
            loc.driverId = m_driver.Id;
            loc.vehicleId = m_vehicle.Id;
            loc.latitude = point.Latitude;
            loc.longitude = point.Longitude;
            m_context.VehicleLocationTimeHistory.Add(loc);
            m_context.SaveChanges();
        }

        private void SortRoute(string routeDirection)
        {
            SimulatedData_HwyDataPoints point = m_route[0];
            if (point.Direction == NORTH_SOUTH)
                if (routeDirection == FORWARD)
                    m_route = m_route.OrderBy(x => x.Latitude).ToList();
                else  //GO BACK
                    m_route = m_route.OrderByDescending(x => x.Latitude).ToList();
            else //EAST_WEST
                if (routeDirection == FORWARD)
                    m_route = m_route.OrderBy(x => x.Longitude).ToList();
                else//GO BACK
                    m_route = m_route.OrderByDescending(x => x.Longitude).ToList();
        }


    }
}
