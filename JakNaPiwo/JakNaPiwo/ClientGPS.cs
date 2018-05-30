using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Geolocator.Abstractions;

namespace JakNaPiwo
{
    public class ClientGPS
    {
        public Location currentLocation;
        private float _totMeters = 0f;
        public bool isConnected = false;

        public IGeolocator geolocator;

        private LocationService locationService;

        public ClientGPS()
        {
            locationService = new LocationService();
            locationService.LocationChanged += OnLocationChanged;
        }

        public void Connect(MainActivity activity)
        {
            locationService.StartLocationUpdates(activity);

            isConnected = true;
        }

        public void Disconnect()
        {
            if (isConnected)
                locationService.StopLocationUpdates();

            isConnected = false;
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (e.Location != null)
            {
                if (currentLocation != null)
                {
                    var results = new float[1];

                    Location.DistanceBetween(currentLocation.Latitude, currentLocation.Longitude, e.Location.Latitude, e.Location.Longitude, results);

                    float speed = e.Location.Speed * 3.6f;

                    if (speed >= 1f)
                        _totMeters += results[0];

                    //geolocator.GetPositionAsync();
                   // DataReceived(null, geolocator.GetPositionAsync());
                    //DataReceived(null, new ClientDataReceivedEventArgs(results));
                }

                currentLocation = e.Location;
            }

        }
    }
}