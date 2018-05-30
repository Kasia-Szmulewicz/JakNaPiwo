using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;

namespace JakNaPiwo
{
    [Activity(Label = "Mapa pubów")]
    public class MapActivity : Activity
    {
        private FrameLayout mapFrameLayout;
        private MapFragment mapFragment;
        private GoogleMap googleMap;
        private LatLng pubLocation;
        private string pubName;
        private List<Pub> pubs;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //tu pobranie pubów z bazy danyh 
            using (var db = new JakNaPiwoContext())
            {
                pubs = db.Pubs.ToList();
            }

            //pubs = beerService.GetAllPubs();

            SetContentView(Resource.Layout.PubMapView);

            FindViews();

            CreateMapFragment();

            UpdateMapView();

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
                    //pętla po każdym pabie 
                    // foreach (PubMapActivity item in pubs)
                    foreach (var item in pubs)
                    {
                        pubName = item.Name;
                        pubLocation = new LatLng(item.PubLatitude, item.PubLongitude);

                        MarkerOptions markerOptions = new MarkerOptions();
                        markerOptions.SetPosition(pubLocation);
                        markerOptions.SetTitle(pubName);
                        googleMap.AddMarker(markerOptions);
                    }


                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(pubLocation, 11);
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