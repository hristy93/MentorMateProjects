using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Country
    {
        private static RandomGenerator _random = RandomGenerator.Instance;

        public string Name { get; private set; }
        public List<City> Cities { get; private set; } = null;

        public Country(string contryName, List<City> citiesInTheCountry)
        {
            Name = contryName;
            Cities = citiesInTheCountry;
        }

        public City GetRandomCity() => Cities.ElementAtOrDefault(_random.RandomNumber(0, Cities.Count));

        public void RemoveDestroyedCity(City destroyedCity) => Cities.Remove(destroyedCity);

        public void RemoveImmigrant(Immigrant immigrantToRemove)
        {
            City city = immigrantToRemove.CurrentCity;
            city.RemoveImmigrant(immigrantToRemove);
        }
    }
}
