using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Passport
    {
        public string Name { get; private set; }
        public byte Age { get; private set; }
        public Country HomeCountry { get; private set; }
        public City HomeCity { get; private set; }

        public Passport (
            string immigrantName,
            byte immigrantAge,
            Country immigrantCountry,
            City immigrantHomeCity
            )
        {
            Name = immigrantName;
            Age = immigrantAge;
            HomeCountry = immigrantCountry;
            HomeCity = immigrantHomeCity;
        }

        public Passport(
            string immigrantName,
            int immigrantAge,
            Country immigrantCountry,
            City immigrantHomeCity
            ) : this(immigrantName,
                (byte) immigrantAge,
                immigrantCountry,
                immigrantHomeCity
                )
        {

        }
    }
}
