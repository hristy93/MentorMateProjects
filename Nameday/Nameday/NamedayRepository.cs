using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Nameday
{
    public static class NamedayRepository
    {
        private static List<NamedayModel> _allNamedaysCache;

        public static async Task<List<NamedayModel>> GetAllNamedaysAsync()
        {
            if (_allNamedaysCache != null)
                return _allNamedaysCache;

            var client = new HttpClient();
            var stream = await client.GetStreamAsync(
                "http://www.response.hu/namedays_hu.json");

            var serializer = new DataContractJsonSerializer(typeof(List<NamedayModel>));
            _allNamedaysCache = (List<NamedayModel>)serializer.ReadObject(stream);

            return _allNamedaysCache;
        }
    }
}
