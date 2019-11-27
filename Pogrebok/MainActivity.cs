using Android.App;
using Android.Widget;
using Android.OS;
using Shared;
using Android.Text.Format;
using System;
using Java;
using Android;
using Android.Graphics;
using Android.Content;

namespace Pogrebok
{
    [Activity(Theme = "@style/Theme.MainPage")]
    public class MainActivity : Activity
    {
        ISharedPreferences sPref;
        string TempMax;
        string TempMin;
        string TempPogrebok;
        string TempStreet;
        string TempHome;
        string RashodEE;
        string TimeWarm;
        string CountTurnOn;
        string PriceEE;
        string Pressure;
        string PrognozTemp;

        protected string Save_text = "Temp_min";

        //DateTime time = DateTime.Now;
        PogrebokV1 pogreb = Core.GetPogrebokData().Result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainTiles);

            // Default name for button

            TempMax= FindViewById<TextView>(Resource.Id.TempMax).Text;
            TempMin = FindViewById<TextView>(Resource.Id.TempMin).Text;
            TempPogrebok = FindViewById<TextView>(Resource.Id.TempPogrebok).Text;
            TempStreet = FindViewById<TextView>(Resource.Id.TempStreet).Text;
            TempHome = FindViewById<TextView>(Resource.Id.TempHome).Text;
            RashodEE = FindViewById<TextView>(Resource.Id.RashodEE).Text;
            TimeWarm = FindViewById<TextView>(Resource.Id.TimeWarm).Text;
            CountTurnOn = FindViewById<TextView>(Resource.Id.countTurnOn).Text;
            PriceEE = FindViewById<TextView>(Resource.Id.PriceEE).Text;
            Pressure = FindViewById<TextView>(Resource.Id.Pressure).Text;
            PrognozTemp = FindViewById<TextView>(Resource.Id.buttWeather).Text;

            // Для нового mainTiles
            FindViewById<TextView>(Resource.Id.TempMax).Text = FindViewById<TextView>(Resource.Id.TempMax).Text + "\n\n" + pogreb.street_temp_max;
            FindViewById<TextView>(Resource.Id.TempMin).Text = FindViewById<TextView>(Resource.Id.TempMin).Text +"\n\n" + pogreb.street_temp_min;
            FindViewById<TextView>(Resource.Id.TempPogrebok).Text = FindViewById<TextView>(Resource.Id.TempPogrebok).Text + "\n\n" + pogreb.cellar_temp;
            FindViewById<TextView>(Resource.Id.TempStreet).Text = FindViewById<TextView>(Resource.Id.TempStreet).Text + "\n\n" + pogreb.street_temp_current;
            FindViewById<TextView>(Resource.Id.TempHome).Text = FindViewById<TextView>(Resource.Id.TempHome).Text + "\n\n" + pogreb.home_temp;
            FindViewById<TextView>(Resource.Id.RashodEE).Text = FindViewById<TextView>(Resource.Id.RashodEE).Text + "\n\n" + pogreb.kwt_full;
            FindViewById<TextView>(Resource.Id.TimeWarm).Text = FindViewById<TextView>(Resource.Id.TimeWarm).Text + "\n" + pogreb.time_power;
            FindViewById<TextView>(Resource.Id.countTurnOn).Text = FindViewById<TextView>(Resource.Id.countTurnOn).Text + "\n\n" + pogreb.count_tarn;
            FindViewById<TextView>(Resource.Id.PriceEE).Text = FindViewById<TextView>(Resource.Id.PriceEE).Text + "\n\n" + pogreb.price_kWt;
            FindViewById<TextView>(Resource.Id.Pressure).Text = FindViewById<TextView>(Resource.Id.Pressure).Text + "\n\n" + pogreb.pressure;
           
            // Weather weather1 = CoreWeather.GetWeather("Новосибирск", true).Result;
            //FindViewById<TextView>(Resource.Id.buttWeather).Text = FindViewById<TextView>(Resource.Id.buttWeather).Text + "\n\n" + weather1.Temperature;

            Button button = FindViewById<Button>(Resource.Id.Refresh);
            button.Click += delegate
             {
                 pogreb = Core.GetPogrebokData().Result;
                 //weather1 = CoreWeather.GetWeather("Новосибирск", true).Result;
                 FindViewById<TextView>(Resource.Id.TempMax).Text = TempMax + "\n\n" + pogreb.street_temp_max;
                 FindViewById<TextView>(Resource.Id.TempMin).Text = TempMin + "\n\n" + pogreb.street_temp_min;
                 FindViewById<TextView>(Resource.Id.TempPogrebok).Text = TempPogrebok + "\n\n" + pogreb.cellar_temp;
                 FindViewById<TextView>(Resource.Id.TempStreet).Text = TempStreet + "\n\n" + pogreb.street_temp_current;
                 FindViewById<TextView>(Resource.Id.TempHome).Text = TempHome + "\n\n" + pogreb.home_temp;
                 FindViewById<TextView>(Resource.Id.RashodEE).Text = RashodEE + "\n\n" + pogreb.kwt_full;
                 FindViewById<TextView>(Resource.Id.TimeWarm).Text = TimeWarm + "\n" + pogreb.time_power;
                 FindViewById<TextView>(Resource.Id.countTurnOn).Text = CountTurnOn + "\n\n" + pogreb.count_tarn;
                 FindViewById<TextView>(Resource.Id.PriceEE).Text = PriceEE + "\n\n" + pogreb.price_kWt;
                 FindViewById<TextView>(Resource.Id.Pressure).Text = Pressure + "\n\n" + pogreb.pressure;
                 Save_state();
                 //FindViewById<TextView>(Resource.Id.buttWeather).Text = PrognozTemp + "\n\n" + weather1.Temperature;


             };

            //button = FindViewById<Button>(Resource.Id.buttWeather);
            //button.Click += delegate
            //  {
            //      Intent intent = new Intent(this, typeof(WeatherActivity));
            //      StartActivity(intent);
            //  };

            button = FindViewById<Button>(Resource.Id.buttWeather);
            button.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ShowPref));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.trans, Resource.Animation.alpha);
            };
        }
        void Save_state()
        {
            sPref = GetSharedPreferences("MyPref", FileCreationMode.Private);

            ISharedPreferencesEditor ed = sPref.Edit();
            ed.PutString(Save_text, pogreb.street_temp_min);
            ed.Commit();
            Toast.MakeText(this, "Параметр Минимальная температура сохранена", ToastLength.Short).Show();

        }
    }
}

