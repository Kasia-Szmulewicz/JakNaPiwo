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
using JakNaPiwo.Adapters;
using JakNaPiwo.Core;
using JakNaPiwo.Core.Model;
using JakNaPiwo.Core.Service;
using JakNaPiwo.Fragments;

namespace JakNaPiwo
{
    //MainLauncher = true
    [Activity(Label = "Jak na Piwo", MainLauncher = true, Icon = "@drawable/icon")]
    public class BeerMenuActivity : Activity
    {
        private ListView beerListView;
        private List<Beer> allBeers;
        private BeerService beerService;

        private string saveImageFile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BeerMenuView);

            //beerService = new BeerService();
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Piwa", Resource.Drawable.icon, new BeerFragment());
            AddTab("Puby", Resource.Drawable.icon, new PubFragment());
            AddTab("Dodaj", Resource.Drawable.icon, new AddBeerFragment());
            AddTab("Szukaj", Resource.Drawable.icon, new SearchBeerFragment());


            using (var db = new JakNaPiwoContext())
            {
            }

            //var saveImageFile = Intent.Extras.GetStringExtra("imageFileBeer");

        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            //tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedBeer = beerService.GetBeerById(data.GetIntExtra("selectedBeerId", 0));
            }
        }
    }
}