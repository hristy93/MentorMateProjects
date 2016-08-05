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

        public IList<Immigrant> immigrants = null;
        public IList<PoliceOfficer> policeOfficers = null;
       

        public City(
            string citizenName,
            IList<Immigrant> cityImmigrants,
            IList<PoliceOfficer> citypoliceOfficers,
            int cityCitizensCount
            )
        {
            Name = citizenName;
            immigrants = cityImmigrants;
            policeOfficers = citypoliceOfficers;
            CitizensCount = cityCitizensCount;
        }
     

    }
}
