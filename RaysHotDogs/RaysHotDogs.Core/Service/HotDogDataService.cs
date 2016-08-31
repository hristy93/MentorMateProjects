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
        private HotDogRepository hotDogRepository = new HotDogRepository();

        public List<HotDog> GetAllHotDogs() => hotDogRepository.AllHotDogs;
        public List<HotDogGroup> GetGroupedHotDogs() => hotDogRepository.GetGroupedHotDogs();
        public List<HotDog> GetHotDogsForGroup(int hotDogGroupId) => hotDogRepository.GetHotDogsForGroup(hotDogGroupId);
        public List<HotDog> GetFavoriteHotDogs() => hotDogRepository.GetFavoriteHotDogs();
        public HotDog GetHotDogById(int hotDogId) => hotDogRepository.GetHotDogById(hotDogId);
    }
}
