using System;
using System.Collections.Generic;
using System.Linq;

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

        public void RemoveDestroyedCity(City destroyedCity)
        {
            if (Cities.Count == 0)
            {
                throw new InvalidOperationException("Unable to remove this city because there is none of them in this country!");
            }
            else if (!Cities.Contains(destroyedCity))
            {
                throw new InvalidOperationException("Unable to remove this city because it is not from in this country!");
            }

            Cities.Remove(destroyedCity);
        }

        public void RemoveImmigrant(Immigrant immigrantToRemove) => immigrantToRemove.CurrentCity.RemoveImmigrant(immigrantToRemove);
    }
}
