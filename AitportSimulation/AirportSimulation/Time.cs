using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AirportSimulation
{
    public sealed class Time
    {
        private const int TimerInterval = 1000;
        private static readonly object syncLock = new object();

        private static Time _instance = null;
        private static Timer _timer = new Timer(TimerInterval);

        private Time()
        {

        }

        public delegate void TimeElapsedHandler(object sender, ElapsedEventArgs e);

        public static Time Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Time();
                            _timer.Interval = TimerInterval;
                        }
                    }
                }

                return _instance;
            }
        }

        public void StartTime()
        {
            _timer.Enabled = true;
            _timer.Start();
        }

        public void StopTime()
        {
            _timer.Enabled = false;
            _timer.Stop();
        }

        public void Subscribe(TimeElapsedHandler handler)
        {
            _timer.Elapsed += new ElapsedEventHandler(handler);
        }
    }
}
