using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    //enum BoeingTypes
    //{
    //    Boeing747,
    //    Boeing737
    //}

    public class Boeing : Plane
    {
        public Boeing(int fuelLeft, AircraftTypes aircraftType, int passengersCount) : base()
        {

            ManufacturingNumber = GetManufacturingNumber(aircraftType, this);
            Name = nameof(Boeing) + " " + ManufacturingNumber;

            if (aircraftType == AircraftTypes.Boeing737)
            {
                try
                {
                    FuelTankCapacity = 1600;
                    ValidateFuelLeft(fuelLeft);
                    MaxPassengersCount = 270;
                    ValidatePassengersCount(passengersCount);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }

                PassengersCount = passengersCount;
                FuelLeft = fuelLeft;
                Weight = 12400;
                FuelConsumption = 75;
                TouchDownTime = 3;
                MaxPassengersCount = 270;
            }
            else if (aircraftType == AircraftTypes.Boeing747)
            {
                FuelTankCapacity = 950;
                ValidateFuelLeft(fuelLeft);
                MaxPassengersCount = 350;
                ValidatePassengersCount(passengersCount);
                PassengersCount = passengersCount;
                FuelLeft = fuelLeft;
                Weight = 9000;
                FuelConsumption = 55;
                TouchDownTime = 4;
            }
        }
    }

}
