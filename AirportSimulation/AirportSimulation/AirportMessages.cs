using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public static class AirportMessages
    {
        public const string INVALID_UNSUBSCRIBTION_MESSAGE = "The aircraft has to land in order to unsubscribe to that event!";
        public const string INVALID_PASSANGERS_COUNT_MESSAGE = "The entered number of passengers is invalid. Please insert a number between 1 and ";
        public const string INVALID_FUEL_LEFT_MESSAGE = "The entered amount of fuel left is invalid. Please insert a number between 1 and ";
    }
}
