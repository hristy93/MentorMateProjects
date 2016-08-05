using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public abstract class Immigrant
    {
        protected abstract Passport Passport { get; set; }
        protected decimal Money { get; set; }
        protected City City { get; set; }
        protected Country Country { get; set; }
        protected PoliceOfficer DelegatedPpoliceOfficer { get; set; } = null;

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

        public virtual void BuyWeapon()
        {

        }

        public virtual void MigrateToAnotherCity()
        {
            City = Country.GetRandomCity();
            City.DelegatePoliceOfficerToImmigrant(this);
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