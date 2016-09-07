using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaysHotDogs.Core
{
	public class CartRepository : ICartRepository
	{
        private static CartRepository _instance = null;
        private static readonly object _synclock = new object();

        public Cart MainCart { get; private set; }
        public IList<CartItem> CartItems { get { return MainCart.CartItems; }}

        public static CartRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_synclock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CartRepository();
                        }
                    }
                }

                return _instance;
            }
        }

        private CartRepository ()
		{
            MainCart = new Cart();
        }

        public CartItem GetCartItemByHotdog(HotDog hotDog) => CartItems.Where(c => c.HotDog.Name == hotDog.Name).SingleOrDefault();

        public bool IsCartItemInCart(CartItem cartItem) => GetCartItemByHotdog(cartItem.HotDog) != null;

        public void RemoveCartItem(CartItem cartItem) => CartItems.Remove(cartItem);

        public void AddCartItem(CartItem cartItem)
        {
            if (!IsCartItemInCart(cartItem))
            {
                CartItems.Add(cartItem);
                MainCart.TotalPrice += cartItem.HotDog.Price * cartItem.Amount;
            }
            else
            {
                var requiredCartItem = GetCartItemByHotdog(cartItem.HotDog);
                cartItem.IncreaseAmount(requiredCartItem.Amount);
                RemoveCartItem(requiredCartItem);
                CartItems.Add(cartItem);
                MainCart.TotalPrice += cartItem.HotDog.Price * (cartItem.Amount - requiredCartItem.Amount);
            }
        }

        public void AddCartItem(HotDog hotDog, int amount) => AddCartItem(new CartItem(hotDog, amount));
    }
}

