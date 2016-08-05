using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public class Immigrant
    {
        public Passport Passport { get; private set; }
        public decimal Money { get; private set; }
        public City City { get; private set; }
        public Country Country { get; private set; }

        public IList<Immigrant> family;
        public IList<Weapon> weapons;

        public Immigrant(
            Passport immigrantPassport,
            decimal immigrantMoney,
            City immigrantCity,
            Country immigrantCountry,
            IList<Immigrant> immigrantfamily,
            IList<Weapon> immigrantweapons
            )
        {
            Passport = immigrantPassport;
            Money = immigrantMoney;
            City = immigrantCity;
            Country = immigrantCountry;
            family = immigrantfamily;
            weapons = immigrantweapons;
        }
    }
}