using Android.App;
using Android.Content;
using Android.OS;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Fragments;

namespace RaysHotDogs
{
    [Activity(Label = "HotDogMenuActivity")]
    public class HotDogMenuActivity : Activity
    {
        private HotDogDataService _hotDogDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogMenuView);

            _hotDogDataService = new HotDogDataService();
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Favorites", Resource.Drawable.FavoritesIcon, new FavoriteHotDogFragment());
            AddTab("Meat Lovers", Resource.Drawable.MeatLoversIcon, new MeatLoversFragment());
            AddTab("Veggie Lovers", Resource.Drawable.VeggieLoversIcon, new VeggieLoversFragment());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedHotDog = _hotDogDataService.GetHotDogById(data.GetIntExtra("selectedHotDogId", 0));
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage($"You've added {data.GetIntExtra("amount", 0)} time(s) the {selectedHotDog.Name}");
                dialog.Show();
            }
        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                {
                    e.FragmentTransaction.Remove(fragment);
                }

                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }
    }
}