using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public class Weapon
    {
        private const byte PISTOL_BULLETS_COUNT = 10;
        private const byte RIFLE_BULLETS_COUNT = 30;

        public WeaponTypes Type { get; private set; }
        public decimal Price { get; private set; }
        public byte BulletsCount { get; private set; }

        public Weapon (WeaponTypes weaponType, decimal weaponPrice)
        {
            Type = weaponType;
            Price = weaponPrice;

            if (Type == WeaponTypes.Pistol)
            {
                BulletsCount = PISTOL_BULLETS_COUNT;
            }
            else if (true)
            {

            }
            else
            {
                throw new NotSupportedException("This type of weapon is not defined!");
            }
        }
    }
}
