using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        public Passport Passport { get; protected set; }
        public decimal Money { get; protected set; }
        public City CurrentCity { get; protected set; }
        public Country CurrentCountry { get; protected set; }
        protected PoliceOfficer DelegatedPoliceOfficer { get; set; } = null;
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

        public virtual void MigrateToAnotherCity(Country countryToImmigrate, List<City> citiesToImmigrate)
        {
            CurrentCity = countryToImmigrate.GetRandomCity();
            CurrentCity.DelegatePoliceOfficerToImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    //sibling.MigrateToAnotherCity(countryToImmigrate, citiesToImmigrate);
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }
        }

        public virtual void MigrateToAnotherCity(List<City> citiesToImmigrate)
        {
            CurrentCity = CurrentCountry.GetRandomCity();
            CurrentCity.DelegatePoliceOfficerToImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    //sibling.MigrateToAnotherCity(citiesToImmigrate);
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }
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
    }
}