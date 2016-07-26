using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class ATCTower
    {
        private List<Aircraft> _aircrafts = null;
        private Time _time = Time.Instance;
        private int aircraftsCountInTheAir = 0;

        //public delegate int AircractsCountHandler();
        //public static event AircractsCountHandler AircraftLand;

        public ATCTower(List<Aircraft> aircraftsInTheAir)
        {
            GetInitialAircraftsState(aircraftsInTheAir);
            SubscribeToRedirectedAircaftEvents();
            StartTimer();
        }

        private void StartTimer()
        {
            _time.StartTime();
        }

        private void GetInitialAircraftsState(List<Aircraft> aircraftsInTheAir)
        {
            _aircrafts = aircraftsInTheAir;
            aircraftsCountInTheAir = _aircrafts.Count;
        }

        public void OrderToTouchDown()
        {
            int initialAircraftCount = _aircrafts.Count;
            while(_aircrafts.Count != 0)
            {
                Aircraft aircraftToLand = _aircrafts.Where(k =>
                k.FuelLeft == _aircrafts.Min(s => s.FuelLeft)).Single();
                aircraftToLand.TouchDown();
                _aircrafts.Remove(aircraftToLand);
                Console.WriteLine($"{_aircrafts.Count} aircrafts left in the air");
                Console.WriteLine("----------\n");
                aircraftsCountInTheAir--;
                Thread.Sleep(aircraftToLand.TouchDownTime * 1000);
            }
            _time.StopTime();
            UnsubscribeToRedirectedAircaftEvents();
            Console.WriteLine("All aircrafts have landed");
        }

        private void SubscribeToRedirectedAircaftEvents()
        {
            Aircraft.RedirectionToOtherAirport += PlaneRedirectedPostProcuedures;
        }

        private void UnsubscribeToRedirectedAircaftEvents()
        {
            Aircraft.RedirectionToOtherAirport -= PlaneRedirectedPostProcuedures;
        }

        private void PlaneRedirectedPostProcuedures(Aircraft redirectedAircraft)
        {
            _aircrafts.Remove(redirectedAircraft);
            Console.WriteLine($"The aircraft {redirectedAircraft.Name} with ID {redirectedAircraft.Id} redirected to another airport due to low fuel indication!");
        }

    }
}
