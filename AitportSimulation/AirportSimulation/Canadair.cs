using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    //enum CanadairType
    //{
    //    CanadairCRJ700,
    //    CanadairCRJ900,
    //    CanadairCL600
    //}

    public class Canadair : Plane
    {
        public Canadair(int fuelLeft, AircraftTypes aircraftType = AircraftTypes.CanadairCRJ700, int passengersCount = 70) : base()
        {
            try
            {
                FuelTankCapacity = 950;
                ValidateFuelLeft(fuelLeft);
                MaxPassengersCount = 70;
                ValidatePassengersCount(passengersCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            PassengersCount = passengersCount;
            FuelLeft = fuelLeft;
            ManufacturingNumber = GetManufacturingNumber(aircraftType, this);
            Name = nameof(Canadair) + " " + ManufacturingNumber;
            Weight = 9000;
            FuelConsumption = 55;
            TouchDownTime = 4;
        }
    }
}
