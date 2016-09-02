using RaysHotDogs.Core.Model;
using System.Collections.Generic;

namespace RaysHotDogs.Core
{
    public interface ICartRepository
    {
        Cart MainCart { get; }
        IList<CartItem> CartItems { get; }

        void AddCartItem(CartItem cartItem);
        void AddCartItem(HotDog hotDog, int amount);
        CartItem GetCartItemByHotdog(HotDog hotDog);
        bool IsCartItemInCart(CartItem cartItem);
        void RemoveCartItem(CartItem cartItem);
    }
}