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
using JakNaPiwo.Core.Model;

namespace JakNaPiwo.Adapters
{
    //zmiana Beer na Pub  -> public class PubListAdapter : BaseAdapter<Pub>
    public class PubListAdapter : BaseAdapter<Pub>
    {
        List<Pub> items;
        Activity context;

        // public PubListAdapter(Activity context, List<Pub> items) : base()
        public PubListAdapter(Activity context, List<Pub> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        //pozycja elementu na liście
        public override long GetItemId(int position)
        {
            return position;
        }

        //zwraca pub na danej poycji 
        //public override Pub this[int position]
        public override Pub this[int position]
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

            //jeśli nie ma wiersza to musi utworzyć nowy 
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.PubRowView, null);
            }

            //zmiana pól
            convertView.FindViewById<TextView>(Resource.Id.pubNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.locationTextView).Text = item.Address;

            return convertView;

        }
    }
}