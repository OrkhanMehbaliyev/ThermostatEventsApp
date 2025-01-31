using System.ComponentModel;
using ThermostatEventsApp.Device;

namespace ThermostatEventsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start device");
            Console.ReadKey();

            IDevice device = new Device.Device();

            device.RunDevice();

            Console.ReadLine();
        }

    }
}