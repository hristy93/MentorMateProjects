using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AirportSimulation
{
    public abstract class Aircraft
    {
        protected static int Count = 0;
        //public int Id { get; protected set; }
        //public string Name { get; protected set; }
        public string ManufacturingNumber { get; protected set; }
        public int Weight { get; protected set; }
        public int FuelConsumption { get; protected set; }
        public int FuelTankCapacity { get; protected set; }
        //public int TouchDownTime { get; protected set; }
        public int PassengersCount { get; protected set; }
        public int MaxPassengersCount { get; protected set; }
        //public int FuelLeft { get; protected set; }
        protected bool hasLanded = false;
        protected bool hasRedirected = false;

        public delegate void RedirectionHandler (ITower redirectedAircraft);
        public static event RedirectionHandler RedirectionToOtherAirport;

        #region Abstract Methods
        protected abstract void SubscribeToPrecomputeFuelLeft();
        protected abstract void UnsubscribeToPrecomputeFuelLeft();
        protected abstract void SubscribeToCheckFuelLeft();
        protected abstract void UnsubscribeToCheckFuelLeft();
        protected abstract void PrecomputeFuelLeft(object sender, ElapsedEventArgs e);
        protected abstract void CheckFuelLeft(object sender, ElapsedEventArgs e);
        #endregion

        #region Protected Methods
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

        protected string GetManufacturingNumber(AircraftTypes aircraftType, Plane plane)
        {
            return aircraftType.ToString().Substring(plane.GetType().Name.Length);
        }

        [Obsolete]
        protected void NotifyATCTowerForRedirection(ITower redirectedAircraft)
        {
            RedirectionToOtherAirport(redirectedAircraft);
        }
        #endregion
    }
}
