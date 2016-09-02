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
using RaysHotDogs.Core.Service;
using RaysHotDogs.Core.Model;

namespace RaysHotDogs.Fragments
{
    public class BaseFragment: Fragment
    {
        protected ListView listView;
        protected HotDogDataService hotDogDataService;
        protected List<HotDog> hotDogs;

        public BaseFragment()
        {
            hotDogDataService = new HotDogDataService();
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
            StartActivityForResult(intent, 100);
        }


        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedHotDog = hotDogDataService.GetHotDogById(data.GetIntExtra("selectedHotDogId", 0));
                var dialog = new AlertDialog.Builder(this.Activity);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage($"You've added {data.GetIntExtra("amount", 0)} time(s) the {selectedHotDog.Name}");
                dialog.Show();
            }
        }
    }
}