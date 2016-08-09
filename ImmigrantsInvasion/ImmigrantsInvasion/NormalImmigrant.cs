using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class NormalImmigrant : Immigrant
    {
        private const int MAX_SIBLINGS_COUNT = 10;

        protected override List<Immigrant> Family { get; set; } = new List<Immigrant>(MAX_SIBLINGS_COUNT);
        //protected override Passport Passport { get; set; }
        protected override List<Weapon> Weapons { get; set; } = null;

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
