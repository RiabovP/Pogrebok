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
        //private string mgr;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //Time time = new Time(Time.CurrentTimezone);
            DateTime time = DateTime.Now;
            PogrebokV1 pogreb = Core.GetPogrebokData().Result;
            
            TextView NewFont = FindViewById<TextView>(Resource.Id.text_header);
            //Typeface tf = Typeface.CreateFromAsset(BaseContext.ApplicationContext.Assets, "Fonts/micra.ttf");
            Typeface tf = Typeface.CreateFromAsset(BaseContext.Assets, "Fonts/micra.ttf");
            NewFont.SetTypeface(tf, TypefaceStyle.Normal);

            FindViewById<TextView>(Resource.Id.TempMaxText).Text = pogreb.street_temp_max;
            FindViewById<TextView>(Resource.Id.TempMinText).Text = pogreb.street_temp_min;
            FindViewById<TextView>(Resource.Id.TempCellarText).Text = pogreb.cellar_temp;
            FindViewById<TextView>(Resource.Id.TempStreetText).Text = pogreb.street_temp_current;
            FindViewById<TextView>(Resource.Id.TempHomeText).Text = pogreb.home_temp;
            FindViewById<TextView>(Resource.Id.kwtFullText).Text = pogreb.kwt_full;
            FindViewById<TextView>(Resource.Id.TimePowerText).Text = pogreb.time_power;
            FindViewById<TextView>(Resource.Id.CountTurnOnText).Text = pogreb.count_tarn;
            FindViewById<TextView>(Resource.Id.PricePower).Text = pogreb.price_kWt;
            FindViewById<TextView>(Resource.Id.PressureText).Text = pogreb.pressure;
            FindViewById<TextView>(Resource.Id.DateText).Text = time.ToString("d");
            FindViewById<TextView>(Resource.Id.TimeText).Text = time.ToString("T");
            //FindViewById<TextView>(Resource.Id.TimeText).Text = "";

            Button button = FindViewById<Button>(Resource.Id.GetDataButton);
            button.Click += delegate
             {
                 DateTime time_refresh = DateTime.Now;
                 PogrebokV1 updateData = Core.GetPogrebokData().Result;
                 FindViewById<TextView>(Resource.Id.TempMaxText).Text = updateData.street_temp_max;
                 FindViewById<TextView>(Resource.Id.TempMinText).Text = updateData.street_temp_min;
                 FindViewById<TextView>(Resource.Id.TempCellarText).Text = updateData.cellar_temp;
                 FindViewById<TextView>(Resource.Id.TempStreetText).Text = updateData.street_temp_current;
                 FindViewById<TextView>(Resource.Id.TempHomeText).Text = updateData.home_temp;
                 FindViewById<TextView>(Resource.Id.kwtFullText).Text = updateData.kwt_full;
                 FindViewById<TextView>(Resource.Id.TimePowerText).Text = updateData.time_power;
                 FindViewById<TextView>(Resource.Id.CountTurnOnText).Text = updateData.count_tarn;
                 FindViewById<TextView>(Resource.Id.PricePower).Text = updateData.price_kWt;
                 FindViewById<TextView>(Resource.Id.PressureText).Text = updateData.pressure;
                 FindViewById<TextView>(Resource.Id.DateText).Text = time_refresh.ToString("d");
                 FindViewById<TextView>(Resource.Id.TimeText).Text = time_refresh.ToString("T");
             };
        }
    }
}

