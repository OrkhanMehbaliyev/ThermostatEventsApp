using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermostatEventsApp.CoolingSystem;
using ThermostatEventsApp.HeatSensor;
using ThermostatEventsApp.Thermostat;

namespace ThermostatEventsApp.Device
{
    public class Device : IDevice
    {
        const double WarningLevel = 27;
        const double EmergencyLevel = 75;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Sending out notifications to emergency services personal...");

            ShutDownDevice();
            Console.WriteLine();
        }

        private void ShutDownDevice()
        {
            Console.WriteLine("Shutting down device...");
        }

        public void RunDevice()
        {
            Console.WriteLine("Device is running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism.CoolingMechanism();
            IHeatSensor heatSensor = new HeatSensor.HeatSensor(WarningLevel, EmergencyLevel);
            IThermostat thermostat = new Thermostat.Thermostat(this, heatSensor, coolingMechanism);

            thermostat.RunThermostat();

        }

        public double WarningTemperatureLevel { get => WarningLevel; }
        public double EmergencyTemperatureLevel { get => EmergencyLevel; }
    }
}
