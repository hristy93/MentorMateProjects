using RaysHotDogs.Core.Model;
using System;

namespace RaysHotDogs.Core
{
	public class CartItem
	{
		public HotDog HotDog { get; private set; }
		public int Amount { get; private set; }

        public CartItem(HotDog hotDog, int amount)
        {
            HotDog = hotDog;
            Amount = amount;
        }

        public void IncreaseAmount(int newAmount)
        {
            Amount += newAmount;
        }
    }
}

