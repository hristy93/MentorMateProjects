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
    [Activity(Label = "About Ray's Hot Dogs", Icon = "@drawable/smallicon", Theme = "@style/Theme.AppCompat.Light")]
    public class AboutActivity : Activity
    {
        private TextView _phoneNumberTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AboutView);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            _phoneNumberTextView = FindViewById<TextView>(Resource.Id.phoneNumberTextView);
        }

        private void HandleEvents()
        {
            _phoneNumberTextView.Click += PhoneNumberTextView_Click;
        }

        private void PhoneNumberTextView_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + _phoneNumberTextView.Text));
            StartActivity(intent);
        }
    }
}