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

namespace RaysHotDogs
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/smallicon", Theme = "@style/Theme.AppCompat.Light")]
    public class MenuActivity : Activity
    {
        private Button _orderButton;
        private Button _cartButton;
        private Button _aboutButton;
        private Button _mapButton;
        private Button _takePictureButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMenu);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            _orderButton = FindViewById<Button>(Resource.Id.orderButton);
            _cartButton = FindViewById<Button>(Resource.Id.cartButton);
            _aboutButton = FindViewById<Button>(Resource.Id.aboutButton);
            _mapButton = FindViewById<Button>(Resource.Id.mapButton);
            _takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents()
        {
            _orderButton.Click += (s,e) => StartActivityByIntent(typeof(HotDogMenuActivity));
            _aboutButton.Click += (s, e) => StartActivityByIntent(typeof(AboutActivity));
            _takePictureButton.Click += (s, e) => StartActivityByIntent(typeof(TakePictureActivity));
            _mapButton.Click += (s, e) => StartActivityByIntent(typeof(RayMapActivity));
            _cartButton.Click += (s, e) => StartActivityByIntent(typeof(CartActivity));
        }

        private void StartActivityByIntent(Type activityType)
        {
            var intent = new Intent(this, activityType);
            StartActivity(intent);
        }
    }
}