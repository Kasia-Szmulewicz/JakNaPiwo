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
using JakNaPiwo.Adapters;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Core.Service;

namespace JakNaPiwo.Fragments
{
    public class BeerFragment : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();

            HandleEvents();

            beers = beerService.GetAllBeers();
            listView.Adapter = new BeerListAdapter(this.Activity, beers);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.BeerFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

    }
}