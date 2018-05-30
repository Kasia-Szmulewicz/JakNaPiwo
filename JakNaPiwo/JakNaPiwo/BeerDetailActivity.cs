using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Core.Service;
using JakNaPiwo.Utility;

namespace JakNaPiwo
{
    [Activity(Label = "Szczegóły piwa")]
    public class BeerDetailActivity : Activity
    {
        private ImageView beerImageView;
        private TextView beerNameTextView;
        private RatingBar beerRatingRatingBar;
        private TextView beerTypeTextView;
        private TextView shortDescriptionTextView;
        private TextView priceTextView;
        private TextView pubAdresTextView;
        private Button pubMapButton;
        private Button editButton;
        private Button beerMenuButton;

        private Beer selectedBeer;
        private BeerService beerService;

        private Pub currentPub;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BeerDetailView);

            beerService = new BeerService();

            var selectedBeerId = Intent.Extras.GetInt("selectedBeerId");
            using (var db = new JakNaPiwoContext())
            {
                selectedBeer = db.Beers.FirstOrDefault(b => b.Id == selectedBeerId);

                currentPub = db.Pubs.FirstOrDefault(p => p.Id == selectedBeer.PubID);
            }

            FindViews();
            BindData();
            HandleEvents();

        }


        //przypisanie widzetów do zmiennych
        private void FindViews()
        {
            beerImageView = FindViewById<ImageView>(Resource.Id.beerImageView);
            beerNameTextView = FindViewById<TextView>(Resource.Id.beerNameTextView);
            beerRatingRatingBar = FindViewById<RatingBar>(Resource.Id.beerRatingRatingBar);
            beerRatingRatingBar.NumStars = 5;
            beerTypeTextView = FindViewById<TextView>(Resource.Id.beerTypeTextView);
            shortDescriptionTextView = FindViewById<TextView>(Resource.Id.shortDescriptionTextView);
            priceTextView = FindViewById<TextView>(Resource.Id.priceTextView);
            pubAdresTextView = FindViewById<TextView>(Resource.Id.pubAdresTextView);
            pubMapButton = FindViewById<Button>(Resource.Id.pubMapButton);
            editButton = FindViewById<Button>(Resource.Id.editButton);
            beerMenuButton = FindViewById<Button>(Resource.Id.beerMenuButton);
        }

        private void BindData()
        {
            using (var db = new JakNaPiwoContext())
            {
                currentPub = db.Pubs.FirstOrDefault(p => p.Id == selectedBeer.PubID);
            }

            beerNameTextView.Text = selectedBeer.Name;
            beerTypeTextView.Text = "Typ: " + selectedBeer.Type;
            shortDescriptionTextView.Text = selectedBeer.ShortDescription;
            priceTextView.Text = "Cena: " + selectedBeer.Price + "zł";
            if (currentPub == null)
            {
                pubAdresTextView.Text = " ";
            }
            else
            {
                pubAdresTextView.Text = "Miejsce: " + currentPub.Name + " " + currentPub.Address;
            }

            beerRatingRatingBar.Rating = selectedBeer.BeerRating;

            //int height = beerImageView.Height;
            //int width = beerImageView.Width;
            int height = 200;
            int width = 200;
            Bitmap imageBitmap = ImageHelper.GetImageBitmapFromFilePath(selectedBeer.ImagePath, width, height);
            beerImageView.SetImageBitmap(imageBitmap);

        }

        private void HandleEvents()
        {
            pubMapButton.Click += PubMapButton_Click;
            editButton.Click += EditButton_Click;
            beerMenuButton.Click += BeerMenuButton_Click;
        }

        private void BeerMenuButton_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(BeerMenuActivity));
            StartActivity(nextActivity);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(EditBeerActivity));
            intent.PutExtra("editBeerId", selectedBeer.Id);

            StartActivityForResult(intent, 100);
        }

        private void PubMapButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(PubMapActivity));
            StartActivityForResult(intent, 100);  
            intent.PutExtra("selectedPubId", selectedBeer.PubID);
            StartActivityForResult(intent, 100);
        }
    }
}