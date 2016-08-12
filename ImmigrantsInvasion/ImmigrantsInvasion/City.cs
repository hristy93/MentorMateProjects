using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmigrantsInvasion
{
    public class City
    {
        private static RandomGenerator _random = RandomGenerator.Instance;

        public List<Immigrant> Immigrants { get; private set; } = new List<Immigrant>();
        public List<PoliceOfficer> PoliceOfficers { get; private set; } = new List<PoliceOfficer>();
        public string Name { get; private set; }
        public int CitizensCount { get; private set; }

        public City(
            string citizenName,
            int cityCitizensCount
            )
        {
            Name = citizenName;
            if (CitizensCount <= 0)
            {
                throw new InvalidOperationException("Unable to create this city because it has no citizens!");
            }

            CitizensCount = cityCitizensCount;
        }
     
        public void DelegatePoliceOfficerToImmigrant(Immigrant immigrant)
        {
            PoliceOfficer delegatedPoliceOfficer = GetRandomPoliceOfficer();
            immigrant.DelegatePoliceOfficer(delegatedPoliceOfficer);
        }

        public PoliceOfficer GetRandomPoliceOfficer()
        {
            return PoliceOfficers.ElementAtOrDefault(_random.RandomNumber(0, PoliceOfficers.Count));
        }

        public void AddPoliceOfficers(List<PoliceOfficer> policeOfficers)
        {
            PoliceOfficers = policeOfficers;
        }

        public void RemovePoliceOfficers()
        {
            PoliceOfficers.Clear();
        }

        public void AddPoliceOfficers(PoliceOfficer policeOfficer)
        {
            PoliceOfficers.Add(policeOfficer);
        }

        public void RemovePoliceOfficers(PoliceOfficer policeOfficer)
        {
            PoliceOfficers.Remove(policeOfficer);
        }

        public void AddImmigrant(Immigrant immigrant)
        {
            Immigrants.Add(immigrant);
        }

        public void RemoveImmigrant(Immigrant immigrant)
        {
            Immigrants.Remove(immigrant);
        }
    }
}
