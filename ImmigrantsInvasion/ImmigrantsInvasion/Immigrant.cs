using System;
using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        public const int MONEY_TOP_LIMIT = 1000;
        public const int MONEY_BOTTOM_LIMIT = 500;

        public Passport Passport { get; protected set; }
        public decimal Money { get; protected set; }
        public City CurrentCity { get; protected set; }
        public Country CurrentCountry { get; protected set; }
        public PoliceOfficer DelegatedPoliceOfficer { get; protected set; }
        public bool hasImmigrated { get; protected set; } = false;
        public bool isDead { get; set; } = false;
        //public bool isCaught { get; set; } = false;
        private bool _isCaught = false;
        public static List<Immigrant> _immigrationBuffer = new List<Immigrant>();
        private static int _immigrantIndex = 0;

        public bool IsCaught
        {
            get { return _isCaught; }
            set { _isCaught = value; }
        }


        protected WeaponsCollection WeaponsCollectionInstance = WeaponsCollection.Instance();
        protected RandomGenerator RandomGeneratorInstance = RandomGenerator.Instance;

        public abstract List<Immigrant> Family { get; protected set; }
        public abstract List<Weapon> Weapons { get; protected set; }

        public Immigrant(
            string immigrantName,
            byte immigrantAge,
            Country immigrantHomeCountry,
            City immigrantHomeCity,
            decimal immigrantMoney
            )
        {
            Passport = new Passport(immigrantName, immigrantAge, immigrantHomeCountry, immigrantHomeCity);
            Money = immigrantMoney;
            CurrentCity = immigrantHomeCity;
            CurrentCountry = immigrantHomeCountry;
        }

        public Immigrant(
           Country immigrantHomeCountry,
           City immigrantHomeCity,
           decimal immigrantMoney
           )
        {
            //Passport = new Passport(immigrantName, immigrantAge, immigrantHomeCountry, immigrantHomeCity);
            Passport = null;
            Money = immigrantMoney;
            CurrentCity = immigrantHomeCity;
            CurrentCountry = immigrantHomeCountry;
        }

        public virtual void ImmigrateToAnotherCountry(Country countryToImmigrate, List<City> citiesToImmigrate)
        {
            //if (!hasImmigrated && !isCaught)
            //{
            City cityToImmigrate = countryToImmigrate.GetRandomCity();
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);
            string oldCityName = CurrentCity.Name;

            if (!_immigrationBuffer.Contains(this))
            {
                _immigrationBuffer.Add(this);
            }
            else
            {
                //return;
            }

            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if ((!sibling.hasImmigrated && !sibling.IsCaught) || !sibling.IsCaught)
                    {
                        if (!_immigrationBuffer.Contains(sibling))
                        {
                            _immigrationBuffer.Add(sibling);
                            sibling.ImmigrateToAnotherCountry(countryToImmigrate, cityToImmigrate);
                        }
                        else
                        {
                            //return;
                        }
                    }
                }
            }

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"#{++_immigrantIndex}   A police officer caught the illegal immigrant with {Money} euro and prevented him from entering {cityToImmigrate.Name}");
                IsCaught = true;
                return;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            DisplayImmigrantInformationAfterRelocation(oldCityName);
            hasImmigrated = true;
            //}
        }

        private void DisplayImmigrantInformationAfterRelocation(string oldCityName)
        {
            Console.Write(String.Format("#{0}   The immigrant who relocated from {1} to {2}, {3}, {4} and {5}.\n",
                ++_immigrantIndex,
                oldCityName,
                CurrentCity.Name,
                hasPassport() ? "doesn't have a passport" : "has a passport",
                hasMoney() ? "doesn't have money" : $"has {Money} euro",
                hasWeapons() ? "doesn't have weapons" : "has weapons"
            ));
        }

        public virtual void ImmigrateToAnotherCountry(Country countryToImmigrate, City cityToImmigrate)
        {
            //if (!hasImmigrated && !isCaught)
            //{
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);
            string oldCityName = CurrentCity.Name;

            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if ((!sibling.hasImmigrated && !sibling.IsCaught) || !sibling.IsCaught)
                    {
                        if (!_immigrationBuffer.Contains(sibling))
                        {
                            _immigrationBuffer.Add(sibling);
                            sibling.ImmigrateToAnotherCountry(countryToImmigrate, cityToImmigrate);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"#{++_immigrantIndex}   A police officer caught the illegal immigrant with {Money} euro and prevented him from entering {cityToImmigrate.Name}");
                IsCaught = true;
                return;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            DisplayImmigrantInformationAfterRelocation(oldCityName);
            hasImmigrated = true;
            //}
        }

        public virtual bool TryToMigrateToAnotherCity(City cityToImmigrate)
        {
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"   A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                IsCaught = true;
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCity.AddImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if (!sibling.hasImmigrated && !sibling.IsCaught)
                    {
                        //sibling.TryToMigrateToAnotherCity(cityToImmigrate);
                        //City.DelegatePoliceOfficerToImmigrant(sibling);
                    }
                }
            }

            hasImmigrated = true;
            return true;
        }

        public virtual void DelegatePoliceOfficer(PoliceOfficer delegatedPoliceOfficer) => DelegatedPoliceOfficer = delegatedPoliceOfficer;

        public virtual void AddFamilyMember(Immigrant immigrantSibling) => Family.Add(immigrantSibling);

        public virtual void RemoveFamilyMember(Immigrant immigrantSibling) => Family.Remove(immigrantSibling);

        public bool HasBombs() => Weapons.Exists(w => w.Type == WeaponTypes.Bomb);

        public bool hasPassport() => Passport == null;

        public bool hasMoney() => Money == 0.0m;

        public bool hasWeapons() => Weapons == null;
    }
}