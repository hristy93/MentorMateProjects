using System;
using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    class ImmigrantExtremist : Immigrant, IKillPeople
    {
        private const int VICTIMS_PERCENTAGE_TOP_LIMIT = 70;
        private const int VICTIMS_PERCENTAGE_BOTTOM_LIMIT = 10;

        public override List<Immigrant> Family { get; protected set; } = new List<Immigrant>();
        //protected override Passport Passport { get; set; } = null;
        public override List<Weapon> Weapons { get; protected set; } = new List<Weapon>();

        public ImmigrantExtremist(
          Country immigrantHomeCountry,
          City immigrantHomeCity,
          decimal immigrantMoney) : base(immigrantHomeCountry,
                                         immigrantHomeCity,
                                         immigrantMoney)
        {
            Type = ImmigrantTypes.Extremist;
        }

        public void KillPeople(int weaponsCount)
        {
            if (HasImmigrated)
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
                    peopleKilled += (int)(bulletsFired * GetVictimsPercentage());

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

        public double GetVictimsPercentage() => 0.01 * RandomGeneratorInstance.RandomNumber(VICTIMS_PERCENTAGE_BOTTOM_LIMIT, VICTIMS_PERCENTAGE_TOP_LIMIT + 1);
    }
}
