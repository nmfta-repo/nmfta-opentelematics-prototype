﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Telematics.DataSimulator
{
    class Program
    {
        public static bool _cancelled;
        public static int _StatusUpdateInterval;

        /// <summary>
        /// args[0] = number of drivers to simulate
        /// args[1] = seconds between adding a location history for a vehicle, in seconds
        /// args[2] = seconds between system status health updates
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int NumberOfDrivers = Int32.Parse(args[0]);
            int HeartbeatSeconds = Int32.Parse(args[1]);
            _StatusUpdateInterval = Int32.Parse(args[2]) * 1000;

            //setup
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            var options = new DbContextOptionsBuilder<TelematicsContext>()
                .UseSqlServer(config.GetConnectionString("SecurityConnection"))
                .Options;
            var context = new TelematicsContext(options);

            //handle Ctrl+C to cancel the simulation
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            //setup the system health status simulator
            StatusSimulator statusSim = new StatusSimulator(context);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(StatusSimulator.Stop);

            //get the drivers, vechicles and routes
            List<Driver> drivers = context.Driver.ToList();
            List<Vehicle> vehicles = context.Vehicle.ToList();
            List<string> routeNames = context.SimulatedData_HwyDataPoints.Select(x => x.HwySectionName).Distinct().ToList();

            if ((NumberOfDrivers > drivers.Count) 
                || (NumberOfDrivers > vehicles.Count)
                || (NumberOfDrivers > routeNames.Count))
            {
                Console.WriteLine("Too many drivers!");
                Console.ReadKey();
                return;
            }
            if (NumberOfDrivers < 1)
            {
                Console.WriteLine("Need at least one driver!");
                Console.ReadKey();
                return;
            }

            //start vehicle location simulation
            var backgroundTasks = new Task[NumberOfDrivers + 1];
            for (int i = 0; i < NumberOfDrivers; i++)
            {
                Driver driver = drivers[i];
                Vehicle vehicle = vehicles[i];
                List<SimulatedData_HwyDataPoints> route = context.SimulatedData_HwyDataPoints.Where(x => x.HwySectionName == routeNames[i]).ToList();    
                Trip trip = new Trip(driver, vehicle, route, HeartbeatSeconds, config.GetConnectionString("SecurityConnection"));
                Console.WriteLine(string.Format("Adding driver {0} and vehicle {1} to route {2}", driver.username, vehicle.name, routeNames[i]));

                backgroundTasks[i] = Task.Run(() => trip.Go(i));
            }
            backgroundTasks[NumberOfDrivers] = Task.Run(() => statusSim.Go());
            Console.WriteLine("Press Ctrl+C to stop.");
            Task.WaitAll(backgroundTasks, token);
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling vehicle location updates.....");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _cancelled = true;
                e.Cancel = true;
            }
        }
    }
}
