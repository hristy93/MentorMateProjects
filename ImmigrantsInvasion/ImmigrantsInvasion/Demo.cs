using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Demo
    {
        public Country DemoCountry { get; set; }
        public List<City> DemoCities { get; set; }
        public List<Immigrant> DemoImmigrants { get; set; }
        public List<PoliceOfficer> DemoPoliceOfficers { get; set; }
        public List<Weapon> DemoWeapons { get; set; }
        public List<string> DemoImmigrantHomeCountryNames { get; set; } = new List<string>()
        {
            "Syria", "Lebanon", "Jordan", "Iraq", "Afghanistan", "Pakistan", "Turkmenistan", "Saudi Arabia"
        };
        public List<string> DemoImmigrantHomeCityNames { get; set; } = new List<string>()
        {
            "Damascus", "Beirut", "Ammam", "Baghdad", "Kabul", "Lahore", "Ashgabat", "Riyadh"
        };
        public List<string> DemoImmigrantNames { get; set; } = new List<string>()
        {
            "Atidge", "Bilem", "Rahmud", "Mustafa", "Fatme", "Rahmud", "Yaman", "Mirra"
        };
        public List<string> DemoPoliceOfficersNames { get; set; } = new List<string>()
        {
            "Hanna", "Bill", "Jonas", "Finn", "Emilly", "Luca", "Yaman", "Marie", "Sofia", "Ben"
        };

        private RandomGenerator _random = RandomGenerator.Instance;
        private List<City> DemoImmigrantHomeCities { get; set; }
        private List<Country> DemoImmigrantHomeCountries { get; set; }

        public Demo(int immigrantsCount, int citiesCount, int weaponsCount)
        {
            IninializeDemo(immigrantsCount, citiesCount, weaponsCount);
        }

        public void ImmigrateAll()
        {
            foreach (var immigrant in DemoImmigrants)
            {
                Console.Write($"The immigrant immigrated from {immigrant.CurrentCity.Name} ");
                immigrant.MigrateToAnotherCity(DemoCountry, DemoCities);
                Console.Write($"to {immigrant.CurrentCity.Name}, ");
                if (immigrant.Passport == null)
                {
                    Console.Write($"he doesn't have a passport, ");
                }
                else
                {
                    Console.Write($"he has a passport, ");
                }

                if (immigrant.Money == 0.0m)
                {
                    Console.Write($"he doesn't have money and ");
                }
                else
                {
                    Console.Write($"he has money and ");
                }

                if (immigrant.Passport == null)
                {
                    Console.WriteLine($"he doesn't have guns.");
                }
                else
                {
                    Console.WriteLine($"he has guns.");
                }
            }
        }

        //private void doesHeHas(object property)
        //{
        //    if (property == null)
        //    {
        //        Console.Write($"doesn't have a passport and ");
        //    }
        //    else
        //    {
        //        Console.Write($"has a passport and ");
        //    }
        //}

        private void IninializeDemo(int immigrantsCount, int citiesCount, int weaponsCount)
        {
            InitialzePoliceOfficers(immigrantsCount);
            InitialzeWeapons(weaponsCount);
            InitialzeHomeCities();
            InitialzeHomeCountries();
            InitialzeImmigrants(immigrantsCount);
            InitialzeCities(citiesCount);
            InitialzeCounty();
        }

        private void InitialzeWeapons(int weaponsCount)
        {
            WeaponsCollection weaponsCollection = WeaponsCollection.Instance(weaponsCount);
        }

        private void InitialzePoliceOfficers(int immigrantsCount)
        {
            DemoPoliceOfficers = new List<PoliceOfficer>(immigrantsCount);
            for (int i = 0; i < immigrantsCount; i++)
            {
                string policeOfficerName = DemoPoliceOfficersNames[_random.RandomNumber(0, DemoPoliceOfficersNames.Count)];
                PoliceOfficerTypes policeOfficerType = _random.Propability(0.5) ? PoliceOfficerTypes.Policeman : PoliceOfficerTypes.SpecialForces;
                DemoPoliceOfficers.Add(new PoliceOfficer(policeOfficerName, policeOfficerType));
            }
        }

        private void InitialzeImmigrants(int immigrantsCount)
        {
            int randomNameIndex;
            int randomHomeIndex;

            DemoImmigrants = new List<Immigrant>(immigrantsCount);

            for (int i = 0; i < immigrantsCount; i++)
            {
                Immigrant immigrant;
                randomNameIndex = _random.RandomNumber(0, DemoImmigrantNames.Count);
                randomHomeIndex = _random.RandomNumber(0, DemoImmigrantHomeCityNames.Count);
                if (_random.Propability(0.4))
                {
                    immigrant = new NormalImmigrant(
                        DemoImmigrantNames[randomNameIndex],
                        (byte) _random.RandomNumber(10, 65),
                        DemoImmigrantHomeCountries[randomHomeIndex],
                        DemoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(500, 1500)
                        );
                }
                else if (_random.Propability(0.35))
                { 
                    immigrant = new ImmigrantExtremist(
                        DemoImmigrantHomeCountries[randomHomeIndex],
                        DemoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(500, 1500)
                        );
                }
                else
                {
                    if (_random.Propability(0.35))
                    {
                        immigrant = new RadicalImmigrant(
                            DemoImmigrantNames[randomNameIndex],
                            (byte)_random.RandomNumber(10, 65),
                            DemoImmigrantHomeCountries[randomHomeIndex],
                            DemoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(500, 1500)
                            );
                    }
                    else
                    {
                        immigrant = new RadicalImmigrant(
                            DemoImmigrantHomeCountries[randomHomeIndex],
                            DemoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(500, 1500)
                            );
                    }
                        
                }
                DemoImmigrants.Add(immigrant);
            }

            foreach (var immigrant in DemoImmigrants)
            {
                AddRandomImmigrantFamilyMember(immigrant);
                AddRandomImmigrantFamilyMember(immigrant);
                BuyNeededWeapons(immigrant);
            }
        }

        private void BuyNeededWeapons(Immigrant immigrant)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (immigrant is ImmigrantExtremist)
                {
                    (immigrant as ImmigrantExtremist).BuyWeapon();
                }
                else if (immigrant is RadicalImmigrant)
                {
                    (immigrant as RadicalImmigrant).BuyWeapon();
                }
            }
        }

        private void AddRandomImmigrantFamilyMember(Immigrant immigrant)
        {
            int randomImmigrantIndex = _random.RandomNumber(0, DemoImmigrants.Count);
            Immigrant immigrantSibling = DemoImmigrants[randomImmigrantIndex];
            immigrant.AddFamilyMember(immigrantSibling);
        }

        private void InitialzeCities(int citiesCount)
        {
            List<int> cityCitizensCount = new List<int>(5);
            for (int i = 0; i < 5; i++)
            {
                cityCitizensCount.Add(_random.RandomNumber(50000, 100000));
            }

            DemoCities = new List<City>(5)
            {
                new City("Berlin", DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[0]),
                new City("Frankfurt", DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[1]),
                new City("Bonn", DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[2]),
                new City("Dresden", DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[3]),
                new City("Hamburg", DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[4])
            };

            AddPoliceOfficersToCities();
        }

        private void AddPoliceOfficersToCities()
        {
            foreach (var city in DemoCities)
            {
                city.AddPoliceOfficers(DemoPoliceOfficers);
            }
        }

        private void InitialzeHomeCities()
        {
            int homeCitiesCount = DemoImmigrantHomeCityNames.Count;
            List<int> cityCitizensCount = new List<int>(homeCitiesCount);
            for (int i = 0; i < 8; i++)
            {
                cityCitizensCount.Add(_random.RandomNumber(500, 1000));
            }

            DemoImmigrantHomeCities = new List<City>(5)
            {
                new City(DemoImmigrantHomeCityNames[0], null, null, cityCitizensCount[0]),
                new City(DemoImmigrantHomeCityNames[1], null, null, cityCitizensCount[1]),
                new City(DemoImmigrantHomeCityNames[2], null, null, cityCitizensCount[2]),
                new City(DemoImmigrantHomeCityNames[3], null, null, cityCitizensCount[3]),
                new City(DemoImmigrantHomeCityNames[4], null, null, cityCitizensCount[4]),
                new City(DemoImmigrantHomeCityNames[5], null, null, cityCitizensCount[5]),
                new City(DemoImmigrantHomeCityNames[6], null, null, cityCitizensCount[6]),
                new City(DemoImmigrantHomeCityNames[7], null, null, cityCitizensCount[7])
            };

            //for (int i = 0; i < homeCitiesCount; i++)
            //{
            //    randomIndex = _random.RandomNumber(0, DemoImmigrantHomeCity.Count + 1);
            //    City homeCity = new City(, DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[0]);
            //}
        }

        private void InitialzeHomeCountries()
        {
            DemoImmigrantHomeCountries = new List<Country>(5)
            {
                new Country(DemoImmigrantHomeCountryNames[0], new List<City>() { DemoImmigrantHomeCities[0] }),
                new Country(DemoImmigrantHomeCountryNames[1], new List<City>() { DemoImmigrantHomeCities[1] }),
                new Country(DemoImmigrantHomeCountryNames[2], new List<City>() { DemoImmigrantHomeCities[2] }),
                new Country(DemoImmigrantHomeCountryNames[3], new List<City>() { DemoImmigrantHomeCities[3] }),
                new Country(DemoImmigrantHomeCountryNames[4], new List<City>() { DemoImmigrantHomeCities[4] }),
                new Country(DemoImmigrantHomeCountryNames[5], new List<City>() { DemoImmigrantHomeCities[5] }),
                new Country(DemoImmigrantHomeCountryNames[6], new List<City>() { DemoImmigrantHomeCities[6] }),
                new Country(DemoImmigrantHomeCountryNames[7], new List<City>() { DemoImmigrantHomeCities[7] })
            };
        }

        private void InitialzeCounty()
        {
            DemoCountry = new Country("Germany", DemoCities);
        }
    }
}
