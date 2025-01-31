using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermostatEventsApp.HeatSensor
{
    public class HeatSensor : IHeatSensor
    {

        double _warningLevel = 0;
        double _emergencyLevel = 0;

        bool _hasReachedWarningTemperature = false;

        protected EventHandlerList _listEventDelegates = new EventHandlerList();

        static readonly object _temperatureReachesWarningLevelKey = new object();
        static readonly object _temperatureFallsBelowWarningLevelKey = new object();
        static readonly object _temperatureReachesEmergencyLevelKey = new object();

        private double[] _temperatureData = null;

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _emergencyLevel = emergencyLevel;
            _warningLevel = warningLevel;

            SeedData();
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat sensor is running...");
            MonitorTemperature();
        }

        private void MonitorTemperature()
        {
            foreach (double temperature in _temperatureData)
            {
                Console.ResetColor();
                Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temperature}");

                if (temperature >= _emergencyLevel)
                {
                    _hasReachedWarningTemperature = true;
                    TemperatureEventArgs e = new TemperatureEventArgs { CurrentDateTime = DateTime.Now, Temperature = temperature };
                    OnTemperatureReachesEmergencyLevel(e);
                }
                else if (temperature >= _warningLevel)
                {
                    _hasReachedWarningTemperature = true;
                    TemperatureEventArgs e = new TemperatureEventArgs { CurrentDateTime = DateTime.Now, Temperature = temperature };
                    OnTemperatureReachesWarningLevel(e);
                }
                else if (temperature < _warningLevel && _hasReachedWarningTemperature)
                {
                    _hasReachedWarningTemperature = false;
                    TemperatureEventArgs e = new TemperatureEventArgs { CurrentDateTime = DateTime.Now, Temperature = temperature };
                    OnTemperatureFallsBelowWarningLevel(e);
                }

                Thread.Sleep(1000);
            }
        }

        private void SeedData()
        {
            _temperatureData = new double[] { 16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86.45 };
        }

        protected void OnTemperatureReachesWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnTemperatureReachesEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesEmergencyLevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnTemperatureFallsBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureFallsBelowWarningLevelKey];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesEmergencyLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesEmergencyLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesEmergencyLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);

            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);
            }
        }

        event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureFallsBelowWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value);
            }
        }

    }
}
