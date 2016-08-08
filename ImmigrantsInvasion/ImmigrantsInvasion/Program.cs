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
            //RandomPropability rand = RandomPropability.Instance;
            //for (int i = 0; i < 10; i++)
            //{
            //    bool hasFiredAGun = rand.Propability(0.4);
            //    Console.WriteLine(hasFiredAGun.ToString());
            //}

            WeaponsCollection weaponsCollection = WeaponsCollection.Instance(20);
            var weapons = weaponsCollection.GetAllWeapons();

            Console.ReadLine();
        }
    }
}
