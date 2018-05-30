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
using JakNaPiwo.Core.Model;

namespace JakNaPiwo.Core.Repository
{
    public class PubRepository
    {
        public List<Pub> GetAllPubs()
        {
            using (var db = new JakNaPiwoContext())
            {
                IEnumerable<Pub> pubs = db.Pubs.ToList();

                return pubs.ToList();
            }

        }
    }
}