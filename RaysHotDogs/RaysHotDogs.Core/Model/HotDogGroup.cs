using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaysHotDogs.Core.Model
{
    public class HotDogGroup
    {
        public int HotDogGroupId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public List<HotDog> HotDogs { get; set; }
    }
}
