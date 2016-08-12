using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class NormalImmigrant : Immigrant
    {
        public const int MAX_SIBLINGS_COUNT = 10;

        public override List<Immigrant> Family { get; protected set; } = new List<Immigrant>(MAX_SIBLINGS_COUNT);
        //protected override Passport Passport { get; set; }
        public override List<Weapon> Weapons { get; protected set; } = null;

        public NormalImmigrant(
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
    }
}
