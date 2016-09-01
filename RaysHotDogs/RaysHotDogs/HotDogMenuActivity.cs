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
using RaysHotDogs.Adapters;

namespace RaysHotDogs
{
    [Activity(Label = "HotDogMenuActivity", MainLauncher = true)]
    public class HotDogMenuActivity : Activity
    {
        private ListView _hotDogListView;
        private List<HotDog> _allHotDogs;
        private HotDogDataService _hotDogDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogMenuView);
            _hotDogListView = FindViewById<ListView>(Resource.Id.hotDogListView);
            _hotDogDataService = new HotDogDataService();
            _allHotDogs = _hotDogDataService.GetAllHotDogs();
            _hotDogListView.Adapter = new HotDogListAdapter(this, _allHotDogs);
            _hotDogListView.FastScrollEnabled = true;
        }
    }
}