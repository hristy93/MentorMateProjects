using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaysHotDogs.Core.Model
{
    public class HotDog
    {
        public int HotDogId { get; private set; }
        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public int Price { get; private set; }
        public bool Available { get; private set; }
        public int PrepTime { get; private set; }
        public List<string> Ingredients { get; private set; }
        public bool IsFavorite { get; private set; }
        public string GroupName { get; private set; }
    }
}
