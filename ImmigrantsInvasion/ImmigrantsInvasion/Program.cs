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
                Demo demo = new Demo(100, 5, 200);
                demo.DisplayImmigrantsStatistics();
                demo.ImmigrateAll();
                demo.UnleashImmigrantsKillingSpree();

                Console.ReadLine();
            }           
        }

        static void PrintToFile()
        {
            using (FileStream fs = new FileStream("ImmigrantsInvasionLog.txt", FileMode.Create))
            {
                TextWriter tmp = Console.Out;
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Console.SetOut(sw);
                    Demo demo = new Demo(100, 5, 200);
                    demo.DisplayImmigrantsStatistics();
                    demo.ImmigrateAll();
                    demo.UnleashImmigrantsKillingSpree();
                }

                Console.SetOut(tmp);
            }
        }
    }
}
