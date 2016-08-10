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
        public bool isCaught { get; set; } = false;

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

        public virtual bool TryToMigrateToAnotherCountry(Country countryToImmigrate, List<City> citiesToImmigrate)
        {
            City cityToImmigrate = countryToImmigrate.GetRandomCity();
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    if (!sibling.hasImmigrated && !sibling.isCaught)
                    {
                        sibling.TryToMigrateToAnotherCountry(countryToImmigrate, cityToImmigrate);
                    }
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"   A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                isCaught = true;
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            hasImmigrated = true;
            return true;
        }

        public virtual bool TryToMigrateToAnotherCountry(Country countryToImmigrate, City cityToImmigrate)
        {
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"   A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                isCaught = true;
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            //if (Family != null)
            //{
            //    foreach (var sibling in Family)
            //    {
            //        if (!sibling.hasImmigrated && !sibling.isCaught)
            //          sibling.TryToMigrateToAnotherCountry(countryToImmigrate, cityToImmigrate);
            //          //sibling.TryToMigrateToAnotherCity(cityToImmigrate);
            //          //City.DelegatePoliceOfficerToImmigrant(sibling);
            //        }
            //    }
            //}

            hasImmigrated = true;
            return true;
        }

        public virtual bool TryToMigrateToAnotherCity(City cityToImmigrate)
        {
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"   A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                isCaught = true;
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCity.AddImmigrant(this);
            if (Family != null)
            {

                foreach (var sibling in Family)
                {
                    if (!sibling.hasImmigrated && !sibling.isCaught)
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