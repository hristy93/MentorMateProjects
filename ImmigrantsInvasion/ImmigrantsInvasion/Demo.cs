using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Demo
    {
        private int _illegalImmigrantsCaught = 0;
        private RandomGenerator _random = RandomGenerator.Instance;
        private List<City> _demoImmigrantHomeCities { get; set; }
        private List<Country> _demoImmigrantHomeCountries { get; set; }

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

        public Demo(int immigrantsCount, int citiesCount, int weaponsCount)
        {
            IninializeDemo(immigrantsCount, citiesCount, weaponsCount);
        }

        public void DisplayImmigrantsStatistics()
        {
            int extremistsCount = DemoImmigrants.OfType<ImmigrantExtremist>().Count();
            int radicalsCount = DemoImmigrants.OfType<RadicalImmigrant>().Count();
            int normalsCount = DemoImmigrants.OfType<NormalImmigrant>().Count();
            Console.WriteLine($"Immigrants' Statistics");
            Console.WriteLine($"-----------");
            Console.WriteLine($"Normal immigrants: {normalsCount}");
            Console.WriteLine($"Immigrant extremists: {extremistsCount}");
            Console.WriteLine($"Radical immigrants: {radicalsCount}");
            Console.WriteLine($"Total illegal immigrants: {extremistsCount + radicalsCount}");
            Console.WriteLine($"-----------\n");
        }

        public void ImmigrateAll()
        {
            Console.WriteLine($"Immigrants' Relocation Information");
            Console.WriteLine($"-----------");
            foreach (var immigrant in DemoImmigrants)
            {
                if (immigrant.TryToMigrateToAnotherCity(DemoCountry, DemoCities))
                {
                    //Thread.Sleep(700);
                    Console.Write($"The immigrant who relocated to {immigrant.CurrentCity.Name}, ");
                    if (immigrant.Passport == null)
                    {
                        Console.Write($"doesn't have a passport, ");
                    }
                    else
                    {
                        Console.Write($"has a passport, ");
                    }

                    if (immigrant.Money == 0.0m)
                    {
                        Console.Write($"doesn't have money and ");
                    }
                    else
                    {
                        Console.Write($"has money and ");
                    }

                    if (immigrant.Passport == null)
                    {
                        Console.WriteLine($"doesn't have guns.");
                    }
                    else
                    {
                        Console.WriteLine($"has guns.");
                    }
                }
                else
                {
                    _illegalImmigrantsCaught++;
                }
            }

            Console.WriteLine($"-----------");
            Console.WriteLine($"Illegal immigrants caught: {_illegalImmigrantsCaught}");
            Console.WriteLine($"-----------\n");
        }

        public void UnleashImmigrantsKillingSpree()
        {
            int oldCityCount;
            int newCityCount;

            foreach (var immigrant in DemoImmigrants)
            {

                if (!immigrant.isDead)
                {
                    oldCityCount = DemoCountry.cities.Count;

                    if (immigrant is ImmigrantExtremist)
                    {
                        (immigrant as ImmigrantExtremist).KillPeople(5);
                    }
                    else if (immigrant is RadicalImmigrant)
                    {
                        (immigrant as RadicalImmigrant).KillPeople(5);
                    }

                    newCityCount = DemoCountry.cities.Count;
                    if (newCityCount == 0)
                    {
                        break;
                    }

                    if (oldCityCount != newCityCount)
                    {
                        var deadImmigrants = immigrant.CurrentCity.Immigrants;
                        DemoImmigrants.Where(d => deadImmigrants.Contains(d)).All(a => { a.isDead = true; return true; });
                        //demo.DemoImmigrants.RemoveAll(j => deadImmigrants.Contains(j));
                    }
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
                        (byte)_random.RandomNumber(10, 65),
                        _demoImmigrantHomeCountries[randomHomeIndex],
                        _demoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(300, 800)
                        );
                }
                else if (_random.Propability(0.35))
                {
                    immigrant = new ImmigrantExtremist(
                        _demoImmigrantHomeCountries[randomHomeIndex],
                        _demoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(500, 1000)
                        );
                }
                else
                {
                    if (_random.Propability(0.35))
                    {
                        immigrant = new RadicalImmigrant(
                            DemoImmigrantNames[randomNameIndex],
                            (byte)_random.RandomNumber(10, 65),
                            _demoImmigrantHomeCountries[randomHomeIndex],
                            _demoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(300, 1000)
                            );
                    }
                    else
                    {
                        immigrant = new RadicalImmigrant(
                            _demoImmigrantHomeCountries[randomHomeIndex],
                            _demoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(300, 1000)
                            );
                    }

                }
                DemoImmigrants.Add(immigrant);
            }

            WeaponsCollection weapons = WeaponsCollection.Instance(200);
            foreach (var immigrant in DemoImmigrants)
            {
                AddRandomImmigrantFamilyMember(immigrant);
                AddRandomImmigrantFamilyMember(immigrant);
                //BuyNeededWeapons(immigrant);
                int WeaponsCount = WeaponsCollection.WeaponsCount();
            }
        }

        //private void BuyNeededWeapons(Immigrant immigrant)
        //{
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        if (immigrant is ImmigrantExtremist)
        //        {
        //            (immigrant as ImmigrantExtremist).BuyWeapon();
        //        }
        //        else if (immigrant is RadicalImmigrant)
        //        {
        //            (immigrant as RadicalImmigrant).BuyWeapon();
        //        }
        //    }
        //}

        private void AddRandomImmigrantFamilyMember(Immigrant immigrant)
        {
            int randomImmigrantIndex = _random.RandomNumber(0, DemoImmigrants.Count);
            Immigrant immigrantSibling = DemoImmigrants[randomImmigrantIndex];
            immigrant.AddFamilyMember(immigrantSibling);
        }

        private void InitialzeCities(int citiesCount)
        {
            List<int> cityCitizensCount = new List<int>(citiesCount);
            for (int i = 0; i < 5; i++)
            {
                cityCitizensCount.Add(_random.RandomNumber(50000, 100000));
            }

            DemoCities = new List<City>(5)
            {
                new City("Berlin", DemoPoliceOfficers, cityCitizensCount[0]),
                new City("Frankfurt", DemoPoliceOfficers, cityCitizensCount[1]),
                new City("Bonn", DemoPoliceOfficers, cityCitizensCount[2]),
                new City("Dresden", DemoPoliceOfficers, cityCitizensCount[3]),
                new City("Hamburg", DemoPoliceOfficers, cityCitizensCount[4])
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

            _demoImmigrantHomeCities = new List<City>(5)
            {
                new City(DemoImmigrantHomeCityNames[0], null, cityCitizensCount[0]),
                new City(DemoImmigrantHomeCityNames[1], null, cityCitizensCount[1]),
                new City(DemoImmigrantHomeCityNames[2], null, cityCitizensCount[2]),
                new City(DemoImmigrantHomeCityNames[3], null, cityCitizensCount[3]),
                new City(DemoImmigrantHomeCityNames[4], null, cityCitizensCount[4]),
                new City(DemoImmigrantHomeCityNames[5], null, cityCitizensCount[5]),
                new City(DemoImmigrantHomeCityNames[6], null, cityCitizensCount[6]),
                new City(DemoImmigrantHomeCityNames[7], null, cityCitizensCount[7])
            };

            //for (int i = 0; i < homeCitiesCount; i++)
            //{
            //    randomIndex = _random.RandomNumber(0, DemoImmigrantHomeCity.Count + 1);
            //    City homeCity = new City(, DemoImmigrants, DemoPoliceOfficers, cityCitizensCount[0]);
            //}
        }

        private void InitialzeHomeCountries()
        {
            _demoImmigrantHomeCountries = new List<Country>(5)
            {
                new Country(DemoImmigrantHomeCountryNames[0], new List<City>() { _demoImmigrantHomeCities[0] }),
                new Country(DemoImmigrantHomeCountryNames[1], new List<City>() { _demoImmigrantHomeCities[1] }),
                new Country(DemoImmigrantHomeCountryNames[2], new List<City>() { _demoImmigrantHomeCities[2] }),
                new Country(DemoImmigrantHomeCountryNames[3], new List<City>() { _demoImmigrantHomeCities[3] }),
                new Country(DemoImmigrantHomeCountryNames[4], new List<City>() { _demoImmigrantHomeCities[4] }),
                new Country(DemoImmigrantHomeCountryNames[5], new List<City>() { _demoImmigrantHomeCities[5] }),
                new Country(DemoImmigrantHomeCountryNames[6], new List<City>() { _demoImmigrantHomeCities[6] }),
                new Country(DemoImmigrantHomeCountryNames[7], new List<City>() { _demoImmigrantHomeCities[7] })
            };
        }

        private void InitialzeCounty()
        {
            DemoCountry = new Country("Germany", DemoCities);
        }
    }
}
