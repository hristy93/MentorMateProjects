using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaysHotDogs.Core
{
	public class CartRepository : ICartRepository
	{
        public Cart MainCart { get; private set; }
        public IList<CartItem> CartItems { get { return MainCart.CartItems; }}

        public CartRepository ()
		{
            MainCart = new Cart();
        }

        public CartItem GetCartItemByHotdog(HotDog hotDog) => CartItems.Where(c => c.HotDog == hotDog).SingleOrDefault();

        public bool IsCartItemInCart(CartItem cartItem) => CartItems.Contains(cartItem);

        public void RemoveCartItem(CartItem cartItem) => CartItems.Remove(cartItem);

        public void AddCartItem(CartItem cartItem)
        {
            if (!IsCartItemInCart(cartItem))
            {
                CartItems.Add(cartItem);
            }
            else
            {
                var requiredCartItem = GetCartItemByHotdog(cartItem.HotDog);
                requiredCartItem.IncreaseAmount(cartItem.Amount);
                RemoveCartItem(cartItem);
                AddCartItem(requiredCartItem);
            }
        }

        public void AddCartItem(HotDog hotDog, int amount) => AddCartItem(new CartItem(hotDog, amount));
    }
}

