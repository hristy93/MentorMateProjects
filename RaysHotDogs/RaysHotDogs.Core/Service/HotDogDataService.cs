using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaysHotDogs.Core.Service
{
    public class HotDogDataService
    {
        private IHotDogRepository _hotDogRepository;

        public HotDogDataService(IHotDogRepository hotDogRepository)
        {
            _hotDogRepository = hotDogRepository;
        }

        public List<HotDog> GetAllHotDogs() => _hotDogRepository.AllHotDogs;
        public List<HotDogGroup> GetGroupedHotDogs() => _hotDogRepository.GetGroupedHotDogs();
        public List<HotDog> GetHotDogsForGroup(int hotDogGroupId) => _hotDogRepository.GetHotDogsForGroup(hotDogGroupId);
        public List<HotDog> GetFavoriteHotDogs() => _hotDogRepository.GetFavoriteHotDogs();
        public HotDog GetHotDogById(int hotDogId) => _hotDogRepository.GetHotDogById(hotDogId);
    }
}
