using Android.App;
using Android.Widget;
using Android.OS;
using Shared;
using Android.Text.Format;
using System;
using Java;
using Android;
using Android.Graphics;

namespace Pogrebok
{
    [Activity(Theme = "@style/Theme.MainPage")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainTiles);

            //DateTime time = DateTime.Now;
            PogrebokV1 pogreb = Core.GetPogrebokData().Result;

            // Default name for button

            string TempMax= FindViewById<TextView>(Resource.Id.TempMax).Text;
            string TempMin = FindViewById<TextView>(Resource.Id.TempMin).Text;
            string TempPogrebok = FindViewById<TextView>(Resource.Id.TempPogrebok).Text;
            string TempStreet = FindViewById<TextView>(Resource.Id.TempStreet).Text;
            string TempHome = FindViewById<TextView>(Resource.Id.TempHome).Text;
            string RashodEE = FindViewById<TextView>(Resource.Id.RashodEE).Text;
            string TimeWarm = FindViewById<TextView>(Resource.Id.TimeWarm).Text;
            string CountTurnOn = FindViewById<TextView>(Resource.Id.countTurnOn).Text;
            string PriceEE = FindViewById<TextView>(Resource.Id.PriceEE).Text;
            string Pressure = FindViewById<TextView>(Resource.Id.Pressure).Text;

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

                Button button = FindViewById<Button>(Resource.Id.Refresh);
                button.Click += delegate
                 {
                     //FindViewById<TextView>(Resource.Id.TempMax).Text = "";
                     FindViewById<TextView>(Resource.Id.TempMax).Text = TempMax + "\n\n" + pogreb.street_temp_max;
                     //FindViewById<TextView>(Resource.Id.TempMin).Text = "";
                     FindViewById<TextView>(Resource.Id.TempMin).Text = TempMin + "\n\n" + pogreb.street_temp_min;
                     //FindViewById<TextView>(Resource.Id.TempPogrebok).Text = "";
                     FindViewById<TextView>(Resource.Id.TempPogrebok).Text = TempPogrebok + "\n\n" + pogreb.cellar_temp;
                     //FindViewById<TextView>(Resource.Id.TempStreet).Text = "";
                     FindViewById<TextView>(Resource.Id.TempStreet).Text = TempStreet + "\n\n" + pogreb.street_temp_current;
                     //FindViewById<TextView>(Resource.Id.TempHome).Text = "";
                     FindViewById<TextView>(Resource.Id.TempHome).Text = TempHome + "\n\n" + pogreb.home_temp;
                     //FindViewById<TextView>(Resource.Id.RashodEE).Text = "";
                     FindViewById<TextView>(Resource.Id.RashodEE).Text = RashodEE + "\n\n" + pogreb.kwt_full;
                     //FindViewById<TextView>(Resource.Id.TimeWarm).Text = "";
                     FindViewById<TextView>(Resource.Id.TimeWarm).Text = TimeWarm + "\n" + pogreb.time_power;
                     //FindViewById<TextView>(Resource.Id.countTurnOn).Text = "";
                     FindViewById<TextView>(Resource.Id.countTurnOn).Text = CountTurnOn + "\n\n" + pogreb.count_tarn;
                     //FindViewById<TextView>(Resource.Id.PriceEE).Text = "";
                     FindViewById<TextView>(Resource.Id.PriceEE).Text = PriceEE + "\n\n" + pogreb.price_kWt;
                     FindViewById<TextView>(Resource.Id.Pressure).Text = Pressure + "\n\n" + pogreb.pressure;
                 };
         }
    }
}

