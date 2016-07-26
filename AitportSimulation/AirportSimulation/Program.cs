using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            ATCTower tower = new ATCTower();
            PlaneCreator planeCreator = new PlaneCreator(tower);
            List<ITower> aircraftsInTheAir = null;
            try
            {
                aircraftsInTheAir = new List<ITower>
                {
                    planeCreator.CreateAircraft(90, AircraftTypes.Boeing737, 270),
                    planeCreator.CreateAircraft(140, AircraftTypes.Boeing737, 270),
                    planeCreator.CreateAircraft(100, AircraftTypes.CanadairCRJ700, 70),
                    planeCreator.CreateAircraft(75, AircraftTypes.Cessna560XL, 8),
                    planeCreator.CreateAircraft(300, AircraftTypes.Boeing747, 350),
                    planeCreator.CreateAircraft(10, AircraftTypes.Cessna560XL, 8)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the aircrafts: {ex.Message}");
                return;
            }
            tower.GetInitialAircraftsState(aircraftsInTheAir);
            tower.OrderToTouchDown();
            Console.ReadLine();
        }
    }
}
