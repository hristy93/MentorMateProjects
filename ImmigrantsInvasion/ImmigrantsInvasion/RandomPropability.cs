using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public sealed class RandomGenerator
    {
        private static RandomGenerator _instance = null;
        private static readonly object syncLock = new object();
        private static readonly Random _random = new Random();

        private RandomGenerator()
        {
                
        }

        public bool Propability (double propabilityValue)
        {
            return _random.NextDouble() >= propabilityValue;
        }

        public int RandomNumber(int from, int to)
        {
            return _random.Next(from, to);
        }

        public static RandomGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RandomGenerator();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
