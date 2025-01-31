using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermostatEventsApp.CoolingSystem;
using ThermostatEventsApp.Device;
using ThermostatEventsApp.HeatSensor;

namespace ThermostatEventsApp.Thermostat
{
    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSensor _heatSensor = null;
        private IDevice _device = null;


        public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
        {
            _coolingMechanism = coolingMechanism;
            _heatSensor = heatSensor;
            _device = device;
        }

        private void WireUpEventsToEventHandlers()
        {
            _heatSensor.TemperatureReachesEmergencyLevelEventHandler += heatSensor_TemperatureReachesEmergencyLevelEventHandler;
            _heatSensor.TemperatureFallsBelowWarningLevelEventHandler += heatSensor_TemperatureFallsBelowWarningLevelEventHandler;
            _heatSensor.TemperatureReachesWarningLevelEventHandler += heatSensor_TemperatureReachesWarningLevelEventHandler;
        }
        private void heatSensor_TemperatureReachesEmergencyLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency alert!! (Emergency level is between {_device.EmergencyTemperatureLevel} and above)");
            _device.HandleEmergency();
            Console.ResetColor();
        }

        private void heatSensor_TemperatureFallsBelowWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information alert!! Temperature falls below warning level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.Off();
            Console.ResetColor();
        }


        private void heatSensor_TemperatureReachesWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning alert!! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.On();
            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
            WireUpEventsToEventHandlers();
            _heatSensor.RunHeatSensor();
        }
    }
}
