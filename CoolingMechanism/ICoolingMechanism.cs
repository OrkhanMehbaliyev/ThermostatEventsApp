using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermostatEventsApp.CoolingSystem
{
    public interface ICoolingMechanism
    {
        void On();

        void Off();
    }
}
