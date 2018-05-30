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
using JakNaPiwo.Core.Repository;
using JakNaPiwo.Core.Service;

namespace JakNaPiwo.Fragments
{
    public class PubFragment : Fragment
    {
        protected List<Pub> pubs;
        protected ListView listView;
        protected Button mapButton;
        protected BeerService beerService;
        protected PubRepository pubService;
        protected List<Beer> beers;

        public PubFragment()
        {
            //otwaorzenie bazy danych
            pubService = new PubRepository();
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            mapButton.Click += MapButton_Click;
        }

        //tu ze mapa z wszystkimi markerami 
        private void MapButton_Click(object sender, EventArgs e)
        {
            //Tu ma być ta mapa ze wszystkimi pinezkami 
            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(MapActivity));
            StartActivityForResult(intent, 100);
        }

        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.pubListView);
            mapButton = this.View.FindViewById<Button>(Resource.Id.mapButton);
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //ListView.ItemClick

            var pub = pubs[e.Position];
            var listView = sender as ListView;

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(PubMapActivity));
            //i tu ma wysyłać id lokallu
            intent.PutExtra("selectedPubId", pub.Id);

            StartActivityForResult(intent, 100);
        }

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

            pubs = pubService.GetAllPubs();
            //beers = beerService.GetAllBeers();
            listView.Adapter = new PubListAdapter(this.Activity, pubs);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.PubFragment, container, false);

        }
    }
}