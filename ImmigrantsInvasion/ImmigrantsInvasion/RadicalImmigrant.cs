using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class RadicalImmigrant : Immigrant, IKillPeople
    {
        private const int MAX_WEAPONS_COUNT = 5;

        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        protected override Passport Passport { get; set; }
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>(MAX_WEAPONS_COUNT);

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

        public void KillPeople()
        {
            foreach (var weapon in Weapons)
            {
                Console.WriteLine($"Emergency news! A radical immigrant called { Passport.Name}, age { Passport.Age}, " +
                    $" killed a lot of people in {CurrentCity}");
                weapon.Fire();
            }
        }

        public void BuyWeapon()
        {
            Weapon weapon = WeaponsCollectionInstance.GetRandomWeapon(false);
            if (weapon.Price >= Money)
            {
                Console.WriteLine($"The radical immigrant doesn't have enough money to buy " +
                    $" the {weapon.Type.ToString().ToLower()}");
                CurrentCountry.RemoveImmigrant(this);
                return;
            }

            Weapons.Add(weapon);
        }

        // public void Get
    }
}

