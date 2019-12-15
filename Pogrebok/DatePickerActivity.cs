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
using Shared;

namespace Pogrebok
{
    [Activity(Theme = "@style/Theme.MainPage")]

    class DatePickerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DataPicker);

            string textIntent = Intent.GetStringExtra("fname");

            PogrebokV1 pogreb = Core.GetPogrebokData_temp_calendar(textIntent).Result;

            FindViewById<TextView>(Resource.Id.DatePciker).Text += textIntent;

            FindViewById<TextView>(Resource.Id.MaxTempDateCal).Text += pogreb.street_temp_max_byDateCal;
            FindViewById<TextView>(Resource.Id.MinTempDateCal).Text += pogreb.street_temp_min_byDateCal;


        }
    }
}