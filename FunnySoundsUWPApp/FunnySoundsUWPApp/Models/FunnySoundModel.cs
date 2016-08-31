using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class FunnySoundModel
    {
        public string Name { get; set; }
        public FunnySoundTypes Type { get; set; }
        public string ImageFilePath { get; set; }
        public string SoundFilePath { get; set; }
    }
}
