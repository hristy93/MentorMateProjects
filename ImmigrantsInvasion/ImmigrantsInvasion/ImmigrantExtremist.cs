using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class ImmigrantExtremist : Immigrant, IKillPeople
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        protected override Passport Passport { get; set; } = null;
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>();

        public ImmigrantExtremist(
          string immigrantName,
          byte immigrantAge,
          Country immigrantHomeCountry,
          City immigrantHomeCity,
          decimal immigrantMoney) : base(immigrantName,
                                          immigrantAge,
                                          immigrantHomeCountry,
                                          immigrantHomeCity,
                                          immigrantMoney)
        {

        }

        public void KillPeople()
        {
            //Console.WriteLine($"Emergency news! An immigrant extremist called {Passport.Name}, age {Passport.Age}, "
            //  + $"detonated a bomb in {CurrentCity} and destroyed the whole city!");
            int bulletsFired = 0;
            int peopleKilled = 0;
            bool isBombDetonated = false;

            Console.WriteLine($"Emergency news! A immigrant extremist called { Passport.Name}, age { Passport.Age}," +
                   $" killed a lot of people in {CurrentCity.Name}! More infromation:");

            foreach (var weapon in Weapons)
            {
                bulletsFired += weapon.Fire();
                peopleKilled += bulletsFired * GetVictimsPercentage();

                if (weapon.Type == WeaponTypes.Bomb)
                {
                    Console.WriteLine($"The immigrant extremist detonated a bomb and " +
                        "destroyed the whole city!");
                    isBombDetonated = true;
                    CurrentCountry.RemoveDestroyedCity(CurrentCity);
                    break;
                }
            }

           
            if (isBombDetonated)
            {
                peopleKilled += CurrentCity.CitizensCount;
            }

            Console.WriteLine($"The immigrant extremist killed {peopleKilled} people!\n");

        }

        public void BuyWeapon()
        {
            Weapon weapon = WeaponsCollectionInstance.GetRandomWeapon(true);
            if (weapon.Price >= Money)
            {
                Console.WriteLine($"The immigrant extremist doesn't have enough money to buy " +
                    $" the {weapon.Type.ToString().ToLower()}");
                CurrentCountry.RemoveImmigrant(this);
                return;
            }

            Weapons.Add(weapon);
        }

        private int GetVictimsPercentage()
        {
            return (int)(0.1 * RandomGeneratorInstance.RandomNumber(10, 70 + 1));
        }
    }
}
