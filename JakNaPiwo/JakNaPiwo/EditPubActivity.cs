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
using Android.Locations;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;

namespace JakNaPiwo
{
    [Activity(Label = "Edytuj pub")]
    public class EditPubActivity : Activity
    {
        private EditText nameEditText;
        private EditText cityEditText;
        private EditText streetEditText;
        private EditText numberEditText;
        private Button gpsButton;
        private Button editButton;

        private Pub editPub;
        private string addressToGeoLocator;
        private string view;
        private int editBeerId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditPubView);

            // Create your application here

            var editPubId = Intent.Extras.GetInt("editPubId");
            view = Intent.GetStringExtra("view");
            editBeerId = Intent.Extras.GetInt("editBeerId");

            using (var db = new JakNaPiwoContext())
            {
                editPub = db.Pubs.FirstOrDefault(b => b.Id == editPubId);

            }

            FindViews();
            BindData();
            HandleEvents();

        }

        private void FindViews()
        {
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            cityEditText = FindViewById<EditText>(Resource.Id.cityEditText);
            streetEditText = FindViewById<EditText>(Resource.Id.streetEditText);
            numberEditText = FindViewById<EditText>(Resource.Id.numberEditText);
            gpsButton = FindViewById<Button>(Resource.Id.gpsButton);
            editButton = FindViewById<Button>(Resource.Id.editButton);

        }

        private void BindData()
        {
            nameEditText.Text = editPub.Name;
            cityEditText.Text = editPub.City;
            streetEditText.Text = editPub.Street;
            numberEditText.Text = editPub.Number;

        }

        private void HandleEvents()
        {
            gpsButton.Click += GPSButton_Click;
            editButton.Click += EditButton_Click;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {

            editPub.Name = nameEditText.Text;
            editPub.Address = cityEditText.Text + " " + streetEditText.Text + " " + numberEditText.Text;
            editPub.City = cityEditText.Text;
            editPub.Street = streetEditText.Text;
            editPub.Number = numberEditText.Text;

            addressToGeoLocator = editPub.Address;

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
                    editPub.PubLatitude = location[0];
                    editPub.PubLongitude = location[1];
                }

                db.UpdatePubTable(editPub);

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

                intent.PutExtra("newPubId", editPub.Id.ToString());
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
            catch (Exception e)
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