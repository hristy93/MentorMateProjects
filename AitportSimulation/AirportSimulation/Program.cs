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
            Plane plane = new Plane();
            Canadair canadair = new Canadair();
            Console.WriteLine(canadair.Name.ToString());
            Console.ReadLine();
        }
    }
}
