using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class Program
    {
        static void Main(string[] args)
        {
            bool toPrintToFile = false;

            if (toPrintToFile)
            {
                PrintToFile();
            }
            else
            {
                RunDemo();
                Console.ReadLine();
            }
        }

        private static void RunDemo()
        {
            ImmigrantCreationPropability immigrantCreationPropability = new ImmigrantCreationPropability
            {
                NormalImmigrantPropability = 0.4,
                RadicalImmigrantPropability = 0.25,
                ImmigrantExtremistPropability = 0.35
            };
            //IDemo demo = new Demo(immigrantCreationPropability, 500, 5, 1000, 5, 10);
            IDemo demo = new Demo(immigrantCreationPropability, 100, 5, 200, 5, 2);
            demo.DisplayImmigrantsStatistics();
            demo.ImmigrateAll();
            demo.UnleashImmigrantsKillingSpree();
        }

        static void PrintToFile()
        {
            using (FileStream fileStream = new FileStream("ImmigrantsInvasionLog.txt", FileMode.Create))
            {
                TextWriter oldTextWriter = Console.Out;
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    Console.SetOut(streamWriter);
                    RunDemo();
                }

                Console.SetOut(oldTextWriter);
            }
        }
    }
}
