using Android.App;
using Android.Content;
using Android.OS;
using RaysHotDogs.Core.Repository;
using RaysHotDogs.Core.Service;
using RaysHotDogs.Fragments;

namespace RaysHotDogs
{
    [Activity(Label = "@string/orderHotDogsText", Icon = "@drawable/smallicon")]
    public class HotDogMenuActivity : Activity
    {
        private HotDogDataService _hotDogDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogMenuView);

            _hotDogDataService = new HotDogDataService(HotDogRepository.Instance);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("Favorites", Resource.Drawable.FavoritesIcon, new FavoriteHotDogFragment());
            AddTab("Meat Lovers", Resource.Drawable.MeatLoversIcon, new MeatLoversFragment());
            AddTab("Veggie Lovers", Resource.Drawable.VeggieLoversIcon, new VeggieLoversFragment());
        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            tab.TabSelected += (object sender, ActionBar.TabEventArgs e) =>
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                {
                    e.FragmentTransaction.Remove(fragment);
                }

                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += (object sender, ActionBar.TabEventArgs e) =>
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }
    }
}