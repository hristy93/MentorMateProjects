using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Repository;
using Android.Support.V4.App;
using Android.App;

namespace RaysHotDogs.Fragments
{
    public class BaseFragment: Android.Support.V4.App.Fragment
    {
        public const int REQUEST_CODE_NUMBER = 100;

        protected ListView listView;
        protected HotDogDataService hotDogDataService;
        protected List<HotDog> hotDogs;

        public BaseFragment()
        {
            hotDogDataService = new HotDogDataService(HotDogRepository.Instance);
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }

        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.hotDogListView);
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var hotDog = hotDogs[e.Position];
            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(HotDogDetailActivity));
            intent.PutExtra("selectedHotDogId", hotDog.HotDogId);
            StartActivityForResult(intent, REQUEST_CODE_NUMBER);
        }


        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == (int)Result.Ok && requestCode == REQUEST_CODE_NUMBER)
            {
                var selectedHotDog = hotDogDataService.GetHotDogById(data.GetIntExtra("selectedHotDogId", 0));
                var dialog = new AlertDialog.Builder(this.Activity);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage($"You've added {data.GetIntExtra("amount", 0)} time(s) the {selectedHotDog.Name}." +
                    "Do you want to see your shopping cart?");
                dialog.SetNeutralButton("Yes", (s, e) => StartActivity(new Intent(this.Activity, typeof(CartActivity))));
                dialog.SetNegativeButton("No", (s, e) => dialog.Dispose());
                dialog.Show();
            }
        }
    }
}