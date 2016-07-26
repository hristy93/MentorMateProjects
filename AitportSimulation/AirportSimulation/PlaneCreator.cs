using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class PlaneCreator : AircraftCreator
    {
        private ATCTower _tower = null;

        public PlaneCreator(ATCTower tower)
        {
            _tower = tower;
        }

        public override ITower CreateAircraft(int fuelLeft, AircraftTypes aircraftType, int passengersCount)
        {
            switch (aircraftType)
            {
                case AircraftTypes.Boeing747:
                    {
                        return new Boeing(_tower, AircraftTypes.Boeing747, fuelLeft, passengersCount);
                    }
                case AircraftTypes.Boeing737:
                    {
                        return new Boeing(_tower, AircraftTypes.Boeing737, fuelLeft, passengersCount);
                    }
                case AircraftTypes.CanadairCRJ700:
                    {
                        return new Canadair(_tower, AircraftTypes.CanadairCRJ700, fuelLeft, passengersCount);
                    }
                case AircraftTypes.Cessna560XL:
                    {
                        return new Cessna(_tower, AircraftTypes.Cessna560XL, fuelLeft, passengersCount);
                    }
                default:
                    {
                        throw new InvalidOperationException("That type of aircraft cannot be created!");
                    }
            }
        }
    }
}
