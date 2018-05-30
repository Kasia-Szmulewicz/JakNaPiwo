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
using JakNaPiwo.Core.Repository;

namespace JakNaPiwo.Core.Service
{
    public class BeerService
    {
        private static BeerRepository beerRepository = new BeerRepository();

        public List<Beer> GetAllBeers()
        {
            return beerRepository.GetAllBeers();
        }

        public List<BeerType> GetBeerTypes()
        {
            return beerRepository.GetBeerTypes();
        }

        public List<Beer> GetBeerForBeerType(int hotDogGroupId)
        {
            return beerRepository.GetBeerForBeerType(hotDogGroupId);
        }

        public Beer GetBeerById(int beerId)
        {
            return beerRepository.GetBeerById(beerId);
        }


    }
}