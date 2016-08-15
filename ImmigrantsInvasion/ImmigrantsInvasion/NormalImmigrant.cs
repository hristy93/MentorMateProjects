using System;
using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    class NormalImmigrant : Immigrant, IImmigrate
    {
        public const int MAX_SIBLINGS_COUNT = 10;

        public override List<Immigrant> Family { get; protected set; } = new List<Immigrant>(MAX_SIBLINGS_COUNT);
        //protected override Passport Passport { get; set; }
        public override List<Weapon> Weapons { get; protected set; } = null;

        public NormalImmigrant(
         string immigrantName,
         byte immigrantAge,
         Country immigrantHomeCountry,
         City immigrantHomeCity,
         decimal immigrantMoney) : base(immigrantName,
                                        immigrantAge,
                                        immigrantHomeCountry,
                                        immigrantHomeCity,
                                        immigrantMoney)
        {
            Type = ImmigrantTypes.Normal;
        }

        public override void AddFamilyMember(Immigrant immigrantSibling)
        {
            if (Family.Count + 1 > MAX_SIBLINGS_COUNT)
            {
                throw new InvalidOperationException($"Unable to add a new family member to immigrant because his/her siblings count will exceeds {MAX_SIBLINGS_COUNT}!");
            }
            Family.Add(immigrantSibling);
        }
    }
}
