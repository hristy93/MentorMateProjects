using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrantsInvasion
{
    public sealed class RandomPropability
    {
        private static RandomPropability _instance = null;
        private static readonly object syncLock = new object();
        private static readonly Random _random = new Random();

        private RandomPropability()
        {
                
        }

        public bool Propability (double propabilityValue)
        {
            return _random.NextDouble() >= propabilityValue;
        }

        public static RandomPropability Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RandomPropability();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
