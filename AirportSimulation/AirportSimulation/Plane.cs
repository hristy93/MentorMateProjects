using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AirportSimulation
{
    public class Plane : Aircraft, ITower
    {
        private const byte FUEL_CONSUMPTION_SCALE = 60;
        private const double FUEL_TANK_CRITICAL_VOLUME_PERCENTAGE = 0.1;

        protected Time time = Time.Instance;
        protected ATCTower controllingTower = null;

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToTouchDown { get; set; }
        public int FuelLeft { get; set; }
        #endregion


        public Plane(ATCTower tower)
        {
            SubscribeToPrecomputeFuelLeft();
            SubscribeToCheckFuelLeft();
            Count++;
            Id = Count;
            controllingTower = tower;
        }

        #region Public Methods
        public void TouchDown()
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
        #endregion

        #region Protected Methods
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
                //NotifyATCTowerForRedirection(this);
                controllingTower.PlaneRedirectedPostProcuedures(this);
            }
        }
        #endregion
    }
}
