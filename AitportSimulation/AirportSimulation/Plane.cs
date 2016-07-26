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
        private const int FUEL_CONSUMPTION_SCALE = 60;
        private const double FUEL_TANK_CRITICAL_VOLUME_PERCENTAGE = 0.1;

        protected Time time = Time.Instance;

        public Plane()
        {
            SubscribeToPrecomputeFuelLeft();
            SubscribeToCheckFuelLeft();
            Count++;
            Id = Count;
        }


        public override void TouchDown()
        {
            hasLanded = true;
            try
            {
                UnsubscribeToPrecomputeFuelLeft();
                UnsubscribeToCheckFuelLeft();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }
            Console.WriteLine("\n----------");
            Console.WriteLine($"Plane {Name} with ID {Id} landed successfully with {FuelLeft} fuel left");
        }

        protected override void PrecomputeFuelLeft(object sender, ElapsedEventArgs e)
        {
            if (!hasLanded && !hasRedirected)
            {
                Console.WriteLine($"Precomputing {FuelLeft} fuel left for {Name} with ID {Id}");
                FuelLeft -= FuelConsumption / FUEL_CONSUMPTION_SCALE;
            }

        }

        protected override void SubscribeToPrecomputeFuelLeft()
        {
            time.Subscribe(new Time.TimeElapsedHandler(PrecomputeFuelLeft));
        }

        protected override void UnsubscribeToPrecomputeFuelLeft()
        {
            if (hasLanded)
            {
                time.Unsubscribe(new Time.TimeElapsedHandler(PrecomputeFuelLeft));
            }
            else
            {
                throw new InvalidOperationException(AirportMessages.INVALID_UNSUBSCRIBTION_MESSAGE);
            }
        }

        protected override void SubscribeToCheckFuelLeft()
        {
            time.Subscribe(new Time.TimeElapsedHandler(CheckFuelLeft));
        }

        protected override void UnsubscribeToCheckFuelLeft()
        {
            if (hasLanded)
            {
                time.Unsubscribe(new Time.TimeElapsedHandler(CheckFuelLeft));
            }
            else
            {
                throw new InvalidOperationException(AirportMessages.INVALID_UNSUBSCRIBTION_MESSAGE);
            }
        }

        protected override void CheckFuelLeft(object sender, ElapsedEventArgs e)
        {
            if (!hasRedirected && FuelLeft < (int)FuelTankCapacity * FUEL_TANK_CRITICAL_VOLUME_PERCENTAGE)
            {
                hasRedirected = true;
                NotifyATCTowerForRedirection(this);
            }
        }

        protected override void NotifyATCTowerForRedirection(Aircraft redirectedAircraft)
        {
            base.NotifyATCTowerForRedirection(redirectedAircraft);
        }

        protected void ValidateFuelLeft(int fuelLeft)
        {
            if (fuelLeft < 0 || fuelLeft > FuelTankCapacity)
            {
                throw new ArgumentOutOfRangeException(AirportMessages.INVALID_FUEL_LEFT_MESSAGE + FuelTankCapacity.ToString());
            }
        }

        protected void ValidatePassengersCount(int passengersCount)
        {
            if (passengersCount < 0 || passengersCount > MaxPassengersCount)
            {
                throw new ArgumentOutOfRangeException(AirportMessages.INVALID_PASSANGERS_COUNT_MESSAGE + MaxPassengersCount.ToString());
            }
        }
    }
}
