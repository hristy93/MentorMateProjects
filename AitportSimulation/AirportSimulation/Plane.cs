using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Plane : Aircraft
    {
        public Plane()
        {

        }

        public override void PrecomputeFuelLeft()
        {
            
        }

        public override void SubcribeToTime()
        {
            
        }

        public override void TouchDown()
        {
            throw new NotImplementedException();
        }
    }

    public class Boeing : Plane
    {
        public Boeing (string manufacturingNumber, int passengersCount, int fuelLeft) 
        {
            if (manufacturingNumber == "737")
            {
                ManufacturingNumber = "CRJ700";
                Name = nameof(Boeing) + " " + ManufacturingNumber;
                Weight = 12400;
                FuelConsumption = 75;
                TouchDownTime = 3;
                TankCapacity = 1600;
                PassengersCount = passengersCount;
                FuelLeft = fuelLeft;
            }
            else if (manufacturingNumber == "747")
            {
                ManufacturingNumber = "CRJ700";
                Name = nameof(Boeing) + " " + ManufacturingNumber;
                Weight = 9000;
                FuelConsumption = 55;
                TouchDownTime = 4;
                TankCapacity = 950;
                PassengersCount = 70;
                FuelLeft = fuelLeft;
            }
        }
    }

    public class Canadair : Plane
    {
        public Canadair(string manufacturingNumber = "CRJ700", int passengersCount = 70, int fuelLeft)
        {
            ManufacturingNumber = manufacturingNumber;
            Name = nameof(Canadair) + " " + ManufacturingNumber;
            Weight = 9000;
            FuelConsumption = 55;
            TouchDownTime = 4;
            TankCapacity = 950;
            PassengersCount = passengersCount;
            FuelLeft = fuelLeft;
        }
    }

    public class Cessna : Plane
    {
        public Cessna(string manufacturingNumber = "560XL", int passengersCount = 8, int fuelLeft)
        {
            ManufacturingNumber = manufacturingNumber;
            Name = nameof(Cessna) + " " + ManufacturingNumber;
            Weight = 4500;
            FuelConsumption = 30;
            TouchDownTime = 2;
            TankCapacity = 700;
            PassengersCount = passengersCount;
            FuelLeft = fuelLeft;
        }
    }
}
