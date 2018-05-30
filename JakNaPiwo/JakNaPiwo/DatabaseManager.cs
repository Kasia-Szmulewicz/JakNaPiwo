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
using JakNaPiwo.Resources.Database;
using JakNaPiwo.Resources.Model;
using SQLite.Net;

namespace JakNaPiwo
{
    public class DatabaseManager
    {
        SQLiteConnection dbConnection;
        public DatabaseManager()
        {
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        public List<Beer> GetAllEmployees()
        {
            return dbConnection.Query<Beer>("Select * From [Beers]");
        }

        public int SaveEmployee(Beer newBeer)

        {
            return dbConnection.Insert(newBeer);
        }
    }
}