using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class NormalImmigrant : Immigrant
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>(10);
        protected override Passport Passport { get; set; }
        protected override List<Weapon> Weapons { get; set; } = null;
    }
}
