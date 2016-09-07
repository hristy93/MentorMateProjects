using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core;
using RaysHotDogs.Adapters;

namespace RaysHotDogs
{
    [Activity(Label = "@string/myCartText", Icon = "@drawable/smallicon")]
    public class CartActivity: Activity
    {
        private CartDataService _cartDataService;
        private List<CartItem> _cartItems;
        private ListView _cartListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CartView);

            _cartDataService = new CartDataService(CartRepository.Instance);
            _cartListView = FindViewById<ListView>(Resource.Id.cartListView);
            _cartItems = _cartDataService.GetCartItems().ToList();
            _cartListView.Adapter = new CartItemsListAdapter(this, _cartItems);
        }
    }
}