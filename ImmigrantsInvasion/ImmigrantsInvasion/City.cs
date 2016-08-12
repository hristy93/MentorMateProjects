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
            if (cityCitizensCount <= 0)
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
            if (PoliceOfficers.Count == 0)
            {
                throw new InvalidOperationException("Unable to remove a police officer because there is none of them in the city!");
            }
            else if (!PoliceOfficers.Contains(policeOfficer))
            {
                throw new InvalidOperationException("Unable to remove a police officer because he/she is not from this city!");
            }
            PoliceOfficers.Remove(policeOfficer);
        }

        public void AddImmigrant(Immigrant immigrant)
        {
            Immigrants.Add(immigrant);
        }

        public void RemoveImmigrant(Immigrant immigrant)
        {
            if (Immigrants.Count == 0)
            {
                throw new InvalidOperationException("Unable to remove this immigrant because there is none of them in the city!");
            }
            else if (!Immigrants.Contains(immigrant))
            {
                throw new InvalidOperationException("Unable to remove this immigrant because he/she is not from this city!");
            }

            Immigrants.Remove(immigrant);
        }

        private PoliceOfficer GetRandomPoliceOfficer()
        {
            return PoliceOfficers.ElementAtOrDefault(_random.RandomNumber(0, PoliceOfficers.Count));
        }

    }
}
