using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class PoliceOfficer
    {
        private const double IMMIGRANT_CATCHING_PROPABILITY_FOR_POLICEMAN = 0.5;
        private const double IMMIGRANT_CATCHING_PROPABILITY_FOR_SPECIAL_FORCES = 0.9;
        private static RandomGenerator _random = RandomGenerator.Instance;

        public string Name { get; private set; }
        public PoliceOfficerTypes Type { get; private set; }
        public double ImmigrantCatchingPropability { get; private set; }

        public PoliceOfficer(string policeOfficerName, PoliceOfficerTypes policeOfficerType)
        {
            Name = policeOfficerName;
            Type = policeOfficerType;

            if (Type == PoliceOfficerTypes.Policeman)
            {
                ImmigrantCatchingPropability = IMMIGRANT_CATCHING_PROPABILITY_FOR_POLICEMAN;
            }
            else if (Type == PoliceOfficerTypes.SpecialForces)
            {
                ImmigrantCatchingPropability = IMMIGRANT_CATCHING_PROPABILITY_FOR_SPECIAL_FORCES;
            }
            else
            {
                throw new NotSupportedException("This type of police officer is not defined!");
            }
        }

        public bool CheckImmigrant(Immigrant immigrant)
        {
            if (Type == PoliceOfficerTypes.Policeman)
            {
                if ((immigrant is RadicalImmigrant || immigrant is ImmigrantExtremist) && !immigrant.HasBombs())
                {
                    return _random.Propability(ImmigrantCatchingPropability);
                }
                else
                {
                    return true;
                }
            }
            else if (Type == PoliceOfficerTypes.SpecialForces)
            {
                if ((immigrant is RadicalImmigrant && immigrant.Passport == null) || immigrant is ImmigrantExtremist)
                {
                    return _random.Propability(ImmigrantCatchingPropability);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new NotSupportedException("This type of police officer is not defined!");
            }
        }
    }
}
