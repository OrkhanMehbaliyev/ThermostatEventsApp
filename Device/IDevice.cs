using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermostatEventsApp.Device
{
    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }
        void RunDevice();

        void HandleEmergency();
    }
}
