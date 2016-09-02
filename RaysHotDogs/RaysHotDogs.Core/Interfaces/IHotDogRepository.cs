using System.Collections.Generic;
using RaysHotDogs.Core.Model;

namespace RaysHotDogs.Core.Repository
{
    public interface IHotDogRepository
    {
        List<HotDog> AllHotDogs { get; }
        List<HotDogGroup> HotDogGroups { get; }

        List<HotDog> GetFavoriteHotDogs();
        List<HotDogGroup> GetGroupedHotDogs();
        HotDog GetHotDogById(int hotDogId);
        List<HotDog> GetHotDogsForGroup(int hotDogGroupId);
        void GetHotDogData();
    }
}