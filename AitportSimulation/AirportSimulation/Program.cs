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
            //Plane plane = new Plane();
            
            ATCTower tower = new ATCTower();
            tower.OrderToTouchDown();
            Console.ReadLine();
        }
    }
}
