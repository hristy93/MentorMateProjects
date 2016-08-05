using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Country
    {
        public string Name { get; private set; }
        public IList<City> cities = null;

        public Country(string contryName, IList<City> citiesInTheCountry)
        {
            Name = contryName;
            cities = citiesInTheCountry;
        }
    }
}
