using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo(100, 5, 200);
            demo.DisplayImmigrantsStatistics();
            demo.ImmigrateAll();
            demo.UnleashImmigrantsKillingSpree();

            Console.ReadLine();
        }
    }
}
