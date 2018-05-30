using System;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using JakNaPiwo.Core.Model;
using SQLite;

namespace JakNaPiwo.Core
{
    public  class JakNaPiwoContext : IDisposable
    {
        private string dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public string dbName = "JakNaPiwo.db";

        public SQLite.SQLiteConnection connection;

        public TableQuery<Beer> Beers { get; set; }
        public TableQuery<Pub> Pubs { get; set; }


        public JakNaPiwoContext()
        {
            connection = new SQLiteConnection(System.IO.Path.Combine(dbFolder, dbName));


            Initialize();
            //Seed();

            Beers = connection.Table<Beer>();
            Pubs = connection.Table<Pub>();
        }

        private void Seed()
        {
            connection.DropTable<Beer>();
            connection.DropTable<Pub>();

            connection.CreateTable<Beer>();
            connection.CreateTable<Pub>();


            Beers = connection.Table<Beer>();
            Pubs = connection.Table<Pub>();
        }

        private void Initialize()
        {
            connection.CreateTable<Beer>();
            connection.CreateTable<Pub>();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void UpdateBeerTable(Beer beer)
        {
            connection.Query<Beer>("UPDATE Beer SET Name=?, Type=?, TypeID=?, ShortDescription=?, Price=?, ImagePath=?, BeerRating=? WHERE Id=?", 
                                                    beer.Name, beer.Type, beer.TypeID, beer.ShortDescription, beer.Price, beer.ImagePath, beer.BeerRating, beer.Id);
        }

        public void DeleteBeerTable(Beer beer)
        {
            connection.Delete(beer);
        }

        public void UpdatePubTable(Pub pub)
        {
            connection.Query<Pub>("UPDATE Pub SET Name=?, Address=?, City=?, Street=?, Number=?, PubLatitude=?, PubLongitude=? WHERE Id=?",
                                                    pub.Name, pub.Address, pub.City, pub.Street, pub.Number, pub.PubLatitude, pub.PubLongitude ,pub.Id);
        }



    }
}