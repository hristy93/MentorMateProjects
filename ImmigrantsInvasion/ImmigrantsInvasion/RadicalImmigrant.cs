using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class RadicalImmigrant : Immigrant, IKillPeople
    {
        private const int VICTIMS_PERCENTAGE_TOP_LIMIT = 70;
        private const int VICTIMS_PERCENTAGE_BOTTOM_LIMIT = 10;
        public const int MAX_WEAPONS_COUNT = 5;

        public override List<Immigrant> Family { get; protected set; } = new List<Immigrant>();
        //protected override Passport Passport { get; set; }
        public override List<Weapon> Weapons { get; protected set; } = new List<Weapon>(MAX_WEAPONS_COUNT);

        public RadicalImmigrant(
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

        public RadicalImmigrant(
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

                //foreach (var weapon in Weapons)
                //{
                //    Console.WriteLine($"Emergency news! A radical immigrant called { Passport.Name}, age { Passport.Age}, " +
                //        $" killed a lot of people in {CurrentCity.Name}");
                //    weapon.Fire();
                //}

                int bulletsFired = 0;
                int peopleKilled = 0;

                BuyNeededWeapons(weaponsCount);

                if (Passport != null)
                {
                    Console.WriteLine($"Emergency news! A radical immigrant called { Passport.Name }, age { Passport.Age }, " +
                       $"killed a lot of people in { CurrentCity.Name }! More infromation:");
                }
                else
                {
                    Console.WriteLine($"Emergency news! A radical immigrant with unknown identity " +
                       $"killed a lot of people in { CurrentCity.Name }! More infromation:");
                }


                foreach (var weapon in Weapons)
                {
                    bulletsFired += weapon.Fire();
                    peopleKilled += (int)(bulletsFired * GetVictimsPercentage());

                    //if (weapon.Type == WeaponTypes.Bomb)
                    //{
                    //    Console.WriteLine($"The immigrant extremist detonated a bomb and " +
                    //        "destroyed the whole city!");
                    //    isBombDetonated = true;
                    //    CurrentCountry.RemoveDestroyedCity(CurrentCity);
                    //    break;
                    //}
                }

                Console.WriteLine($"The radical immigrant killed {peopleKilled} people!\n"); 
            }
        }

        public bool TryToBuyWeapon()
        {
            if (Weapons.Count + 1 > MAX_WEAPONS_COUNT)
            {
                throw new InvalidOperationException($"Unable to buy a new weapon because the weapons count will exceeds {MAX_WEAPONS_COUNT}!");
            }
            Weapon weapon = WeaponsCollectionInstance.GetRandomWeapon(false);
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
                if (!TryToBuyWeapon() )
                {
                    break;
                }
            }
        }

        public double GetVictimsPercentage() => 0.01 * RandomGeneratorInstance.RandomNumber(VICTIMS_PERCENTAGE_BOTTOM_LIMIT, VICTIMS_PERCENTAGE_TOP_LIMIT + 1);

        // public void Get
    }
}

