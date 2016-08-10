﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class ImmigrantExtremist : Immigrant, IKillPeople
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        //protected override Passport Passport { get; set; } = null;
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>();

        public ImmigrantExtremist(
          Country immigrantHomeCountry,
          City immigrantHomeCity,
          decimal immigrantMoney) : base(immigrantHomeCountry,
                                         immigrantHomeCity,
                                         immigrantMoney)
        {

        }

        public void KillPeople(int weaponsCount)
        {
            if (hasImmigrated)
            {
                //Console.WriteLine($"Emergency news! An immigrant extremist called {Passport.Name}, age {Passport.Age}, "
                //  + $"detonated a bomb in {CurrentCity} and destroyed the whole city!");
                int bulletsFired = 0;
                int peopleKilled = 0;
                bool isBombDetonated = false;

                BuyNeededWeapons(weaponsCount);

                Console.WriteLine($"Emergency news! An immigrant extremist with unknown identity" +
                       $" killed a lot of people in {CurrentCity.Name}! More infromation:");

                foreach (var weapon in Weapons)
                {
                    bulletsFired += weapon.Fire();
                    peopleKilled += bulletsFired * GetVictimsPercentage();

                    if (weapon.Type == WeaponTypes.Bomb)
                    {
                        Console.WriteLine($"It destroyed the whole city and killed all of its citizens!");
                        isBombDetonated = true;
                        CurrentCountry.RemoveDestroyedCity(CurrentCity);
                        break;
                    }
                }


                if (isBombDetonated)
                {
                    peopleKilled += CurrentCity.CitizensCount;
                }

                Console.WriteLine($"The immigrant extremist killed {peopleKilled} people including {CurrentCity.Immigrants.Count} immigrants!\n");

            }
        }

        public bool TryToBuyWeapon()
        {
            Weapon weapon = WeaponsCollectionInstance.GetRandomWeapon(true);
            if (weapon.Price >= Money)
            {
                Console.WriteLine($"The immigrant extremist doesn't have enough money to buy " +
                     $"a {weapon.Type.ToString().ToLower()} so he dies from anger and dissapointment!\n");
                CurrentCountry.RemoveImmigrant(this);
                return false;
            }

            Money -= weapon.Price;
            Weapons.Add(weapon);
            return true;
        }

        public void BuyNeededWeapons(int weaponsCount)
        {
            for (int i = 1; i <= weaponsCount; i++)
            {
                if (!TryToBuyWeapon())
                {
                    break;
                }
            }
        }

        private int GetVictimsPercentage()
        {
            return (int)(0.1 * RandomGeneratorInstance.RandomNumber(10, 70 + 1));
        }
    }
}
