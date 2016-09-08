using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using System;

namespace RaysHotDogs
{
    [Activity(Label = "@string/visitRaysStoreText", Icon = "@drawable/smallicon", Theme = "@style/Theme.AppCompat.Light")]
    public class RayMapActivity : Activity
    {
        private readonly LatLng rayLocation = new LatLng(50.846704, 4.352446);

        private Button _externalMapButton;
        private FrameLayout _mapFrameLayout;
        private MapFragment _mapFragment;
        private GoogleMap _googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RayMapView);

            FindViews();
            HandleEvents();
            CreateMapFragment();
            UpdateMapView();
        }

        private void HandleEvents()
        {
            _externalMapButton.Click += ExternalMapButton_Click;
        }

        private void ExternalMapButton_Click(object sender, EventArgs e)
        {
            Android.Net.Uri rayLocationUri = Android.Net.Uri.Parse($"geo:{rayLocation.ToString()}");
            //Android.Net.Uri rayLocationUri = Android.Net.Uri.Parse("geo:50.846704,4.352446");
            Intent mapIntent = new Intent(Intent.ActionView, rayLocationUri);
            StartActivity(mapIntent);
        }

        private void FindViews()
        {
            _externalMapButton = FindViewById<Button>(Resource.Id.externalMapButton);
            _mapFrameLayout = FindViewById<FrameLayout>(Resource.Id.mapFrameLayout);
        }

        private void UpdateMapView()
        {
            var mapReadyCallback = new LocalMapReady();
            mapReadyCallback.MapReady += (sender, args) =>
            {
                _googleMap = (sender as LocalMapReady).Map;
                if (_googleMap != null)
                {
                    MarkerOptions markerOptions = new MarkerOptions();
                    markerOptions.SetPosition(rayLocation);
                    markerOptions.SetTitle("Ray's Hot Dogs");
                    _googleMap.AddMarker(markerOptions);

                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(rayLocation, 15);
                    _googleMap.MoveCamera(cameraUpdate);
                }
            };

            _mapFragment.GetMapAsync(mapReadyCallback);
        }

        private void CreateMapFragment()
        {
            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;

            if (_mapFragment == null)
            {
                var googleMapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(true)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(googleMapOptions);
                fragmentTransaction.Add(Resource.Id.mapFrameLayout, _mapFragment, "map");
                fragmentTransaction.Commit();
            }
        }

        private class LocalMapReady : Java.Lang.Object, IOnMapReadyCallback
        {
            public GoogleMap Map { get; private set; }

            public event EventHandler MapReady;

            public void OnMapReady(GoogleMap googleMap)
            {
                Map = googleMap;
                MapReady?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}