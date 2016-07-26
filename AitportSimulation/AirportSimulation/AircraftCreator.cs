using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public abstract class AircraftCreator
    {
        public abstract Aircraft CreateAircraft(int fuelLeft, AircraftTypes aircraftType, int passengersCount);
    }
}
