using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;

namespace JakNaPiwo
{
    [Activity(Label = "Mapa")]
    public class PubMapActivity : Activity
    {
        private FrameLayout mapFrameLayout;
        private MapFragment mapFragment;
        private GoogleMap googleMap;
        private LatLng pubLocation;
        private Pub selectedPub;
        private string pubName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here


            var selectedPubId = Intent.Extras.GetInt("selectedPubId");
            //tu pobranie piwa z bazy danyh 
            using (var db = new JakNaPiwoContext())
            {
                selectedPub = db.Pubs.FirstOrDefault(p => p.Id == selectedPubId);

                pubLocation = new LatLng(selectedPub.PubLatitude, selectedPub.PubLongitude);
                pubName = selectedPub.Name;
            }



            SetContentView(Resource.Layout.PubMapView);

            FindViews();

            HandleEvents();

            CreateMapFragment();

            UpdateMapView();

        }

        private void HandleEvents()
        {
            //chyba nie potrzebne 
        }

        private void FindViews()
        {
            mapFrameLayout = FindViewById<FrameLayout>(Resource.Id.mapFrameLayout);
        }

        private void UpdateMapView()
        {
            var mapReadyCallback = new LocalMapReady();

            mapReadyCallback.MapReady += (sender, args) =>
            {
                googleMap = (sender as LocalMapReady).Map;

                if (googleMap != null)
                {
                    MarkerOptions markerOptions = new MarkerOptions();
                    markerOptions.SetPosition(pubLocation);
                    markerOptions.SetTitle(pubName);
                    googleMap.AddMarker(markerOptions);

                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(pubLocation, 15);
                    googleMap.MoveCamera(cameraUpdate);
                }
                 };
            mapFragment.GetMapAsync(mapReadyCallback);
        }

        private void CreateMapFragment()
        {
            mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;

            if (mapFragment == null)
            {
                var googleMapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeNormal)
                    .InvokeZoomControlsEnabled(true)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                mapFragment = MapFragment.NewInstance(googleMapOptions);
                fragmentTransaction.Add(Resource.Id.mapFrameLayout, mapFragment, "map");
                fragmentTransaction.Commit();
            }
           // mapFragment.GetMapAsync(this);
        }

        private class LocalMapReady : Java.Lang.Object, IOnMapReadyCallback
        {
            public GoogleMap Map { get; private set; }

            public event EventHandler MapReady;

            public void OnMapReady(GoogleMap googleMap)
            {
                Map = googleMap;
                var handler = MapReady;
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }
        }

    }
}