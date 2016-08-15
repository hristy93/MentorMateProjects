using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmigrantsInvasion
{
    public class Demo : IDemo
    {
        private RandomGenerator _random = RandomGenerator.Instance;
        private List<City> _demoImmigrantHomeCities;
        private List<Country> _demoImmigrantHomeCountries;
        private int _extremistsCount => DemoImmigrants.OfType<ImmigrantExtremist>().Count();
        private int _radicalsCount => DemoImmigrants.OfType<RadicalImmigrant>().Count();
        private int _normalsCount => DemoImmigrants.OfType<NormalImmigrant>().Count();
        private int _illegalsCount => _radicalsCount + _extremistsCount;
        private int _numberOfWeaponsToBuy;
        private int _numberOfSibling;
        private ImmigrantOrigin _immigrantOrigin;
        private ImmigrantDestination _immigrantDestination;
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
            _immigrantOrigin = new ImmigrantOrigin(ImmigrantOriginOptions.MiddleEast);
            _immigrantDestination = new ImmigrantDestination(ImmigrantDestinationOptions.Germany, citiesCount);
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
            InitializePoliceOfficers(immigrantsCount);
            InitializeWeapons(weaponsCount);
            InitializeHomeCities();
            InitializeHomeCountries();
            InitializeImmigrants(immigrantsCount);
            InitializeCities(citiesCount);
            InitializeCountry();
        }

        private void InitializeWeapons(int weaponsCount)
        {
            WeaponsCollection weaponsCollection = WeaponsCollection.Instance(weaponsCount);
        }

        private void InitializePoliceOfficers(int immigrantsCount)
        {
            DemoPoliceOfficers = new List<PoliceOfficer>(immigrantsCount);
            for (int i = 0; i < immigrantsCount; i++)
            {
                string policeOfficerName = _immigrantDestination.PoliceOfficersNames[_random.RandomNumber(0, _immigrantDestination.PoliceOfficersNames.Count)];
                PoliceOfficerTypes policeOfficerType = _random.Propability(0.5) ? PoliceOfficerTypes.Policeman : PoliceOfficerTypes.SpecialForces;
                DemoPoliceOfficers.Add(new PoliceOfficer(policeOfficerName, policeOfficerType));
            }
        }

        private void InitializeImmigrants(int immigrantsCount)
        {
            int randomNameIndex;
            int randomHomeIndex;
            DemoImmigrants = new List<Immigrant>(immigrantsCount);
            ImmigrantCreator immigrantCreator = new ImmigrantCreator();
            for (int i = 0; i < immigrantsCount; i++)
            {
                Immigrant immigrant;
                randomNameIndex = _random.RandomNumber(0, _immigrantOrigin.ImmigrantNames.Count);
                randomHomeIndex = _random.RandomNumber(0, _immigrantOrigin.ImmigrantHomeCityNames.Count);
                if (_random.Propability(_immigrantCreationPropability.NormalImmigrantPropability))
                {
                    immigrant = immigrantCreator.CreateImmigrant(
                        ImmigrantTypes.Normal,
                        _immigrantOrigin.ImmigrantNames[randomNameIndex],
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
                            _immigrantOrigin.ImmigrantNames[randomNameIndex],
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

        private void InitializeCities(int citiesCount)
        {
            DemoCities = _immigrantDestination.Cities;
            AddPoliceOfficersToCities();
        }

        private void AddPoliceOfficersToCities()
        {
            foreach (var city in DemoCities)
            {
                city.AddPoliceOfficers(DemoPoliceOfficers);
            }
        }

        private void InitializeHomeCities() => _demoImmigrantHomeCities = _immigrantOrigin.ImmigrantHomeCities;

        private void InitializeHomeCountries() => _demoImmigrantHomeCountries = _immigrantOrigin.ImmigrantHomeCountries;

        private void InitializeCountry() => DemoCountry = _immigrantDestination.Country;
    }
}