﻿using Newtonsoft.Json;
using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RaysHotDogs.Core.Repository
{
    public sealed class HotDogRepository : IHotDogRepository
    {
        private const string HOTDOG_GROUPS_DATA_URL = "http://gillcleerenpluralsight.blob.core.windows.net/files/hotdogs.json";
        private static HotDogRepository _instance = null;
        private static readonly object _synclock = new object();

        public List<HotDogGroup> HotDogGroups { get; private set; }
        public List<HotDog> AllHotDogs
        {
            get
            {
                var hotDogs =
                    from hotDogGroup in HotDogGroups
                    from hotDog in hotDogGroup.HotDogs
                    select hotDog;

                return hotDogs.ToList<HotDog>();
            }

            private set
            {
                AllHotDogs = value;
            }
        }

        public static HotDogRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_synclock)
                    {
                        if (_instance == null)
                        {
                            _instance = new HotDogRepository();
                        }
                    }
                }

                return _instance;
            }
        }

        private HotDogRepository()
        {
            GetHotDogData();
        }

        public HotDog GetHotDogById(int hotDogId) => AllHotDogs.Where(h => h.HotDogId == hotDogId).FirstOrDefault();

        public List<HotDog> GetFavoriteHotDogs() => AllHotDogs.Where(h => h.IsFavorite == true).ToList();

        public List<HotDogGroup> GetGroupedHotDogs() => HotDogGroups;

        public List<HotDog> GetHotDogsForGroup(int hotDogGroupId)
        {
            var group = HotDogGroups.Where(h => h.HotDogGroupId == hotDogGroupId).FirstOrDefault();
            return group?.HotDogs;
        }

        public void GetHotDogData()
        {
            Task.Run(() => GetHotDogDataAsync(HOTDOG_GROUPS_DATA_URL)).Wait();
        }

        private async Task GetHotDogDataAsync(string uri)
        {
            if (HotDogGroups == null)
            {
                string responseJsonString = null;
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var response = await httpClient.GetAsync(uri);
                        responseJsonString = await response.Content.ReadAsStringAsync();
                        await Task.Factory.StartNew(() => { HotDogGroups = JsonConvert.DeserializeObject<List<HotDogGroup>>(responseJsonString); });
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"There was a problem with getting the data from the Web: {ex.ToString()}");
                    }
                }
            }
        }
    }
}
