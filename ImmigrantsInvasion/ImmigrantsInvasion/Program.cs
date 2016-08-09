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

            //WeaponsCollection weaponsCollection = WeaponsCollection.Instance(20);
            //var weapons = weaponsCollection.GetAllWeapons();

            //ImmigrantExtremist extremist = new ImmigrantExtremist("John", 23, new Country("USA", null), null, 4.5m);
            //for (int i = 0; i < 6; i++)
            //{
            //    extremist.BuyWeapon();
            //}
            //extremist.KillPeople();

            Demo demo = new Demo(100, 5, 200);
            //List<ImmigrantExtremist> extremists = demo.DemoImmigrants.OfType<ImmigrantExtremist>().ToList();
            foreach (var immigrant in demo.DemoImmigrants)
            {              
                if (immigrant is ImmigrantExtremist)
                {
                    (immigrant as ImmigrantExtremist).KillPeople();
                }
                else if (immigrant is RadicalImmigrant)
                {
                    (immigrant as RadicalImmigrant).KillPeople();
                }
            }

            Console.ReadLine();
        }
    }
}
