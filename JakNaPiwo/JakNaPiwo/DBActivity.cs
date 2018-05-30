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
using JakNaPiwo.Core;

namespace JakNaPiwo
{
    [Activity(Label = "DBActivity")]
    public class DBActivity : Activity
    {
        //ListView lstData;
        //List<Beer> lstSource = new List<Beer>();
        //JakNaPiwoContext db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);

            ////jaki widok
            //SetContentView(Resource.Layout.Main);

            ////utworzenie bazy danych 
            //db = new JakNaPiwoContext();
            //db.CreateDataBase();

            //tylko by logi 
            //string dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //Log.Info("DB_PATH", dbFolder);

            //połączenie z widzetami
            //lstData = FindViewById<ListView>(Resource.Id.listView);

            //LoadData
            //LoadData();

            //Event

            //dodanie nowego piwa w AddBeerFragments
            //buttonAdd.Click += delegate
            //{
            //    Beer beer = new Beer()
            //    {
            //        Name = edtName.Text,
            //        Age = "" + int.Parse(edtAge.Text),
            //        Email = edtEmail.Text
            //    };
            //    db.InsertIntoTableBeer(beer);
            //    LoadData();
            //};

            //edycja w EditBeerActivity
            //btnEdit.Click += delegate
            //{
            //    Beer beer = new Beer()
            //    {
            //        Id = int.Parse(edtName.Tag.ToString()),
            //        Name = edtName.Text,
            //        Age = "" + int.Parse(edtAge.Text),
            //        Email = edtEmail.Text
            //    };
            //    //?????czy tak tu a nie ze update
            //    db.UpdateTableBeer();
            //    LoadData();

            //};

            //usunięcie w EditBeerActivity
            //btnDelete.Click += delegate
            //{
            //    Beer beer = new Beer()
            //    {
            //        Id = int.Parse(edtName.Tag.ToString()),
            //        Name = edtName.Text,
            //        Age = "" + int.Parse(edtAge.Text),
            //        Email = edtEmail.Text
            //    };
            //    //?????czy tak tu a nie ze update
            //    db.DeleteTableBeer(beer);
            //    LoadData();

            //};

            //lstData.ItemClick += (s, e) =>{
            //    //set background for selected item

            //    for (int i = 0; i < lstData.Count; i++)
            //    {
            //        if (e.Position == i)
            //            lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //        else
            //            lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
            //    }

            //    //Binding Data
            //    var txtName = e.View.FindViewById<TextView>(Resource.Id.textView);

            //    //ustawienie danych w textwiew
            //    EditText.Text = textName.Text
            //        //to nie wiem co to 
            //        edtName.Tag= else.Id
            //};


        }

        //private void LoadData()
        //{
        //    lstSource = db.SelectTableBeer();
        //    var adapter = new BeerListAdapter(this, lstSource);
        //    lstData.Adapter = adapter;
        //}
    }
}