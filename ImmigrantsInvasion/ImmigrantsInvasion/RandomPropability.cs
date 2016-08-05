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
        private readonly object syncLock = new object();
        private readonly Random _random = new Random();

        private RandomPropability()
        {
                
        }

        public bool Propability (int propabilityValue)
        {
            return _random.NextDouble() >= propabilityValue;
        }

        public RandomPropability Instance
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
