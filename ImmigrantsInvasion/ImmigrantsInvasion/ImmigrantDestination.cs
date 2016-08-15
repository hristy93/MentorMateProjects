using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    class ImmigrantDestination
    {
        private const int CITIZENS_COUNT_BOTTOM_LIMIT = 50000;
        private const int CITIZENS_COUNT_TOP_LIMIT = 100000;

        private RandomGenerator _random = RandomGenerator.Instance;

        public Country Country;
        public List<City> Cities;
        public List<string> CityNames;
        public List<string> PoliceOfficersNames;

        public ImmigrantDestination(ImmigrantDestinationOptions destinationOption, int citiesCount)
        {
            InitializeNames(destinationOption);
            InitializeCountry(destinationOption);
            InitializeCities(destinationOption, citiesCount);
        }

        private void InitializeCountry(ImmigrantDestinationOptions destinationOption)
        {
            if (destinationOption == ImmigrantDestinationOptions.Germany)
            {
                Country = new Country("Germany", Cities);
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's destination because is is not supported!");
            }
        }

        private void InitializeCities(ImmigrantDestinationOptions destinationOption, int citiesCount)
        {
            if (destinationOption == ImmigrantDestinationOptions.Germany)
            {
                List<int> cityCitizensCount = new List<int>(citiesCount);
                for (int i = 0; i < 5; i++)
                {
                    cityCitizensCount.Add(_random.RandomNumber(CITIZENS_COUNT_BOTTOM_LIMIT, CITIZENS_COUNT_TOP_LIMIT));
                }

                Cities = new List<City>(5)
                {
                    new City(CityNames[0], cityCitizensCount[0]),
                    new City(CityNames[1], cityCitizensCount[1]),
                    new City(CityNames[2], cityCitizensCount[2]),
                    new City(CityNames[3], cityCitizensCount[3]),
                    new City(CityNames[4], cityCitizensCount[4])
                }; 
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's destination because is is not supported!");
            }
        }

        private void InitializeNames(ImmigrantDestinationOptions destinationOption)
        {
            if (destinationOption == ImmigrantDestinationOptions.Germany)
            {
                CityNames = new List<string>()
                {
                    "Berlin", "Frankfurt", "Bonn", "Dresden", "Hamburg"
                };

                PoliceOfficersNames = new List<string>()
                {
                    "Hanna", "Bill", "Jonas", "Finn", "Emilly", "Luca", "Yaman", "Marie", "Sofia", "Ben"
                };
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's destination because is is not supported!");
            }
        }
    }
}
