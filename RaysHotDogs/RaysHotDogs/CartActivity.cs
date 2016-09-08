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
    [Activity(Label = "@string/myCartText", Icon = "@drawable/smallicon", Theme = "@style/Theme.AppCompat.Light")]
    public class CartActivity: Activity
    {
        private CartDataService _cartDataService;
        private List<CartItem> _cartItems;
        private ListView _cartListView;
        private TextView _totalPriceTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CartView);

            _cartDataService = new CartDataService(CartRepository.Instance);
            _cartListView = FindViewById<ListView>(Resource.Id.cartListView);
            _cartItems = _cartDataService.GetCartItems().ToList();
            _cartListView.Adapter = new CartItemsListAdapter(this, _cartItems);

            _totalPriceTextView = FindViewById<TextView>(Resource.Id.totalPriceTextView);
            if (_cartItems.Count == 0)
            {
                _totalPriceTextView.Visibility = ViewStates.Invisible;
            }
            else
            {
                _totalPriceTextView.Visibility = ViewStates.Visible;
                _totalPriceTextView.Text = "Total Price: $ " + _cartDataService.GetTotalPrice().ToString();
            }
        }
    }
}