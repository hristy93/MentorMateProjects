using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class ImmigrantOrigin
    {
        private RandomGenerator _random = RandomGenerator.Instance;

        public List<string> ImmigrantHomeCountryNames;
        public List<string> ImmigrantHomeCityNames;
        public List<string> ImmigrantNames;
        public List<City> ImmigrantHomeCities;
        public List<Country> ImmigrantHomeCountries;

        public ImmigrantOrigin(ImmigrantOriginOptions immigrantOrigin)
        {
            InitializeNames(immigrantOrigin);
            InitializeHomeCities(immigrantOrigin);
            InitializeHomeCountries(immigrantOrigin);

        }
        private void InitializeHomeCities(ImmigrantOriginOptions immigrantOrigin)
        {
            if (immigrantOrigin == ImmigrantOriginOptions.MiddleEast)
            {
                int citiesCount = 8;
                int homeCitiesCount = ImmigrantHomeCityNames.Count;
                List<int> cityCitizensCount = new List<int>(homeCitiesCount);
                for (int i = 0; i < citiesCount; i++)
                {
                    cityCitizensCount.Add(_random.RandomNumber(500, 1000));
                }

                ImmigrantHomeCities = new List<City>(citiesCount)
                {
                    new City(ImmigrantHomeCityNames[0], cityCitizensCount[0]),
                    new City(ImmigrantHomeCityNames[1], cityCitizensCount[1]),
                    new City(ImmigrantHomeCityNames[2], cityCitizensCount[2]),
                    new City(ImmigrantHomeCityNames[3], cityCitizensCount[3]),
                    new City(ImmigrantHomeCityNames[4], cityCitizensCount[4]),
                    new City(ImmigrantHomeCityNames[5], cityCitizensCount[5]),
                    new City(ImmigrantHomeCityNames[6], cityCitizensCount[6]),
                    new City(ImmigrantHomeCityNames[7], cityCitizensCount[7])
                };
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's origin because is is not supported!");
            }
        }

        private void InitializeHomeCountries(ImmigrantOriginOptions immigrantOrigin)
        {
            if (immigrantOrigin == ImmigrantOriginOptions.MiddleEast)
            {
                int contriesCount = 8;
                ImmigrantHomeCountries = new List<Country>(contriesCount)
            {
                new Country(ImmigrantHomeCountryNames[0], new List<City>() { ImmigrantHomeCities[0] }),
                new Country(ImmigrantHomeCountryNames[1], new List<City>() { ImmigrantHomeCities[1] }),
                new Country(ImmigrantHomeCountryNames[2], new List<City>() { ImmigrantHomeCities[2] }),
                new Country(ImmigrantHomeCountryNames[3], new List<City>() { ImmigrantHomeCities[3] }),
                new Country(ImmigrantHomeCountryNames[4], new List<City>() { ImmigrantHomeCities[4] }),
                new Country(ImmigrantHomeCountryNames[5], new List<City>() { ImmigrantHomeCities[5] }),
                new Country(ImmigrantHomeCountryNames[6], new List<City>() { ImmigrantHomeCities[6] }),
                new Country(ImmigrantHomeCountryNames[7], new List<City>() { ImmigrantHomeCities[7] })
            };
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's origin because is is not supported!");
            }
        }

        private void InitializeNames(ImmigrantOriginOptions immigrantOrigin)
        {
            if (immigrantOrigin == ImmigrantOriginOptions.MiddleEast)
            {
                ImmigrantHomeCountryNames = new List<string>()
                {
                    "Syria", "Lebanon", "Jordan", "Iraq", "Afghanistan", "Pakistan", "Turkmenistan", "Saudi Arabia"
                };
                ImmigrantHomeCityNames = new List<string>()
                {
                    "Damascus", "Beirut", "Ammam", "Baghdad", "Kabul", "Lahore", "Ashgabat", "Riyadh"
                };
                ImmigrantNames = new List<string>()
                {
                    "Atidge", "Bilem", "Rahmud", "Mustafa", "Fatme", "Rahmud", "Yaman", "Mirra"
                };
            }
            else
            {
                throw new InvalidOperationException("Unable to create immigrant's origin because is is not supported!");
            }
        }
    }
}
