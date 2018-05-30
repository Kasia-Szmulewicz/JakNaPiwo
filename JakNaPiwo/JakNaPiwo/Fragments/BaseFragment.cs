using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Core.Service;

namespace JakNaPiwo.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        protected BeerService beerService;
        protected List<Beer> beers;

        public BaseFragment()
        {
            beerService = new BeerService();
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }

        protected virtual void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.beerListView);
        }

        protected virtual void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var beer = beers[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(BeerDetailActivity));
            intent.PutExtra("selectedBeerId", beer.Id);

            StartActivityForResult(intent, 100);
        }
    }
}