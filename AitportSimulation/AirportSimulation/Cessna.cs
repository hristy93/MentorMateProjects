using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{ 
    //enum CessnaTypes
    //{
    //    Cessna560XL,
    //    Cessna680,
    //    Cessna750
    //}

    public class Cessna : Plane
    {
        public Cessna(int fuelLeft, string manufacturingNumber = "560XL", int passengersCount = 8) : base()
        {
            try
            {
                FuelTankCapacity = 700;
                ValidateFuelLeft(fuelLeft);
                MaxPassengersCount = 8;
                ValidatePassengersCount(passengersCount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            PassengersCount = passengersCount;
            FuelLeft = fuelLeft;
            ManufacturingNumber = manufacturingNumber;
            Name = nameof(Cessna) + " " + ManufacturingNumber;
            Weight = 4500;
            FuelConsumption = 30;
            TouchDownTime = 2;
        }
    }
}
