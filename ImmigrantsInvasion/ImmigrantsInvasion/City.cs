using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<PoliceOfficer> citypoliceOfficers,
            int cityCitizensCount
            )
        {
            Name = citizenName;
            PoliceOfficers = citypoliceOfficers;
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
