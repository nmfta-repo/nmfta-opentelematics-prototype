using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Prototype.OpenTelematics.DataAccess;


namespace Prototype.Telematics.FaultSimulator
{
    public class FaultSimulator
    {
        private TelematicsContext m_context;
        private List<Vehicle> m_trucks;
        private List<SimulatedData_FaultEvent> m_faultEvents;
        private int m_vehicleIdx;
        private int m_eventIdx;
        private static Timer heartbeatTimer = new Timer(Program._heartbeatInterval);
        private bool m_keepGoing;

        public FaultSimulator(TelematicsContext context, bool bKeepGoing)
        {
            m_context = context;
            m_trucks = m_context.Vehicle.ToList();
            m_faultEvents = m_context.SimulatedData_FaultEvents.OrderBy(x => x.createOrder).ToList();
            m_vehicleIdx = 0;
            m_eventIdx = 0;
            m_keepGoing = bKeepGoing;

            if (m_trucks.Count < 1)
            {
                Console.WriteLine("Need at least one vehicle!");
                Console.ReadKey();
                return;
            }
            if (m_faultEvents.Count < 1)
            {
                Console.WriteLine("Need at least one fault event!");
                Console.ReadKey();
                return;
            }
        }

        public void Go()
        {
            List<SimulatedData_FaultEvent> m_faultEvents = m_context.SimulatedData_FaultEvents.OrderBy(x => x.createOrder).ToList();

            //Add 5 events to start
            int max = Math.Min(5, m_faultEvents.Count);
            for (int idx = 0; idx < max; idx++)
            {
                AddAFault(this, null);
            }
            Console.WriteLine("Press Ctrl+C to stop.");

            //Add one event at every heartbeat interval
            heartbeatTimer.Elapsed += AddAFault;
            heartbeatTimer.AutoReset = true;
            heartbeatTimer.Enabled = true;
            while (!Program._cancelled) { }
        }

        private void AddAFault(Object source, ElapsedEventArgs e)
        {
            Vehicle vehicle = GetNextVehicle();
            SimulatedData_FaultEvent fault = GetNextFaultEvent();
            if (fault != null)
                createFault(vehicle, fault);
        }

        private Vehicle GetNextVehicle()
        {            
            if (m_vehicleIdx >= m_trucks.Count)
                m_vehicleIdx = 0;
            Vehicle vehicle = m_trucks[m_vehicleIdx];
            m_vehicleIdx++;
            return vehicle;
        }

        private SimulatedData_FaultEvent GetNextFaultEvent()
        {
            if (m_eventIdx >= m_faultEvents.Count)
                if (m_keepGoing)
                    m_eventIdx = 0;
                else
                    return null;
            SimulatedData_FaultEvent fault = m_faultEvents[m_eventIdx];
            m_eventIdx++;
            return fault;
        }

        public static void Stop(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling......");
            heartbeatTimer.Stop();
            heartbeatTimer.Dispose();
        }

        private void createFault(Vehicle vehicle, SimulatedData_FaultEvent fault)
        {
            Console.WriteLine("Adding fault code event to vehicle " + vehicle.Id.ToString() + ": event Id: " + fault.Id.ToString());
            VehicleFaultCodeEvent vfce = new VehicleFaultCodeEvent
            {
                vehicleId = vehicle.Id,
                longitude = fault.longitude,
                latitude = fault.latitude,
                eventComment = fault.eventComment,
                triggerDate = DateTimeOffset.UtcNow,
                occurences = fault.occurencesCount,
                messageIdentifier = fault.messageIdentifier,
                parameterOrSubsystemIdType = fault.parameterOrSubsystemIdType,
                sourceAddress = fault.sourceAddress,
                suspectParameterNumber = fault.suspectParameterNumber,
                failureModeIdentifier = fault.failureModeIdentifier,
                urgentFlag = fault.urgentFlag,
                odometer = fault.odometer,
                engineRpm = fault.engineRpm,
                ecmSpeed = fault.ecmSpeed,
                ccAccelerationSwitch = false,
                ccBrakeSwitch = false,
                ccClutchSwitch = false,
                ccCoastSwitch = false,
                ccCruiseSwitch = false,
                ccResumeSwitch = false,
                ccSetSwitch = false,
                ccSpeed = 0.0M,
                ccSwitch = false,
                ignitionAccessory = false,
                ignitionAidContact = false,
                ignitionCrankContact = false,
                ignitionRunContact = false,
                gpsQuality = fault.gpsQuality,
            };
            m_context.Add(vfce);
            m_context.SaveChanges();
        }


    }
}
