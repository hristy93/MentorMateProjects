using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class RadicalImmigrant : Immigrant, IKillPeople
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        protected override Passport Passport { get; set; }
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>(5);

        public void KillPeople()
        {
            foreach (var weapon in Weapons)
            {
                Console.WriteLine($"Emergency news! A radical immigrant called { Passport.Name}, age { Passport.Age}, " +
                    $" killed a lot of people in {CurrentCity}");
                weapon.Fire();
            }
        }

       // public void Get
    }
}

