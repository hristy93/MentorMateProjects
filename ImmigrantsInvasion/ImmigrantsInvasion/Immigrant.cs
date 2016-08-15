using System;
using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        public const int MONEY_TOP_LIMIT = 1000;
        public const int MONEY_BOTTOM_LIMIT = 500;

        private static List<Immigrant> _immigrationBuffer = new List<Immigrant>();
        private static int _immigrantIndex = 0;

        public Passport Passport { get; protected set; }
        public decimal Money { get; protected set; }
        public City CurrentCity { get; protected set; }
        public Country CurrentCountry { get; protected set; }
        public PoliceOfficer DelegatedPoliceOfficer { get; protected set; }
        public bool HasImmigrated { get; protected set; } = false;
        public bool IsDead { get; set; } = false;
        public bool IsCaught { get; set; } = false;
        public ImmigrantTypes Type { get; set; } = ImmigrantTypes.None;

        //private bool _isCaught = false;
        //public bool IsCaught
        //{
        //    get { return _isCaught; }
        //    set { _isCaught = value; }
        //}


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

            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if (!(sibling.HasImmigrated ^ sibling.IsCaught))
                    {
                        if (!_immigrationBuffer.Contains(sibling))
                        {
                            _immigrationBuffer.Add(sibling);
                            sibling.ImmigrateToAnotherCountry(countryToImmigrate, cityToImmigrate);
                        }
                    }
                }
            }

            if (!PassTheBorder(cityToImmigrate))
            {
                return;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            DisplayImmigrantInformationAfterRelocation(oldCityName);
            HasImmigrated = true;
            //}
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
                    if (!(sibling.HasImmigrated ^ sibling.IsCaught))
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

            if (!PassTheBorder(cityToImmigrate))
            {
                return;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            DisplayImmigrantInformationAfterRelocation(oldCityName);
            HasImmigrated = true;
            //}
        }

        public virtual void MigrateToAnotherCity(City cityToImmigrate)
        {
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);
            string oldCityName = CurrentCity.Name;

            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if (!(sibling.HasImmigrated ^ sibling.IsCaught))
                    {
                        if (!_immigrationBuffer.Contains(sibling))
                        {
                            _immigrationBuffer.Add(sibling);
                            sibling.ImmigrateToAnotherCountry(CurrentCountry, cityToImmigrate);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            if (!PassTheBorder(cityToImmigrate))
            {
                return;
            }

            CurrentCity = cityToImmigrate;
            CurrentCity.AddImmigrant(this);
            DisplayImmigrantInformationAfterRelocation(oldCityName);
            HasImmigrated = true;
        }

        public virtual void DelegatePoliceOfficer(PoliceOfficer delegatedPoliceOfficer) => DelegatedPoliceOfficer = delegatedPoliceOfficer;

        public virtual void AddFamilyMember(Immigrant immigrantSibling) => Family.Add(immigrantSibling);

        public virtual void RemoveFamilyMember(Immigrant immigrantSibling)
        {
            if (Family.Count == 0)
            {
                throw new InvalidOperationException("Unable to remove the family member because the immigrant has no family!");
            }
            else if (!Family.Contains(immigrantSibling))
            {
                throw new InvalidOperationException("Unable to remove the family member because he/she is not part of this immigrant's family!");
            }

            Family.Remove(immigrantSibling);
        }

        public bool HasBombs => Weapons.Exists(w => w.Type == WeaponTypes.Bomb);

        public bool HasPassport() => Passport == null;

        public bool HasMoney() => Money == 0.0m;

        public bool HasWeapons() => Weapons == null;

        protected virtual bool PassTheBorder(City cityToImmigrate)
        {
            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"#{++_immigrantIndex}   A police officer caught the illegal immigrant with {Money} euro and prevented him from entering {cityToImmigrate.Name}");
                IsCaught = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        protected virtual void DisplayImmigrantInformationAfterRelocation(string oldCityName)
        {
            Console.Write(String.Format("#{0}   The immigrant who relocated from {1} to {2}, {3}, {4} and {5}.\n",
                ++_immigrantIndex,
                oldCityName,
                CurrentCity.Name,
                HasPassport() ? "doesn't have a passport" : "has a passport",
                HasMoney() ? "doesn't have money" : $"has {Money} euro",
                HasWeapons() ? "doesn't have weapons" : "has weapons"
            ));
        }
    }
}