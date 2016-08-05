using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public static class WeaponsCollection
    {
        private static List<Weapon> _weapons = null;
        private static RandomGenerator _random = RandomGenerator.Instance;

        public static void AddWeapon (Weapon weapon)
        {
            _weapons.Add(weapon);
        }

        public static void RemoveWeapon(Weapon weapon)
        {
            _weapons.Remove(weapon);
        }

        public static Weapon GetRandomWeapon(Weapon weapon)
        {
            return _weapons.ElementAtOrDefault(_random.RandomNumber(1, _weapons.Count));
        }

        public static List<Weapon> GetAllWeapons()
        {
            return _weapons;
        }
    }
}
