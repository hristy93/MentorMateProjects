using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AirportSimulation
{
    public class Plane : Aircraft
    {
        protected Time time = Time.Instance;
        protected bool hasLanded = false;

        public Plane()
        {
            SubcribeToTime();
        }

        public override void PrecomputeFuelLeft(object sender, ElapsedEventArgs e)
        {
            if(!hasLanded)
            {
                Console.WriteLine($"Precomputing {this.FuelLeft} fuel left for {this.Name}");
                FuelLeft -= FuelConsumption / 60;
            }
           
        }

        public override void SubcribeToTime()
        {
            time.Subscribe(new Time.TimeElapsedHandler(PrecomputeFuelLeft));
        }

        public override void TouchDown()
        {
            hasLanded = true;
            Console.WriteLine("\n----------");
            Console.WriteLine($"Plane {this.Name} landed successfully with {this.FuelLeft} fuel left");
            Console.WriteLine("----------\n");
        }
    }

    public class Boeing : Plane
    {
        public Boeing (int fuelLeft, string manufacturingNumber, int passengersCount) : base()
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
        public Canadair(int fuelLeft, string manufacturingNumber = "CRJ700", int passengersCount = 70) : base()
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
        public Cessna(int fuelLeft, string manufacturingNumber = "560XL", int passengersCount = 8) : base()
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
