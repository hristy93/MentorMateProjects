using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaysHotDogs.Core.Model
{
    public class HotDogGroup
    {
        public int HotDogGroupId { get; private set; }
        public string Title { get; private set; }
        public string ImagePath { get; private set; }
        public List<HotDog> HotDogs { get; private set; }
    }
}
