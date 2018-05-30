using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Adapters;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;

namespace JakNaPiwo.Fragments
{
    public class SearchBeerFragment : BaseFragment
    {
        // protected SearchView beerSearchView;
        protected EditText beerSearchEditText;
        private BeerListAdapter searchAdapter;
        List<Beer> searchBeers;

        //++
        protected override void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.beerListView);
            beerSearchEditText = this.View.FindViewById<EditText>(Resource.Id.beerSearchEditText);
        }

        //++
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

            //Jakieś ustawienia EditText
            //beerSearchEditText.Alpha = 0;
            beerSearchEditText.TextChanged += BeerSearchEditText_TextChanged;


            //tu musza być te wybrane piwa 
            beers = beerService.GetAllBeers();
            searchBeers = beers;
            // listView.Adapter = new BeerListAdapter(this.Activity, beers);
            searchAdapter = new BeerListAdapter(this.Activity, beers);
            listView.Adapter = searchAdapter;

            // beerSearchView.QueryTextChange += beerSearchView_QueryTextChange;

        }

        private void BeerSearchEditText_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*searchBeers = (from Beer in beers
                           where Beer.Name.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase) || Beer.ShortDescription.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase) || Beer.PubName.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase)
                           select Beer).ToList<Beer>();
            */

            //List<int> currentPubIds;

            //using (var db = new JakNaPiwoContext())
            //{
            //    currentPubIds = db.Pubs.Where(p => p.Name.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase)).Select(p => p.Id).ToList();
            //}

            //List<Beer> beersFromPubs = beers.Where(b => currentPubIds.Contains(b.PubID)).ToList();

            searchBeers = beers.Where
                        (b => b.Name.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase)
                        || b.ShortDescription.Contains(beerSearchEditText.Text, StringComparison.OrdinalIgnoreCase)).ToList();

            searchAdapter = new BeerListAdapter(this.Activity, searchBeers);
            listView.Adapter = searchAdapter;
        }

        protected override void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            var beer = searchBeers[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(BeerDetailActivity));
            intent.PutExtra("selectedBeerId", beer.Id);

            StartActivityForResult(intent, 100);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            //return base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.SearchBeerFragment, container, false);

        }
    }
}