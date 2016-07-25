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

        public ATCTower()
        {
            GetInitialAircraftsState();
            StartTimer();
        }

        private void StartTimer()
        {
            _time.StartTime();
        }

        private void GetInitialAircraftsState()
        {
            _aircrafts = new List<Aircraft>
            {
                new Boeing(90, "737", 270),
                new Boeing(140, "737", 270),
                new Canadair(100, "CRJ700", 70),
                new Cessna(75, "560XL", 8),
                new Boeing(300, "747", 350),
                new Cessna(10, "560XL", 8)
            };
            aircraftsCountInTheAir = _aircrafts.Count;
        }

        public void OrderToTouchDown()
        {
            int initialAircraftCount = _aircrafts.Count;
            for (int i = 1; i <= initialAircraftCount; i++)
            {
                Aircraft aircraftToLand = _aircrafts.Where(k =>
               k.FuelLeft == _aircrafts.Min(s => s.FuelLeft)).Single();
                aircraftToLand.TouchDown();
                _aircrafts.Remove(aircraftToLand);
                aircraftsCountInTheAir--;
                Thread.Sleep(aircraftToLand.TouchDownTime * 1000);
            }
            _time.StopTime();
            Console.WriteLine("\n");
            Console.WriteLine("All aircraft have landed");
        }

    }
}
