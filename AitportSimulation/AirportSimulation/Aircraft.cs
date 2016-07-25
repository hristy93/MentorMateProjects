using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        public string Name { get; protected set; }
        public string ManufacturingNumber { get; protected set; }
        public int Weight { get; protected set; }
        public int FuelConsumption { get; protected set; }
        public int FuelTankCapacity { get; protected set; }
        public int TouchDownTime { get; protected set; }
        public int PassengersCount { get; protected set; }
        public int FuelLeft { get; protected set; }
        protected bool hasLanded { get; set; } = false;

        public delegate void RedirectionHandler();
        public static event RedirectionHandler RedirectingToOtherAirport; 

        public abstract void TouchDown();
        protected abstract void SubcribeToPrecomputeFuelLeft();
        protected abstract void PrecomputeFuelLeft(object sender, ElapsedEventArgs e);
        protected abstract void SubcribeToCheckFuelLeft();
        protected abstract void CheckFuelLeft(object sender, ElapsedEventArgs e);
        protected abstract void NotifyATCTowerForRedirection();
    }
}
