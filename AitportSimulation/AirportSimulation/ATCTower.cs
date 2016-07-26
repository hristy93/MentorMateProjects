﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class ATCTower
    {
        private List<ITower> _aircrafts = null;
        private Time _time = Time.Instance;
        private int aircraftsCountInTheAir = 0;

        //public delegate int AircractsCountHandler();
        //public static event AircractsCountHandler AircraftLand;

        public ATCTower()
        {
            //SubscribeToRedirectedAircaftEvents();
            _time.StartTime();
        }

        public void GetInitialAircraftsState(List<ITower> aircraftsInTheAir)
        {
            _aircrafts = aircraftsInTheAir;
            aircraftsCountInTheAir = _aircrafts.Count;
        }

        public void PlaneRedirectedPostProcuedures(ITower redirectedAircraft)
        {
            _aircrafts.Remove(redirectedAircraft);
            Console.WriteLine($"The aircraft {redirectedAircraft.Name} with ID {redirectedAircraft.Id} redirected to another airport due to low fuel indication!");
        }

        public void OrderToTouchDown()
        {
            int initialAircraftCount = _aircrafts.Count;
            while(_aircrafts.Count != 0)
            {
                ITower aircraftToLand = _aircrafts.Where(k =>
                k.FuelLeft == _aircrafts.Min(s => s.FuelLeft)).Single();
                aircraftToLand.TouchDown();
                _aircrafts.Remove(aircraftToLand);
                Console.WriteLine($"{_aircrafts.Count} aircrafts left in the air");
                Console.WriteLine("----------\n");
                aircraftsCountInTheAir--;
                Thread.Sleep(aircraftToLand.TouchDownTime * 1000);
            }
            _time.StopTime();
            //UnsubscribeToRedirectedAircaftEvents();
            Console.WriteLine("All aircrafts have landed");
        }

        [Obsolete]
        private void SubscribeToRedirectedAircaftEvents()
        {
            Aircraft.RedirectionToOtherAirport += PlaneRedirectedPostProcuedures;
        }

        [Obsolete]
        private void UnsubscribeToRedirectedAircaftEvents()
        {
            Aircraft.RedirectionToOtherAirport -= PlaneRedirectedPostProcuedures;
        }
    }
}
