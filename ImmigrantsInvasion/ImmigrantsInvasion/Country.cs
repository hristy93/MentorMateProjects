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
        public List<City> cities = null;
        private static RandomGenerator _random = RandomGenerator.Instance; 

        public Country(string contryName, List<City> citiesInTheCountry)
        {
            Name = contryName;
            cities = citiesInTheCountry;
        }

        public City GetRandomCity()
        {
            return this.cities.ElementAtOrDefault(_random.RandomNumber(1, cities.Count));
        }
    }
}
