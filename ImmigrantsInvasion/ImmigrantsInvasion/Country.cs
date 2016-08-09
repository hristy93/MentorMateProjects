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
        public List<City> cities = null;

        public Country(string contryName, List<City> citiesInTheCountry)
        {
            Name = contryName;
            cities = citiesInTheCountry;
        }

        public City GetRandomCity()
        {
            return cities.ElementAtOrDefault(_random.RandomNumber(0, cities.Count));
        }

        public void RemoveDestroyedCity(City destroyedCity)
        {
            cities.Remove(destroyedCity);
        }

        public void RemoveImmigrant(Immigrant immigrantToRemove)
        {
            City city = cities.Where(c => c.Immigrants.Contains(immigrantToRemove)).Single();
            city.RemoveImmigrant(immigrantToRemove);
        }
    }
}
