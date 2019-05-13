using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Prototype.Telematics.DataSimulator
{
    public enum SystemStatus
    {
        SERVICESTATUS_DEGRADED_PERFORMANCE,
        SERVICESTATUS_MAJOR_OUTAGE,
        SERVICESTATUS_OPERATIONAL,
        SERVICESTATUS_PARTIAL_OUTAGE
    };

    public class StatusSimulator
    {
        private TelematicsContext m_context;
        private static Timer m_heartbeatTimer = new Timer(Program._StatusUpdateInterval);
        private int m_statusCounter;

        public StatusSimulator(TelematicsContext context)
        {
            m_context = context;
            m_statusCounter = 1;
        }

        public void Go()
        {
            CreateStatus(SystemStatus.SERVICESTATUS_OPERATIONAL);

            //Add one event at every heartbeat interval
            m_heartbeatTimer.Elapsed += AddAStatus;
            m_heartbeatTimer.AutoReset = true;
            m_heartbeatTimer.Enabled = true;
            while (!Program._cancelled) { }
        }

        public static void Stop(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling status updates......");
            m_heartbeatTimer.Stop();
            m_heartbeatTimer.Dispose();
        }

        private void AddAStatus(Object source, ElapsedEventArgs e)
        {
            int rem = 0;

            //if counter is divisible by 5, add a degraded performance status
            Math.DivRem(m_statusCounter, 3, out rem);
            if (rem == 0)
            {
                CreateStatus(SystemStatus.SERVICESTATUS_DEGRADED_PERFORMANCE);
            }
            else
            {
                //if counter is divisible by 7, add a partial outage status
                Math.DivRem(m_statusCounter, 5, out rem);
                if (rem == 0)
                    CreateStatus(SystemStatus.SERVICESTATUS_PARTIAL_OUTAGE);
                else
                    //otherwise, add an operational status
                    CreateStatus(SystemStatus.SERVICESTATUS_OPERATIONAL);
            }
            m_statusCounter++;
        }

        private void CreateStatus(SystemStatus status)
        {
            Console.WriteLine("Adding health status "+ status.ToString());

            ServiceStatusEvent sse = new ServiceStatusEvent
            {
                Status = status.ToString(),
                dateTime = DateTimeOffset.Now
            };
            m_context.ServiceStatusEvent.Add(sse);
            m_context.SaveChanges();
        }
    }
}
