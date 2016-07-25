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

        public delegate void TimeElapsedHandler(object sender, ElapsedEventArgs e);

        private Time()
        {

        }

        private static Time Instance
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
                        }
                    }
                }

                return _instance;
            }
        }

        public void Subscribe(TimeElapsedHandler handler)
        {
            _timer.Elapsed += new ElapsedEventHandler(handler);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            
        }
    }
}
