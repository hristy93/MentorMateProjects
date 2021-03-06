﻿using System;
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

    public class Canadair : Plane, ITower
    {
        public Canadair(ATCTower tower, AircraftTypes aircraftType = AircraftTypes.CanadairCRJ700, int fuelLeft = 950, int passengersCount = 70) : base(tower)
        {
            try
            {
                FuelTankCapacity = 950;
                ValidateFuelLeft(fuelLeft);
                MaxPassengersCount = 70;
                ValidatePassengersCount(passengersCount);
            }
            catch (ArgumentOutOfRangeException)
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
            TimeToTouchDown = 4;
        }
    }
}
