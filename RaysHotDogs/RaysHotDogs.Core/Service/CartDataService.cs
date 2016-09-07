using RaysHotDogs.Core.Model;
using System;
using System.Collections.Generic;

namespace RaysHotDogs.Core
{
	public class CartDataService
	{
        private ICartRepository _cartRepository;

		public CartDataService(ICartRepository cartRepository)
		{
            _cartRepository = cartRepository;
        }

        public IList<CartItem> GetCartItems() => _cartRepository.CartItems;

        public CartItem GetCartItemByHotdog(HotDog hotDog) => _cartRepository.GetCartItemByHotdog(hotDog);

        public bool IsCartItemInCart(CartItem cartItem) => _cartRepository.IsCartItemInCart(cartItem);

        public void RemoveCartItem(CartItem cartItem) => _cartRepository.RemoveCartItem(cartItem);

        public void AddCartItem(CartItem cartItem) => _cartRepository.AddCartItem(cartItem);

        public void AddCartItem(HotDog hotDog, int amount) => _cartRepository.AddCartItem(hotDog, amount);

        public int GetTotalPrice() => _cartRepository.MainCart.TotalPrice;
    }
}

