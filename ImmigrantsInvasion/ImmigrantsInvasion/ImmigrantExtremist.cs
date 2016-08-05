using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class ImmigrantExtremist : Immigrant
    {
        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>();
        protected override Passport Passport { get; set; } = null;
        protected override List<Weapon> Weapons { get; set; } = new List<Weapon>();
    }
}
