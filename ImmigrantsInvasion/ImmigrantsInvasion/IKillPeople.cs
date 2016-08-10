using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    interface IKillPeople
    {
        void KillPeople(int weaponsCount);
        bool TryToBuyWeapon();
        void BuyNeededWeapons(int weaponsCount);
        double GetVictimsPercentage();
    }
}
