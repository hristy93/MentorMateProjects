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
    [Activity(Label = "MenuActivity", MainLauncher = true, Icon = "@drawable/smallicon")]
    public class MenuActivity : Activity
    {
        public Button OrderButton { get; private set; }
        public Button CartButton { get; private set; }
        public Button AboutButton { get; private set; }
        public Button MapButton { get; private set; }
        public Button TakePictureButton { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMenu);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            OrderButton = FindViewById<Button>(Resource.Id.orderButton);
            CartButton = FindViewById<Button>(Resource.Id.cartButton);
            AboutButton = FindViewById<Button>(Resource.Id.aboutButton);
            MapButton = FindViewById<Button>(Resource.Id.mapButton);
            TakePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents()
        {
            OrderButton.Click += (s,e) => StartActivityByIntent(typeof(HotDogMenuActivity));
            AboutButton.Click += (s, e) => StartActivityByIntent(typeof(AboutActivity));
            TakePictureButton.Click += (s, e) => StartActivityByIntent(typeof(TakePictureActivity));
            MapButton.Click += (s, e) => StartActivityByIntent(typeof(RayMapActivity));
            CartButton.Click += (s, e) => StartActivityByIntent(typeof(CartActivity));
        }

        private void StartActivityByIntent(Type activityType)
        {
            var intent = new Intent(this, activityType);
            StartActivity(intent);
        }
    }
}