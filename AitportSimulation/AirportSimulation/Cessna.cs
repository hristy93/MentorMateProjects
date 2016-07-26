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

    public class Cessna : Plane, ITower
    {
        public Cessna(ATCTower tower, AircraftTypes aircraftType = AircraftTypes.Cessna560XL, int fuelLeft = 700, int passengersCount = 8) : base(tower)
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
            ManufacturingNumber = GetManufacturingNumber(aircraftType, this);
            Name = nameof(Cessna) + " " + ManufacturingNumber;
            Weight = 4500;
            FuelConsumption = 30;
            TouchDownTime = 2;
        }
    }
}
