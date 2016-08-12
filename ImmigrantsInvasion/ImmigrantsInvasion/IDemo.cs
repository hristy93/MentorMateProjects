using System.Collections.Generic;

namespace ImmigrantsInvasion
{
    public interface IDemo
    {
        List<City> DemoCities { get; }
        Country DemoCountry { get; }
        List<Immigrant> DemoImmigrants { get; }
        List<PoliceOfficer> DemoPoliceOfficers { get; }
        List<Weapon> DemoWeapons { get; }

        void DisplayImmigrantsStatistics();
        void ImmigrateAll();
        void UnleashImmigrantsKillingSpree();
    }
}