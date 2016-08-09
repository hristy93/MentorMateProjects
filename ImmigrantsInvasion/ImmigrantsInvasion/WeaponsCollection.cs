using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public sealed class WeaponsCollection
    {
        private static List<Weapon> _weapons = null;
        private static WeaponsCollection _instance = null;
        private static readonly object syncLock = new object();
        private static RandomGenerator _random = null;

        private WeaponsCollection(int weaponsCount)
        {
            _weapons = new List<Weapon>(weaponsCount);
            _random = RandomGenerator.Instance;
            BuyNeededWeapons(weaponsCount);
        }

        public static WeaponsCollection Instance()
        {
            return Instance(200);
        }

        public static WeaponsCollection Instance(int weaponsCount)
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new WeaponsCollection(weaponsCount);
                    }
                }
            }

            return _instance;
        }

        public Weapon GetRandomWeapon(bool areBombsAllowed)
        {
            if(_weapons.Count == 0)
            {
                throw new IndexOutOfRangeException("Not enought weapons");
            }
            Weapon weapon;
            if (!areBombsAllowed)
            {
                WeaponTypes bombWeaponType = WeaponTypes.Bomb;
                do
                {
                    weapon = _weapons.ElementAtOrDefault(_random.RandomNumber(1, _weapons.Count));
                }
                while (weapon.Type.Equals(bombWeaponType));
            }
            else
            {
                weapon = _weapons.ElementAtOrDefault(_random.RandomNumber(0, _weapons.Count));
            }

            RemoveWeaponFromCollection(weapon);
            return weapon;
        }

        public List<Weapon> GetAllWeapons()
        {
            return _weapons;
        }

        private void BuyNeededWeapons(int weaponsCount)
        {
            for (int i = 0; i < weaponsCount; i++)
            {
                BuyRandomWeapon();
            }
        }

        private void BuyRandomWeapon()
        {
            Weapon weapon;
            var weaponTypesCount = Enum.GetNames(typeof(WeaponTypes)).Length;
            int randomNumber = _random.RandomNumber(1, weaponTypesCount + 1);
            if (randomNumber == 1)
            {
                weapon = new Weapon(WeaponTypes.Pistol);
            }
            else if (randomNumber == 2)
            {
                weapon = new Weapon(WeaponTypes.Riffle);
            }
            else
            {
                weapon = new Weapon(WeaponTypes.Bomb);
            }

            AddWeaponToCollection(weapon);
        }

        private void AddWeaponToCollection(Weapon weapon)
        {
            _weapons.Add(weapon);
        }

        private void RemoveWeaponFromCollection(Weapon weapon)
        {
            _weapons.Remove(weapon);
        }
    }
}
