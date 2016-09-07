using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaysHotDogs.Core
{
	public class Cart
    {
        public List<CartItem> CartItems { get; private set; }
        public int TotalPrice { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            TotalPrice = 0;
        }
    }
}

