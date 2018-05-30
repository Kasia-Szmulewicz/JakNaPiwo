using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JakNaPiwo.Utility;
using Java.IO;

namespace JakNaPiwo
{
    [Activity(Label = "Edytuj zdjęcie")]
    public class EditPictureActivity : Activity
    {
        private ImageView takePictureImageView;
        private Button takePictureButton;
        private Button savePictureButton;
        //folder w którym przechowywany jest tymczasowy obraz 
        private File imageDirectory;
        //Ścieżka do zrobionego zdjęcia 
        private File imageFile;
        private Bitmap imageBitmap;
        private string receiveImagePath;
        private int editBeerId;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());

            SetContentView(Resource.Layout.TakePictureView);

            FindViews();

            HandleEvents();

            //utworzenie podfolderu o nazwie JakNaPiwo
            imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "JakNaPiwo");

            if (!imageDirectory.Exists())
            {
                imageDirectory.Mkdirs();
            }

            //getIntent().getSerializableExtra("Myitem");
            //receiveImagePath = Intent.GetStringExtra("imagePath");
            editBeerId = Intent.Extras.GetInt("editBeerId");
            // Toast.MakeText(this, "coś " + receiveImagePath, ToastLength.Short).Show();

        }

        private void FindViews()
        {
            takePictureImageView = FindViewById<ImageView>(Resource.Id.takePictureImageView);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
            savePictureButton = FindViewById<Button>(Resource.Id.savePictureButton);
        }

        private void HandleEvents()
        {
            takePictureButton.Click += TakePictureButton_Click;
            savePictureButton.Click += SavePictureButton_Click;
        }

        private void SavePictureButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(EditBeerActivity));
            intent.PutExtra("editBeerId", editBeerId);
            intent.PutExtra("imagePath", (string) imageFile);

            StartActivityForResult(intent, 100);
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            //pozwala na użycie aparatu 
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            //gdzie obraz ma być zapisany
            imageFile = new File(imageDirectory, String.Format("PhotoNewBeer_{0}.jpg", Guid.NewGuid()));
            //imageFile = new File(receiveImagePath);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            int height = takePictureImageView.Height;
            int width = takePictureImageView.Width;
            imageBitmap = ImageHelper.GetImageBitmapFromFilePath(imageFile.Path, width, height);

            if (imageBitmap != null)
            {
                takePictureImageView.SetImageBitmap(imageBitmap);
                imageBitmap = null;
            }

            //required to avoid memory leaks!
            GC.Collect();
        }



    }
}