using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pogrebok
{
    [Activity(Theme = "@style/Theme.MainPage")]
    public class WeatherActivity:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WeatherScreen);

            WeatherDay weather = Core_Weather_api.GetWeather().Result;

            FindViewById<TextView>(Resource.Id.cityTemp).Text = weather.Temperature;
            FindViewById<TextView>(Resource.Id.cityWind).Text = weather.Wind;
            FindViewById<TextView>(Resource.Id.cityHumidity).Text = weather.Humidity;
            FindViewById<TextView>(Resource.Id.cityVisible).Text = weather.Visibility;
            FindViewById<TextView>(Resource.Id.cityPressure).Text = weather.Pressure;
            FindViewById<TextView>(Resource.Id.citySunRise).Text = weather.Sunrise;
            FindViewById<TextView>(Resource.Id.citySunset).Text = weather.Sunset;

            int date_id = Resource.Id.Date_0;
            int day_id = Resource.Id.Day_0;
            int tempH_id = Resource.Id.tempHigh_0;
            int tempL_id = Resource.Id.tempLow_0;

            for (int i = 0; i < 10; i++)
            {
                FindViewById<TextView>(date_id++).Text = weather.date[i];
                FindViewById<TextView>(day_id++).Text = weather.day[i];
                if (i != 9)
                {
                    FindViewById<TextView>(tempH_id++).Text = weather.temp_high[i] + " " + FindViewById<TextView>(tempH_id).Text.ToString();
                    FindViewById<TextView>(tempL_id++).Text = weather.temp_low[i] + " " + FindViewById<TextView>(tempL_id).Text.ToString();
                }
                else if (i==9) //”словие дл€ правильный работы крайнего дн€ (low крашилс€ / high отображалс€ с начальной температурой Low
                {
                    FindViewById<TextView>(tempH_id).Text = weather.temp_high[i] + " " + FindViewById<TextView>(tempH_id).Text.ToString();
                    FindViewById<TextView>(tempL_id).Text = weather.temp_low[i] + " " + FindViewById<TextView>(tempL_id).Text.ToString();
                }
            }
        }
    }
}