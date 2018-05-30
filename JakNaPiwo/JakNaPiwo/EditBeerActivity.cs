using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Core.Service;
using JakNaPiwo.Utility;
using Java.IO;

namespace JakNaPiwo
{
    [Activity(Label = "Edytuj piwo")]
    public class EditBeerActivity : Activity
    {
        private ImageButton beerPictureImageButton;
        private EditText nameEditText;
        private TextView beerRatingTextView;
        private RatingBar beerRatingRatingBar;
        private TextView beerTypeTextView;
        private Spinner beerTypeSpinner;
        private EditText shortDescriptionEditText;
        private EditText priceEditText;
        private EditText pubAdresEditText;
        private Button mapButton;
        private Button deleteButton;
        private Button saveButton;

        private ArrayAdapter spinnerAdapter;
        private string selectedSpinner;
        private int selectedSpinnerID;

        private File newImageFile;
        private File imageDirectory;
        private string image;
        private Bitmap imageBitmap;
        private string receiveImagePath;

        public ClientGPS clientgps;
        public Location pubLocation;

        private Beer editBeer;
        private Pub newPub;
        private Pub currentPub;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditBeerView);
            // Create your application here

            var editBeerId = Intent.Extras.GetInt("editBeerId");

            int newPubId = -1;
            if (Intent.GetStringExtra("newPubId") != null)
                newPubId = int.Parse(Intent.GetStringExtra("newPubId"));

            receiveImagePath = Intent.GetStringExtra("imagePath");

            using (var db = new JakNaPiwoContext())
            {
                editBeer = db.Beers.FirstOrDefault(b => b.Id == editBeerId);

                currentPub = db.Pubs.FirstOrDefault(p => p.Id == editBeer.PubID);

                if (newPubId != -1)
                    newPub = db.Pubs.FirstOrDefault(p => p.Id == newPubId);
            }

            image = editBeer.ImagePath;

            FindViews();
            SpinnerStaff();
            BindData();

            HandleEvents();

        }

        protected void FindViews()
        {
            beerPictureImageButton = FindViewById<ImageButton>(Resource.Id.beerPictureImageButton);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            beerRatingTextView = FindViewById<TextView>(Resource.Id.beerRatingTextView);
            beerRatingRatingBar = FindViewById<RatingBar>(Resource.Id.beerRatingRatingBar);
            beerRatingRatingBar.NumStars = 5;
            beerTypeTextView = FindViewById<TextView>(Resource.Id.beerTypeTextView);
            beerTypeSpinner = FindViewById<Spinner>(Resource.Id.beerTypeSpinner);
            beerTypeSpinner.Prompt = "Wybierz typ piwa";
            shortDescriptionEditText = FindViewById<EditText>(Resource.Id.shortDescriptionEditText);
            priceEditText = FindViewById<EditText>(Resource.Id.priceEditText);
            pubAdresEditText = FindViewById<EditText>(Resource.Id.pubAdresEditText);
            mapButton = FindViewById<Button>(Resource.Id.mapButton);
            deleteButton = FindViewById<Button>(Resource.Id.deleteButton);
            saveButton = FindViewById<Button>(Resource.Id.saveButton);
        }

        private void SpinnerStaff()
        {

            beerTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            spinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.BeerType, Android.Resource.Layout.SimpleSpinnerItem);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            beerTypeSpinner.Adapter = spinnerAdapter;

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            selectedSpinner = spinner.GetItemAtPosition(e.Position).ToString();
            selectedSpinnerID = (int)spinner.GetItemIdAtPosition(e.Position);
            // Toast.MakeText(this, spinner.GetItemAtPosition(e.Position).ToString(), ToastLength.Short).Show();
        }

        private void BindData()
        {
            nameEditText.Text = editBeer.Name;
            beerRatingRatingBar.Rating = editBeer.BeerRating;
            beerTypeSpinner.SetSelection(editBeer.TypeID);
            shortDescriptionEditText.Text = editBeer.ShortDescription;
            priceEditText.Text = editBeer.Price.ToString();
            if (currentPub != null || newPub != null)
                pubAdresEditText.Text = (newPub != null ? newPub.Name : currentPub.Name) + " " + (newPub != null ? newPub.Address : currentPub.Address);
            else
                pubAdresEditText.Text = "";
            int height = 600;
            int width = 220;
            //string newIF = (string)newImageFile;
            if (string.IsNullOrEmpty(receiveImagePath))
            {
                imageBitmap = ImageHelper.GetImageBitmapFromFilePath(image, width, height);
            }
            else
            {
                imageBitmap = ImageHelper.GetImageBitmapFromFilePath(receiveImagePath, width, height);
            }

            beerPictureImageButton.SetImageBitmap(imageBitmap);

        }

        private void HandleEvents()
        {
            beerPictureImageButton.Click += BeerPictureImageButton_Click;
            mapButton.Click += MapButton_Click;
            deleteButton.Click += DeleteButton_Click;
            saveButton.Click += SaveButton_Click;

        }

        private void BeerPictureImageButton_Click(object sender, EventArgs e)
        {
            //imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
            //    Android.OS.Environment.DirectoryPictures), "JakNaPiwo");
            //newImageFile = new File(imageDirectory, String.Format("PhotoNewBeer_{0}.jpg", Guid.NewGuid()));

            var intent = new Intent();
            intent.SetClass(this, typeof(EditPictureActivity));
            //intent.PutExtra("imagePath", (string) newImageFile);
            intent.PutExtra("editBeerId", editBeer.Id);
            StartActivityForResult(intent, 100);

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // tu muszę zrobić by zapisało newImageFile jako nową ścieżkę do zdjęcia, a te poprzednie zdjęcie usuneło
            //File.Delete(editBeer.ImagePath) chyba coś takiego 
            //var Image = (string)imageFile;

            using (var db = new JakNaPiwoContext())
            {
                var beer = db.Beers.FirstOrDefault(b => b.Id == editBeer.Id);

                beer.Name = nameEditText.Text;
                beer.Type = selectedSpinner;
                beer.TypeID = selectedSpinnerID;
                beer.BeerRating = beerRatingRatingBar.Rating;
                beer.ShortDescription = shortDescriptionEditText.Text;
                beer.Price = float.Parse(priceEditText.Text);

                if (receiveImagePath == null)
                {
                    if (image == null)
                    {
                        beer.ImagePath = " ";
                    }
                    else
                    {
                        beer.ImagePath = image;
                    }

                }
                else
                {
                    beer.ImagePath = receiveImagePath;
                }

                db.UpdateBeerTable(beer);

            }

            Toast.MakeText(this, "Piwo " + editBeer.Name + " zostało zaktualizowane!", ToastLength.Short).Show();

            var intent = new Intent();
            intent.SetClass(this, typeof(BeerDetailActivity));
            intent.PutExtra("selectedBeerId", editBeer.Id);

            StartActivityForResult(intent, 100);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            using (var db = new JakNaPiwoContext())
            {
                var beer = db.Beers.FirstOrDefault(b => b.Id == editBeer.Id);

                db.DeleteBeerTable(beer);

            }

            Toast.MakeText(this, "Piwo " + editBeer.Name + " zostało usunięte!", ToastLength.Short).Show();
            Intent nextActivity = new Intent(this, typeof(BeerMenuActivity));
            StartActivity(nextActivity);
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            //clientgps = new ClientGPS();
            //pubLocation = clientgps.currentLocation;
            //Toast.MakeText(this, (string)pubLocation, ToastLength.Short).Show();
            //var intent = new Intent();
            //intent.SetClass(this, typeof(AddPubActivity));
            ////intent.PutExtra("imagePath", (string) newImageFile);
            //intent.PutExtra("editBeerId", editBeer.Id);
            //intent.PutExtra("view", "editBeer");
            //StartActivityForResult(intent, 100);

            var intent = new Intent();
            intent.SetClass(this, typeof(EditPubActivity));
            //intent.PutExtra("imagePath", (string) newImageFile);
            intent.PutExtra("editBeerId", editBeer.Id);
            intent.PutExtra("view", "editBeer");
            intent.PutExtra("editPubId", editBeer.PubID);
            StartActivityForResult(intent, 100);


        }


    }
}