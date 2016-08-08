using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        protected abstract Passport Passport { get; set; }
        protected decimal Money { get; set; }
        protected City CurrentCity { get; set; }
        protected Country CurrentCountry { get; set; }
        protected PoliceOfficer DelegatedPoliceOfficer { get; set; } = null;
        protected WeaponsCollection weaponsCollection = WeaponsCollection.Instance();
        protected RandomGenerator random = RandomGenerator.Instance;

        protected abstract List<Immigrant> Family { get; set; } 
        protected abstract List<Weapon> Weapons { get; set; }

        //public Immigrant(
        //    Passport immigrantPassport,
        //    decimal immigrantMoney,
        //    City immigrantCity,
        //    Country immigrantCountry,
        //    IList<Immigrant> immigrantfamily,
        //    IList<Weapon> immigrantweapons
        //    )
        //{
        //    Passport = immigrantPassport;
        //    Money = immigrantMoney;
        //    City = immigrantCity;
        //    Country = immigrantCountry;
        //    family = immigrantfamily;
        //    weapons = immigrantweapons;
        //}

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
    }
}