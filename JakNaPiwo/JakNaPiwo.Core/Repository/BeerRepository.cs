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
using SQLite;

namespace JakNaPiwo.Core.Repository
{
    public class BeerRepository
    {
    
        public List<Beer> GetAllBeers()
        {
            using (var db = new JakNaPiwoContext())
            {
                IEnumerable<Beer> beers = db.Beers.ToList();

                return beers.ToList();
            }
            
        }


        public Beer GetBeerById(int beerId)
        {
            IEnumerable<Beer> beers =
               from beerType in beerTypes
               from beer in beerType.Beers
               where beer.Id == beerId
               select beer;
            return beers.FirstOrDefault();
        }

        public List<BeerType> GetBeerTypes()
        {
            return beerTypes;
        }

        public List<Beer> GetBeerForBeerType(int beerTypeId)
        {
            var group = beerTypes.Where(h => h.BeerTypeId == beerTypeId).FirstOrDefault();

            if (group != null)
            {
                return group.Beers;
            }
            return null;
        }

        private static List<BeerType> beerTypes = new List<BeerType>()
        {
            //new BeerType()
            //{
            //    BeerTypeId = 1, Name = "Pszeniczne", Beers = new List<Beer>()
            //    {
            //        new Beer ()
            //        {
            //            Id = 1,
            //            Name = "Ciechan Pszeniczne",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 4,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 2,
            //            Name = "Pilzner",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "4,20",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 3,
            //            PubName = "Biedronka",
            //             PubAdress = "Plac Europejski 3"
            //        }
            //    }
            //},
            //new BeerType()
            //{
            //    BeerTypeId = 2, Name = "Porter", Beers = new List<Beer>()
            //    {
            //        new Beer ()
            //        {
            //            Id = 3,
            //            Name = "Stout",
            //            Type = "Porter",
            //            ShortDescription ="Fajne ciemne piwo",
            //            Price = "8,30",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 5,
            //            PubName = "PiwPaw",
            //            PubAdress = "ul. Żurawia 17a"
            //        },
            //         new Beer ()
            //        {
            //            Id = 4,
            //            Name = "Żywiec Porter",
            //            Type = "Porter",
            //            ShortDescription ="Mało ciekawe",
            //            Price = "3,80",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 2,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        }
            //    }
            //},
            //new BeerType()
            //{
            //    BeerTypeId = 3, Name = "Lager", Beers = new List<Beer>()
            //    {
            //        new Beer ()
            //        {
            //            Id = 5,
            //            Name = "Lech Pils",
            //            Type = "Lager",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 3,
            //            PubName = "Kufle i kapsle",
            //            PubAdress = "ul.Prosta 7"
            //        },
            //         new Beer ()
            //        {
            //            Id = 6,
            //            Name = "Epic Lager",
            //            Type = "Lager",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "7,60",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 3,
            //            PubName = "Pawilony",
            //            PubAdress = "ul.Nowy Świat 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 7,
            //            Name = "Ciechan Pszeniczne",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 4,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 8,
            //            Name = "Ciechan Pszeniczne2",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 4,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 9,
            //            Name = "Ciechan Pszeniczne3",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 4,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 10,
            //            Name = "Ciechan Pszeniczne4",
            //            Type = "Pszeniczne",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = true,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 4,
            //            PubName = "Żabka",
            //            PubAdress = "ul. Karolkowa 34"
            //        },
            //         new Beer ()
            //        {
            //            Id = 11,
            //            Name = "Lech Pils",
            //            Type = "Lager",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 3,
            //            PubName = "Kufle i kapsle",
            //            PubAdress = "ul.Prosta 7"
            //        },
            //            new Beer ()
            //        {
            //            Id = 12,
            //            Name = "Lech Pils2",
            //            Type = "Lager",
            //            ShortDescription ="Fajne, lekkie piwo. W sam raz na spokojny wieczór",
            //            Price = "3,80",
            //            IsFavorite = false,
            //            ImagePath = "/storage/emulated/0/Pictures/JakNaPiwo/PhotoNewBeer_9f73d2f0-3198-4be9-a6ab-cd1db1aa35d7.jpg",
            //            BeerRating = 3,
            //            PubName = "Kufle i kapsle",
            //            PubAdress = "ul.Prosta 7"
            //        }
            //    }
            //}

        };


    }
}