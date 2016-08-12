using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmigrantsInvasion
{
    public class Demo : IDemo
    {
        private RandomGenerator _random = RandomGenerator.Instance;
        private List<City> _demoImmigrantHomeCities { get; set; }
        private List<Country> _demoImmigrantHomeCountries { get; set; }
        private List<string> _demoCityNamesToImmigrate = new List<string>()
        {
            "Berlin", "Frankfurt", "Bonn", "Dresden", "Hamburg"
        };
        private List<string> _demoImmigrantHomeCountryNames = new List<string>()
        {
            "Syria", "Lebanon", "Jordan", "Iraq", "Afghanistan", "Pakistan", "Turkmenistan", "Saudi Arabia"
        };
        private List<string> _demoImmigrantHomeCityNames = new List<string>()
        {
            "Damascus", "Beirut", "Ammam", "Baghdad", "Kabul", "Lahore", "Ashgabat", "Riyadh"
        };
        private List<string> _demoImmigrantNames = new List<string>()
        {
            "Atidge", "Bilem", "Rahmud", "Mustafa", "Fatme", "Rahmud", "Yaman", "Mirra"
        };
        private List<string> _demoPoliceOfficersNames = new List<string>()
        {
            "Hanna", "Bill", "Jonas", "Finn", "Emilly", "Luca", "Yaman", "Marie", "Sofia", "Ben"
        };
        private int _extremistsCount => DemoImmigrants.OfType<ImmigrantExtremist>().Count();
        private int _radicalsCount => DemoImmigrants.OfType<RadicalImmigrant>().Count();
        private int _normalsCount => DemoImmigrants.OfType<NormalImmigrant>().Count();
        private int _illegalsCount => _radicalsCount + _extremistsCount;
        private int _numberOfWeaponsToBuy;
        private int _numberOfSibling;
        private ImmigrantCreationPropability _immigrantCreationPropability;

        public Country DemoCountry { get; private set; }
        public List<City> DemoCities { get; private set; }
        public List<Immigrant> DemoImmigrants { get; private set; }
        public List<PoliceOfficer> DemoPoliceOfficers { get; private set; }
        public List<Weapon> DemoWeapons { get; private set; }

        public Demo(ImmigrantCreationPropability immigrantCreationPropability, int immigrantsCount, int citiesCount, int numberOfWeaponsToCreate, int numberOfWeaponsToBuy, int numberOfSiblings)
        {
            ValidateDemoInputParameters(numberOfWeaponsToBuy, numberOfSiblings);
            _numberOfWeaponsToBuy = numberOfWeaponsToBuy;
            _numberOfSibling = numberOfSiblings;
            _immigrantCreationPropability = immigrantCreationPropability;
            InitializeDemo(immigrantsCount, citiesCount, numberOfWeaponsToCreate);
        }

        public void DisplayImmigrantsStatistics()
        {
            Console.WriteLine($"-----------");
            Console.WriteLine($"Immigrants' Statistics");
            Console.WriteLine($"-----------");
            Console.WriteLine($"   Normal immigrants: {_normalsCount}");
            Console.WriteLine($"   Immigrant extremists: {_extremistsCount}");
            Console.WriteLine($"   Radical immigrants: {_radicalsCount}");
            Console.WriteLine($"   Total illegal immigrants: {_illegalsCount}");
            Console.WriteLine($"-----------\n");
        }

        public void ImmigrateAll()
        {
            Console.WriteLine($"-----------");
            Console.WriteLine($"Immigrants' Relocation Information");
            Console.WriteLine($"-----------");
            foreach (var immigrant in DemoImmigrants)
            {
                if (!(immigrant.HasImmigrated ^ immigrant.IsCaught))
                {
                    immigrant.ImmigrateToAnotherCountry(DemoCountry, DemoCities);
                }
            }

            Console.WriteLine($"-----------");
            Console.WriteLine($"The police officers caught {DemoImmigrants.Where(i => i.IsCaught == true).Count()} of {_illegalsCount} illegal immigrants ");
            Console.WriteLine($"-----------\n");
        }

        public void UnleashImmigrantsKillingSpree()
        {
            Console.WriteLine($"-----------");
            Console.WriteLine($"Emergency News: The immigrant's killing spree has started!!!");
            Console.WriteLine($"-----------\n");

            ////int oldCityCount;
            //var immigrantsAbletoKill = DemoImmigrants.Select(c => c as IKillPeople).Where(s => s != null);
            //foreach (var immigrant in immigrantsAbletoKill)
            //{
            //    if (!(immigrant as Immigrant).isDead)
            //    {
            //        //oldCityCount = DemoCountry.Cities.Count;
            //        immigrant.KillPeople(_numberOfWeaponsToBuy);
            //        //newCityCount = DemoCountry.Cities.Count;
            //        if (DemoCountry.Cities.Count == 0)
            //        {
            //            Console.WriteLine($"The illegal immigrants destroyed all cities in {(immigrant as Immigrant).CurrentCountry.Name}!");
            //            break;
            //        }

            //        if ((immigrant as Immigrant).HasBombs)
            //        {
            //            MarkTheDeadImmigrants(immigrant);
            //        }
            //    }
            //}

            foreach (var immigrant in DemoImmigrants)
            {
                IKillPeople immigrantAbleToKill = immigrant as IKillPeople;
                if (!immigrant.IsDead && immigrantAbleToKill != null)
                {
                    immigrantAbleToKill.KillPeople(_numberOfWeaponsToBuy);
                    if (DemoCountry.Cities.Count == 0)
                    {
                        Console.WriteLine($"Emergency news! The illegal immigrants destroyed all cities in {immigrant.CurrentCountry.Name}!");
                        break;
                    }

                    if (immigrant.HasBombs)
                    {
                        MarkTheDeadImmigrants(immigrant);
                    }
                }
            }
        }

        private void MarkTheDeadImmigrants(Immigrant immigrant)
        {
            var deadImmigrants = immigrant.CurrentCity.Immigrants;
            DemoImmigrants.Where(d => deadImmigrants.Contains(d)).All(a => { a.IsDead = true; return true; });
            //demo.DemoImmigrants.RemoveAll(j => deadImmigrants.Contains(j));
        }

        private static void ValidateDemoInputParameters(int numberOfWeaponsToBuy, int numberOfSiblings)
        {
            if (numberOfWeaponsToBuy > RadicalImmigrant.MAX_WEAPONS_COUNT)
            {
                throw new InvalidOperationException($"Unable to buy these weapons because their count will exceeds {RadicalImmigrant.MAX_WEAPONS_COUNT}!");
            }
            if (numberOfSiblings > NormalImmigrant.MAX_SIBLINGS_COUNT)
            {
                throw new InvalidOperationException($"Unable to create a normal immigrant because his/her siblings count will exceeds {NormalImmigrant.MAX_SIBLINGS_COUNT}!");
            }
        }

        private void InitializeDemo(int immigrantsCount, int citiesCount, int weaponsCount)
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
                string policeOfficerName = _demoPoliceOfficersNames[_random.RandomNumber(0, _demoPoliceOfficersNames.Count)];
                PoliceOfficerTypes policeOfficerType = _random.Propability(0.5) ? PoliceOfficerTypes.Policeman : PoliceOfficerTypes.SpecialForces;
                DemoPoliceOfficers.Add(new PoliceOfficer(policeOfficerName, policeOfficerType));
            }
        }

        private void InitialzeImmigrants(int immigrantsCount)
        {
            int randomNameIndex;
            int randomHomeIndex;

            DemoImmigrants = new List<Immigrant>(immigrantsCount);
            ImmigrantCreator immigrantCreator = new ImmigrantCreator();

            for (int i = 0; i < immigrantsCount; i++)
            {
                Immigrant immigrant;
                randomNameIndex = _random.RandomNumber(0, _demoImmigrantNames.Count);
                randomHomeIndex = _random.RandomNumber(0, _demoImmigrantHomeCityNames.Count);
                if (_random.Propability(_immigrantCreationPropability.NormalImmigrantPropability))
                {
                    immigrant = immigrantCreator.CreateImmigrant(
                        ImmigrantTypes.Normal,
                        _demoImmigrantNames[randomNameIndex],
                        (byte)_random.RandomNumber(10, 65),
                        _demoImmigrantHomeCountries[randomHomeIndex],
                        _demoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(Immigrant.MONEY_BOTTOM_LIMIT, Immigrant.MONEY_TOP_LIMIT)
                        );
                }
                else if (_random.Propability(_immigrantCreationPropability.ImmigrantExtremistPropability))
                {
                    immigrant = immigrantCreator.CreateImmigrant(
                        ImmigrantTypes.Extremist,
                        _demoImmigrantHomeCountries[randomHomeIndex],
                        _demoImmigrantHomeCities[randomHomeIndex],
                        _random.RandomNumber(Immigrant.MONEY_BOTTOM_LIMIT, Immigrant.MONEY_TOP_LIMIT)
                        );
                }
                else
                {
                    if (_random.Propability(_immigrantCreationPropability.RadicalImmigrantPropability))
                    {
                        immigrant = immigrantCreator.CreateImmigrant(
                            ImmigrantTypes.Radical,
                            _demoImmigrantNames[randomNameIndex],
                            (byte)_random.RandomNumber(10, 65),
                            _demoImmigrantHomeCountries[randomHomeIndex],
                            _demoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(Immigrant.MONEY_BOTTOM_LIMIT, Immigrant.MONEY_TOP_LIMIT)
                            );
                    }
                    else
                    {
                        immigrant = immigrantCreator.CreateImmigrant(
                            ImmigrantTypes.Radical,
                            _demoImmigrantHomeCountries[randomHomeIndex],
                            _demoImmigrantHomeCities[randomHomeIndex],
                            _random.RandomNumber(Immigrant.MONEY_BOTTOM_LIMIT, Immigrant.MONEY_TOP_LIMIT)
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
            }
        }

        private void AddRandomImmigrantFamilyMember(Immigrant immigrant)
        {
            int immigrantIndex = DemoImmigrants.IndexOf(immigrant);
            int randomImmigrantIndex;
            do
            {
                randomImmigrantIndex = _random.RandomNumber(0, DemoImmigrants.Count);
            }

            while (immigrantIndex == randomImmigrantIndex);
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
                new City(_demoCityNamesToImmigrate[0], cityCitizensCount[0]),
                new City(_demoCityNamesToImmigrate[1], cityCitizensCount[1]),
                new City(_demoCityNamesToImmigrate[2], cityCitizensCount[2]),
                new City(_demoCityNamesToImmigrate[3], cityCitizensCount[3]),
                new City(_demoCityNamesToImmigrate[4], cityCitizensCount[4])
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
            int homeCitiesCount = _demoImmigrantHomeCityNames.Count;
            List<int> cityCitizensCount = new List<int>(homeCitiesCount);
            for (int i = 0; i < 8; i++)
            {
                cityCitizensCount.Add(_random.RandomNumber(500, 1000));
            }

            _demoImmigrantHomeCities = new List<City>(5)
            {
                new City(_demoImmigrantHomeCityNames[0], cityCitizensCount[0]),
                new City(_demoImmigrantHomeCityNames[1], cityCitizensCount[1]),
                new City(_demoImmigrantHomeCityNames[2], cityCitizensCount[2]),
                new City(_demoImmigrantHomeCityNames[3], cityCitizensCount[3]),
                new City(_demoImmigrantHomeCityNames[4], cityCitizensCount[4]),
                new City(_demoImmigrantHomeCityNames[5], cityCitizensCount[5]),
                new City(_demoImmigrantHomeCityNames[6], cityCitizensCount[6]),
                new City(_demoImmigrantHomeCityNames[7], cityCitizensCount[7])
            };
        }

        private void InitialzeHomeCountries()
        {
            _demoImmigrantHomeCountries = new List<Country>(5)
            {
                new Country(_demoImmigrantHomeCountryNames[0], new List<City>() { _demoImmigrantHomeCities[0] }),
                new Country(_demoImmigrantHomeCountryNames[1], new List<City>() { _demoImmigrantHomeCities[1] }),
                new Country(_demoImmigrantHomeCountryNames[2], new List<City>() { _demoImmigrantHomeCities[2] }),
                new Country(_demoImmigrantHomeCountryNames[3], new List<City>() { _demoImmigrantHomeCities[3] }),
                new Country(_demoImmigrantHomeCountryNames[4], new List<City>() { _demoImmigrantHomeCities[4] }),
                new Country(_demoImmigrantHomeCountryNames[5], new List<City>() { _demoImmigrantHomeCities[5] }),
                new Country(_demoImmigrantHomeCountryNames[6], new List<City>() { _demoImmigrantHomeCities[6] }),
                new Country(_demoImmigrantHomeCountryNames[7], new List<City>() { _demoImmigrantHomeCities[7] })
            };
        }

        private void InitialzeCounty() => DemoCountry = new Country("Germany", DemoCities);
    }
}
