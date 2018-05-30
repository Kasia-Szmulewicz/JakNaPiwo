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
using JakNaPiwo.Core.Model;
using JakNaPiwo.Utility;

namespace JakNaPiwo.Adapters
{
    public class BeerListAdapter : BaseAdapter<Beer>
    {
        List<Beer> items;
        Activity context;

        public BeerListAdapter(Activity context, List<Beer> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        //pozycja elementu na liście 
        public override long GetItemId(int position)
        {
            return position;
        }

        //zwraca piwo na danej poycji 
        public override Beer this[int position]
        {
            get
            {
                return items[position];
            }
        }

        //ile rzeczy w liście
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            int width = 40;
            int height = 40; 

            Bitmap imageBitmap = ImageHelper.GetImageBitmapFromFilePath(item.ImagePath, width, height);
            
            //jeśli nie ma wiersza to musi utworzyć nowy 
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.BeerRowView, null);
            }

            convertView.FindViewById<ImageView>(Resource.Id.beerImageView).SetImageBitmap(imageBitmap);
            convertView.FindViewById<TextView>(Resource.Id.beerNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.beerTypeTextView).Text = item.Type;
            convertView.FindViewById<RatingBar>(Resource.Id.beerRatingRatingBar).Rating = item.BeerRating;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = item.Price + "zł";

            return convertView;

        }
    }
}