using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Utility;
using Java.IO;

namespace JakNaPiwo.Fragments
{
    public class AddBeerFragment : Fragment
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
        private Button addButton;

        private File imageFile;
        private File imageDirectory;

        private string imageFileBeer;
        private string selectedSpinner;
        private int selectedSpinnerID;
        private ArrayAdapter spinnerAdapter;
        private int newPubId;

        public AddBeerFragment()
        {

        }

        protected void FindViews()
        {
            beerPictureImageButton = this.View.FindViewById<ImageButton>(Resource.Id.beerPictureImageButton);
            nameEditText = this.View.FindViewById<EditText>(Resource.Id.nameEditText);
            beerRatingTextView = this.View.FindViewById<TextView>(Resource.Id.beerRatingTextView);
            beerRatingRatingBar = this.View.FindViewById<RatingBar>(Resource.Id.beerRatingRatingBar);
            beerRatingRatingBar.NumStars = 5;
            beerTypeTextView = this.View.FindViewById<TextView>(Resource.Id.beerTypeTextView);
            beerTypeSpinner = this.View.FindViewById<Spinner>(Resource.Id.beerTypeSpinner);
            beerTypeSpinner.Prompt = "Wybierz typ piwa";
            shortDescriptionEditText = this.View.FindViewById<EditText>(Resource.Id.shortDescriptionEditText);
            priceEditText = this.View.FindViewById<EditText>(Resource.Id.priceEditText);
            pubAdresEditText = this.View.FindViewById<EditText>(Resource.Id.pubAdresEditText);
            mapButton = this.View.FindViewById<Button>(Resource.Id.mapButton);
            addButton = this.View.FindViewById<Button>(Resource.Id.addButton);

            int height = 600;
            int width = 220;
            Bitmap imageBitmap = ImageHelper.GetImageBitmapFromFilePath((string)imageFile, width, height);
            beerPictureImageButton.SetImageBitmap(imageBitmap);

        }

        protected void HandleEvents()
        {
            beerPictureImageButton.Click += BeerPictureImageButton_Click;
            mapButton.Click += MapButton_Click;
            addButton.Click += AddButton_Click;
        }

        private void SpinnerStaff()
        {
            beerTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            spinnerAdapter = ArrayAdapter.CreateFromResource(this.Activity, Resource.Array.BeerType, Android.Resource.Layout.SimpleSpinnerItem);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            beerTypeSpinner.Adapter = spinnerAdapter;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            selectedSpinner = spinner.GetItemAtPosition(e.Position).ToString();
            selectedSpinnerID = (int)spinner.GetItemIdAtPosition(e.Position);

           // Toast.MakeText(this.Activity, spinner.GetItemAtPosition(e.Position).ToString(),ToastLength.Short).Show();
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(AddPubActivity));
            //intent.PutExtra("imagePath", (string) newImageFile);
            intent.PutExtra("view", "addBeer");
            StartActivityForResult(intent, 100);

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            using (var db = new JakNaPiwoContext())
            {
                newPubId = db.Pubs.LastOrDefault().Id;
            }


            var newBeer = new Beer
            {
                Name = nameEditText.Text,
                Type = selectedSpinner,
                TypeID = selectedSpinnerID,
                BeerRating = beerRatingRatingBar.Rating,
                ShortDescription = shortDescriptionEditText.Text,
                Price = float.Parse(priceEditText.Text),
                ImagePath = (string)imageFile,
                PubID = newPubId
            };

           Toast.MakeText(this.Activity, "Piwo " + newBeer.Name + " zostało dodane!", ToastLength.Short).Show();

            using (var db = new JakNaPiwoContext())
            {
                db.connection.Insert(newBeer, typeof(Beer));

                var intent = new Intent();
                intent.SetClass(this.Activity, typeof(BeerDetailActivity));
                // pobiera Id ostatnio dodanego elementu
                intent.PutExtra("selectedBeerId", db.Beers.LastOrDefault().Id);
                StartActivityForResult(intent, 100);
            }
        }

        private void BeerPictureImageButton_Click(object sender, EventArgs e)
        {
            imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "JakNaPiwo");
            imageFile = new File(imageDirectory, String.Format("PhotoNewBeer_{0}.jpg", Guid.NewGuid()));

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(TakePictureActivity));
            intent.PutExtra("imagePath", (string)imageFile);
            StartActivityForResult(intent, 100);

        }
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

           // imageFileBeer = Android.Content.Res.Resources.GetString(Resource.String.TakePictureActivity);

            FindViews();

            SpinnerStaff();
            // imageFileBeer = this.Activity.Intent.GetStringExtra("imageFileBeer");
            HandleEvents();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
           // imageFileBeer = this.Activity.Intent.GetStringExtra("imageFileBeer");
                       //imageFileBeer = this.Activity.Intent.GetStringExtra("imageFileBeer");
            return inflater.Inflate(Resource.Layout.AddBeerFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected void Clear()
        {




































            beerPictureImageButton = this.View.FindViewById<ImageButton>(Resource.Id.beerPictureImageButton);
            nameEditText = this.View.FindViewById<EditText>(Resource.Id.nameEditText);
            beerRatingTextView = this.View.FindViewById<TextView>(Resource.Id.beerRatingTextView);
            beerRatingRatingBar = this.View.FindViewById<RatingBar>(Resource.Id.beerRatingRatingBar);
            beerRatingRatingBar.NumStars = 5;
            beerTypeTextView = this.View.FindViewById<TextView>(Resource.Id.beerTypeTextView);
            beerTypeSpinner = this.View.FindViewById<Spinner>(Resource.Id.beerTypeSpinner);
            beerTypeSpinner.Prompt = "Wybierz typ piwa";
            shortDescriptionEditText = this.View.FindViewById<EditText>(Resource.Id.shortDescriptionEditText);
            priceEditText = this.View.FindViewById<EditText>(Resource.Id.priceEditText);
            pubAdresEditText = this.View.FindViewById<EditText>(Resource.Id.pubAdresEditText);
            mapButton = this.View.FindViewById<Button>(Resource.Id.mapButton);
            addButton = this.View.FindViewById<Button>(Resource.Id.addButton);

            int height = 600;
            int width = 220;
            Bitmap imageBitmap = ImageHelper.GetImageBitmapFromFilePath((string)imageFile, width, height);
            beerPictureImageButton.SetImageBitmap(imageBitmap);

        }
    }
}