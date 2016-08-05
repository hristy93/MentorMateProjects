using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class RadicalImmigrant : Immigrant
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        protected override Passport Passport { get; set; }
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>(5);
    }
}
}
