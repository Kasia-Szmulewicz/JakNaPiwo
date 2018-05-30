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

namespace JakNaPiwo
{
    public class LocationService : Java.Lang.Object, ILocationListener
    {
        //pobranie menedżera LocationManager do kontrolowania aktualnej lokalizacji
        protected LocationManager locationManager = Application.Context.GetSystemService("location") as LocationManager;

        public void StartLocationUpdates(MainActivity activity)
        {
            //kryteria aplikacji dotycząe wyboru dostawcy lokalizacji
            var locationCriteria = new Criteria();
            //zwraca stała wskazującą na pożądaną dokładność połozenia 
            locationCriteria.Accuracy = Accuracy.Fine;
            //stała określająca żadane zapotrzebowanie mocy 
            locationCriteria.PowerRequirement = Power.NoRequirement;
            //dostawca, który najlepiej spełnia kryteria
            var locationProvider = locationManager.GetBestProvider(locationCriteria, true);

            //dostawca, minTime, minDistance, listener
            activity.RunOnUiThread(() => locationManager.RequestLocationUpdates(locationProvider, 1000, 1, this));
        }

        public void StopLocationUpdates()
        {
            locationManager.RemoveUpdates(this);
        }

        public event EventHandler<LocationChangedEventArgs> LocationChanged = delegate { };

        public void OnLocationChanged(Location location)
        {
            this.LocationChanged(this, new LocationChangedEventArgs(location));
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
        }
    }
}