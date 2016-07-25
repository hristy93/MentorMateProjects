using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        public string Name { get; protected set; }
        public string ManufacturingNumber { get; protected set; }
        public int Weight { get; protected set; }
        public int FuelConsumption { get; protected set; }
        public int TankCapacity { get; protected set; }
        public int TouchDownTime { get; protected set; }
        public int PassengersCount { get; protected set; }
        public int FuelLeft { get; protected set; }

        public abstract void SubcribeToTime();
        public abstract void TouchDown();
        public abstract void PrecomputeFuelLeft();
    }
}
