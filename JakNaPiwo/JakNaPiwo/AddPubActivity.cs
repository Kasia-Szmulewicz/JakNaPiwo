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
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;
using Java.Util;

namespace JakNaPiwo
{
    [Activity(Label = "Dodaj Pub")]
    public class AddPubActivity : Activity
    {
        private EditText nameEditText;
        private EditText cityEditText;
        private EditText streetEditText;
        private EditText numberEditText;
        private Button gpsButton;
        private Button addButton;
        private int editBeerId;
        private string view;

        private string addressToGeoLocator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPub);

            // Create your application here
            editBeerId = Intent.Extras.GetInt("editBeerId");
            view = Intent.GetStringExtra("view");

            FindViews();

            HandleEvents();

        }

        private void FindViews()
        {
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            cityEditText = FindViewById<EditText>(Resource.Id.cityEditText);
            streetEditText = FindViewById<EditText>(Resource.Id.streetEditText);
            numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
            gpsButton = FindViewById<Button>(Resource.Id.gpsButton);
            addButton = FindViewById<Button>(Resource.Id.addButton);
        }

        private void HandleEvents()
        {
            gpsButton.Click += GPSButton_Click;
            addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var newPub = new Pub
            {
                Name = nameEditText.Text,
                Address = cityEditText.Text + " " + streetEditText.Text + " " + numberEditText.Text,
                City = cityEditText.Text,
                Street = streetEditText.Text,
                Number = numberEditText.Text

            };

            addressToGeoLocator = newPub.Address;

            using (var db = new JakNaPiwoContext())
            {
                var location = getLocation();

                var intent = new Intent();

                if (location == null)
                {
                    intent.SetClass(this, typeof(AddPubActivity));
                } 
                else
                {
                    newPub.PubLatitude = location[0];
                    newPub.PubLongitude = location[1];
                }

                db.connection.Insert(newPub, typeof(Pub));

                if (view == "editBeer")
                {
                    intent.SetClass(this, typeof(EditBeerActivity));
                    intent.PutExtra("editBeerId", editBeerId);
                }
                else
                {
                    intent.SetClass(this, typeof(BeerMenuActivity));
                    intent.SetFlags(ActivityFlags.ReorderToFront);
                }

                intent.PutExtra("newPubId", db.Pubs.LastOrDefault().ToString());
                StartActivityForResult(intent, 100);
            }

        }

        private double[] getLocation()
        {
            var location = new double[2];

            Geocoder geo = new Geocoder(this);

            try
            {
                List<Address> addressList = geo.GetFromLocationName(addressToGeoLocator, 5).ToList();
                Address address = addressList[0];
                if (address == null)
                    return null;

                location[0] = address.Latitude;
                location[1] = address.Longitude;

                return location;
            }
            catch(Exception e)
            {
                
            }

            return null;
        }

        private void GPSButton_Click(object sender, EventArgs e)
        {
            //TODO
        }
    }
}