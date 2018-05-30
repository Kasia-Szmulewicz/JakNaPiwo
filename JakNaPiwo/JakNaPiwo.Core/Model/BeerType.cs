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

namespace JakNaPiwo.Core.Model
{
    public class BeerType
    {
        public BeerType()
        {

        }
        public int BeerTypeId { get; set; }

        public string Name { get; set; }

        public List<Beer> Beers { get; set; }
    }
}