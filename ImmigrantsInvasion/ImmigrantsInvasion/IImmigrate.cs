using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    interface IImmigrate
    {
        void ImmigrateToAnotherCountry(Country countryToImmigrate, List<City> citiesToImmigrate);
        void ImmigrateToAnotherCountry(Country countryToImmigrate, City cityToImmigrate);
        void MigrateToAnotherCity(City cityToImmigrate);
    }
}
