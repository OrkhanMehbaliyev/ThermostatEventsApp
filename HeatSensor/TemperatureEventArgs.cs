using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermostatEventsApp.HeatSensor
{
    public class TemperatureEventArgs
    {
        public double Temperature { get; set; }  
        public DateTime CurrentDateTime { get; set; }

    }
}
