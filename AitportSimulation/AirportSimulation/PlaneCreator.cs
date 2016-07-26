using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class PlaneCreator : AircraftCreator
    {
        public override Aircraft CreateAircraft(int fuelLeft, AircraftTypes aircraftType, int passengersCount)
        {
            switch (aircraftType)
            {
                case AircraftTypes.Boeing747:
                    {
                        return new Boeing(fuelLeft, AircraftTypes.Boeing747, passengersCount);
                    }
                case AircraftTypes.Boeing737:
                    {
                        return new Boeing(fuelLeft, AircraftTypes.Boeing737, passengersCount);
                    }
                case AircraftTypes.CanadairCRJ700:
                    {
                        return new Canadair(fuelLeft, AircraftTypes.CanadairCRJ700, passengersCount);
                    }
                case AircraftTypes.Cessna560XL:
                    {
                        return new Cessna(fuelLeft, AircraftTypes.Cessna560XL, passengersCount);
                    }
                default:
                    {
                        throw new InvalidOperationException("That type of aircraft cannot be created!");
                    }
            }
        }
    }
}
