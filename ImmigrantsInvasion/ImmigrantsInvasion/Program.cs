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
            int extremistsCount = demo.DemoImmigrants.OfType<ImmigrantExtremist>().Count();
            int radicalsCount = demo.DemoImmigrants.OfType<RadicalImmigrant>().Count();
            int normalsCount = demo.DemoImmigrants.OfType<NormalImmigrant>().Count();
            Console.WriteLine($"Normal immigrants: {normalsCount}");
            Console.WriteLine($"Immigrant extremists: {extremistsCount}");
            Console.WriteLine($"Radical immigrants: {radicalsCount}");
            Console.WriteLine($"Total illegal immigrants: {extremistsCount + radicalsCount}\n");

            demo.ImmigrateAll();
            //List<ImmigrantExtremist> extremists = demo.DemoImmigrants.OfType<ImmigrantExtremist>().ToList();

            Console.WriteLine($"Illegal immigrants caught: {demo.IllegalImmigrantsCaught}\n");

            int oldCityCount;
            int newCityCount;

            foreach (var immigrant in demo.DemoImmigrants)
            {

                if (!immigrant.isDead)
                {
                    oldCityCount = demo.DemoCountry.cities.Count;

                    if (immigrant is ImmigrantExtremist)
                    {
                        (immigrant as ImmigrantExtremist).KillPeople(5);
                    }
                    else if (immigrant is RadicalImmigrant)
                    {
                        (immigrant as RadicalImmigrant).KillPeople(5);
                    }

                    newCityCount = demo.DemoCountry.cities.Count;
                    if (oldCityCount != newCityCount)
                    {
                        var deadImmigrants = immigrant.CurrentCity.Immigrants;
                        demo.DemoImmigrants.Where(j => deadImmigrants.Contains(j)).All(c => { c.isDead = true; return true; });
                        //demo.DemoImmigrants.RemoveAll(j => deadImmigrants.Contains(j));
                    } 
                }
            }

            Console.ReadLine();
        }
    }
}
