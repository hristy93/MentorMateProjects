using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        protected Passport Passport { get; set; }
        protected decimal Money { get; set; }
        protected City CurrentCity { get; set; }
        protected Country CurrentCountry { get; set; }
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

        public virtual void MigrateToAnotherCity()
        {
            CurrentCity = CurrentCountry.GetRandomCity();
            CurrentCity.DelegatePoliceOfficerToImmigrant(this);
            if (Family != null)
            {
                foreach (var sibling in Family)
                {
                    sibling.MigrateToAnotherCity();
                    //City.DelegatePoliceOfficerToImmigrant(sibling);
                }
            }
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