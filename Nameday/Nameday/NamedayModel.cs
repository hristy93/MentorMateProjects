﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameday
{
    public class NamedayModel
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public IEnumerable<string> Names { get; set; }
        public string NamesAsString => String.Join(", ", Names);
        
        public NamedayModel(int month, int day, IEnumerable<string> names)
        {
            Month = month;
            Day = day;
            Names = names;
        }

        public NamedayModel()
        {

        }
    }
}
