using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public sealed class ConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static object _synchronizationLock = new object();

        private ConfigurationManager()
        {

        }

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_synchronizationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager();
                        }
                    }
                }

                return _instance;
            }
        }
    }

    public sealed class LazyConfigurationManager
    {
        private static readonly Lazy<LazyConfigurationManager> _instance = new Lazy<LazyConfigurationManager>(() => new LazyConfigurationManager());
        private static object _synchronizationLock = new object();

        private LazyConfigurationManager()
        {

        }

        public static LazyConfigurationManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
