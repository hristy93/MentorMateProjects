using System;
using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        public Passport Passport { get; protected set; }
        public decimal Money { get; protected set; }
        public City CurrentCity { get; protected set; }
        public Country CurrentCountry { get; protected set; }
        public PoliceOfficer DelegatedPoliceOfficer { get; protected set; }
        public bool hasImmigrated { get; protected set; } = false;
        public bool isDead { get; set; } = false;
        protected WeaponsCollection WeaponsCollectionInstance = WeaponsCollection.Instance();
        protected RandomGenerator RandomGeneratorInstance = RandomGenerator.Instance;

        protected abstract List<Immigrant> Family { get; set; } 
        protected abstract List<Weapon> Weapons { get; set; }

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

        public virtual bool TryToMigrateToAnotherCity(Country countryToImmigrate, List<City> citiesToImmigrate)
        {
            City cityToImmigrate = countryToImmigrate.GetRandomCity();
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCountry = countryToImmigrate;
            CurrentCity.AddImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    //sibling.MigrateToAnotherCity(countryToImmigrate, citiesToImmigrate);
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }

            hasImmigrated = true;
            return true;
        }

        public virtual bool MigrateToAnotherCity(List<City> citiesToImmigrate)
        {
            City cityToImmigrate = CurrentCountry.GetRandomCity();
            cityToImmigrate.DelegatePoliceOfficerToImmigrant(this);

            if (!DelegatedPoliceOfficer.CheckImmigrant(this))
            {
                Console.WriteLine($"A police officer caught the illegal immigrant and prevented him from entering {cityToImmigrate.Name}");
                return false;
            }

            CurrentCity = cityToImmigrate;
            CurrentCity.AddImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    //sibling.MigrateToAnotherCity(countryToImmigrate, citiesToImmigrate);
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }

            hasImmigrated = true;
            return true;
        }

        public void DelegatePoliceOfficer(PoliceOfficer delegatedPoliceOfficer)
        {
            DelegatedPoliceOfficer = delegatedPoliceOfficer;
        }

        public void AddFamilyMember(Immigrant immigrantSibling)
        {
            Family.Add(immigrantSibling);
        }

        public void RemoveFamilyMember(Immigrant immigrantSibling)
        {
            Family.Remove(immigrantSibling);
        }

        public bool HasBombs()
        {
            return Weapons.Exists(w => w.Type == WeaponTypes.Bomb);
        }
    }
}