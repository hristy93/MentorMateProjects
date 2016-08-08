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
        private const int PISTOL_PRICE = 100;
        private const int RIFLE_PRICE= 150;
        private const int BOMB_PRICE = 300;

        private bool _isFired = false;

        public WeaponTypes Type { get; private set; }
        public decimal Price { get; private set; }
        public byte BulletsCount { get; private set; } = 0;

        public Weapon (WeaponTypes weaponType)
        {
            Type = weaponType;

            if (Type == WeaponTypes.Pistol)
            {
                BulletsCount = PISTOL_BULLETS_COUNT;
                Price = PISTOL_PRICE;
            }
            else if (Type == WeaponTypes.Riffle)
            {
                BulletsCount = RIFLE_BULLETS_COUNT;
                Price = RIFLE_PRICE;
            }
            else if (Type == WeaponTypes.Bomb)
            {
                Price = BOMB_PRICE;
            }
            else
            {
                throw new NotSupportedException("This type of weapon is not defined!");
            }
        }

        public int Fire()
        {
            if (!_isFired)
            {
                _isFired = true;
                if (Type == WeaponTypes.Bomb)
                {
                    Console.WriteLine($"A bomb was detonated!");
                }
                else if (Type == WeaponTypes.Pistol || Type == WeaponTypes.Riffle)
                {
                    Console.WriteLine($"A {Type.ToString().ToLower()} was fired and used all of its "
                        + $"{BulletsCount} bullets!");
                }
                else
                {
                    throw new NotSupportedException("This type of weapon is not defined!");
                }
            }
            else
            {
                throw new InvalidOperationException("The weapon was already used!");
            }

            return BulletsCount;
        }
    }
}
