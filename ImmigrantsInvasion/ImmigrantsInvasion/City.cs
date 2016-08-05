using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class City
    {
        public string Name { get; private set; }
        public int CitizensCount { get; private set; }

        public List<Immigrant> immigrants = null;
        public List<PoliceOfficer> policeOfficers = null;
       

        public City(
            string citizenName,
            List<Immigrant> cityImmigrants,
            List<PoliceOfficer> citypoliceOfficers,
            int cityCitizensCount
            )
        {
            Name = citizenName;
            immigrants = cityImmigrants;
            policeOfficers = citypoliceOfficers;
            CitizensCount = cityCitizensCount;
        }
     
        public void DelegatePoliceOfficerToImmigrant(Immigrant immigrant)
        {

        }
    }
}
