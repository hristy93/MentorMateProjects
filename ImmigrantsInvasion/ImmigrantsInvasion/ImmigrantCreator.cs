using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class ImmigrantCreator
    {
        public Immigrant CreateImmigrant(
            ImmigrantTypes immigrantType,
            string immigrantName,
            byte immigrantAge,
            Country immigrantHomeCountry,
            City immigrantHomeCity,
            decimal immigrantMoney
            )
        {
            if (immigrantType == ImmigrantTypes.Normal)
            {
                return new NormalImmigrant(immigrantName, immigrantAge, immigrantHomeCountry, immigrantHomeCity, immigrantMoney);
            }
            else if (immigrantType == ImmigrantTypes.Radical)
            {
                return new RadicalImmigrant(immigrantName, immigrantAge, immigrantHomeCountry, immigrantHomeCity, immigrantMoney);
            }
            else
            {
                throw new InvalidOperationException("Unable to create an immigrant with there parameters!");
            }
        }

        public Immigrant CreateImmigrant(
           ImmigrantTypes immigrantType,
           Country immigrantHomeCountry,
           City immigrantHomeCity,
           decimal immigrantMoney
           )
        {
            if (immigrantType == ImmigrantTypes.Extremist)
            {
                return new ImmigrantExtremist(immigrantHomeCountry, immigrantHomeCity, immigrantMoney);
            }
            else if (immigrantType == ImmigrantTypes.Radical)
            {
                return new RadicalImmigrant(immigrantHomeCountry, immigrantHomeCity, immigrantMoney);
            }
            else
            {
                throw new InvalidOperationException("Unable to create an immigrant with there parameters!");
            }
        }
    }
}
