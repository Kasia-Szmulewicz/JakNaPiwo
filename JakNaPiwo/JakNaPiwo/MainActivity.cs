using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace JakNaPiwo
{

    // Icon = "@drawable/icon"  to co bedzie wysweitlane w lewym górnym roku - ikonka 
    // MainLauncher = true ze ma być uruchamiany jak program jest uruchamiany 

    [Activity(Label = "Jak Na Piwo", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);



            Button someButton = FindViewById<Button>(Resource.Id.sombutton);
            someButton.Click += (s, e) =>
            {
                Intent nextActivity = new Intent(this, typeof(BeerDetailActivity));
                StartActivity(nextActivity);
                //nextActivity.PutExtra("email", zmiennatextview.Text); coś takiego chyba przkazuje wartość do nastepnego widoku 
                // w tym 2 widoku by odebrać 
                // string zmiannawktorejprzechowuje = Intetnt.GetStringExtra("name" ?? "Not recv");
                //GetStringExtra() do odczytu wysłanych danych z poprzedniej aktywności 
                //PutExtra() używamy do przesyłania danych między activity 
                //musimy ją uruchomić 
               
            };
        }
    }
}

