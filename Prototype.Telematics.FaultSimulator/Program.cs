using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype.Telematics.FaultSimulator
{
    class Program
    {
        public static bool _cancelled;
        public static int _heartbeatInterval;

        /// <summary>
        /// args[0] = heartbeat frequency in seconds, 
        ///           after startup 1 fault code is added per heartbeat
        /// args[1] = repeat faults?  'true' or 'false'
        ///           once we run out of simulated data, should we start over?
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _heartbeatInterval = Int32.Parse(args[0]) * 1000;
            bool bKeepGoing = bool.Parse(args[1]);

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

            FaultSimulator sim = new FaultSimulator(context, bKeepGoing);

            //handle Ctrl+C to cancel the simulation
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(FaultSimulator.Stop);
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            sim.Go();
        }


        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling......");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _cancelled = true;
                e.Cancel = true;
            }
        }
    }
}
