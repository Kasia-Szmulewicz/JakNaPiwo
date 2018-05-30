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
using SQLite;

namespace JakNaPiwo.Core.Model
{
    [Table("Beer")]
    public class Beer
    {
      
        public Beer()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public float Price { get; set; }

        public string Type { get; set; }

        public int TypeID { get; set; }

        public int PubID { get; set; }

        public string ImagePath { get; set; }

        public float BeerRating { get; set; }

    }
}