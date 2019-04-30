using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// args[0] = number of drivers to simulate
        /// args[1] = heartbeat frequency in seconds
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int NumberOfDrivers = Int32.Parse(args[0]);
            int HeartbeatSeconds = Int32.Parse(args[1]);

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

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            List<Driver> drivers = context.Driver.ToList();
            List<Vehicle> vehicles = context.Vehicle.ToList();
            List<string> routeNames = context.HwyDataPoints.Select(x => x.HwySectionName).Distinct().ToList();

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

            var backgroundTasks = new Task[NumberOfDrivers];
            for (int i = 0; i < NumberOfDrivers; i++)
            {
                Driver driver = drivers[i];
                Vehicle vehicle = vehicles[i];
                List<HwyDataPoint> route = context.HwyDataPoints.Where(x => x.HwySectionName == routeNames[i]).ToList();

                Trip trip = new Trip(driver, vehicle, route, HeartbeatSeconds, config.GetConnectionString("SecurityConnection"));
                backgroundTasks[i] = Task.Run(() => trip.Go());
            }
            Task.WaitAll(backgroundTasks, token);
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _cancelled = true;
                e.Cancel = true;
            }
        }
    }
}
