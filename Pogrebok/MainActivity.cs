﻿using Android.App;
using Android.Widget;
using Android.OS;
using Shared;
using Android.Text.Format;
using System;
using Java;
using Android;
using Android.Graphics;
using Android.Content;
using Android.Views;

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
        //string CountTurnOn;
        string PriceEE;
        string Pressure;
        string PrognozTemp;
        string Date_hange;
        //string Heating;



        protected string Save_text = "Temp_min";

        //DateTime time = DateTime.Now;
        PogrebokV1 pogreb = Core.GetPogrebokData().Result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainTiles);

            // Default name for button

            TempMax = FindViewById<TextView>(Resource.Id.TempMax).Text;  //УДАЛИТЬ можно уменьшит размера кода через .text см экран погоды
            TempMin = FindViewById<TextView>(Resource.Id.TempMin).Text;
            TempPogrebok = FindViewById<TextView>(Resource.Id.TempPogrebok).Text;
            TempStreet = FindViewById<TextView>(Resource.Id.TempStreet).Text;
            TempHome = FindViewById<TextView>(Resource.Id.TempHome).Text;
            RashodEE = FindViewById<TextView>(Resource.Id.RashodEE).Text;
            TimeWarm = FindViewById<TextView>(Resource.Id.TimeWarm).Text;
            PriceEE = FindViewById<TextView>(Resource.Id.PriceEE).Text;
            Pressure = FindViewById<TextView>(Resource.Id.Pressure).Text;
            PrognozTemp = FindViewById<TextView>(Resource.Id.buttWeather).Text;
            Date_hange = FindViewById<TextView>(Resource.Id.dateUpdate).Text;


            // Для нового mainTiles
            FindViewById<TextView>(Resource.Id.TempMax).Text = FindViewById<TextView>(Resource.Id.TempMax).Text + "\n\n" + pogreb.street_temp_max + " °C";
            FindViewById<TextView>(Resource.Id.TempMin).Text = FindViewById<TextView>(Resource.Id.TempMin).Text + "\n\n" + pogreb.street_temp_min + " °C";
            FindViewById<TextView>(Resource.Id.TempPogrebok).Text = FindViewById<TextView>(Resource.Id.TempPogrebok).Text + "\n\n" + pogreb.cellar_temp + " °C";
            FindViewById<TextView>(Resource.Id.TempStreet).Text = FindViewById<TextView>(Resource.Id.TempStreet).Text + "\n\n" + pogreb.street_temp_current + " °C";
            FindViewById<TextView>(Resource.Id.TempHome).Text = FindViewById<TextView>(Resource.Id.TempHome).Text + "\n\n" + pogreb.home_temp + " °C";
            FindViewById<TextView>(Resource.Id.RashodEE).Text = FindViewById<TextView>(Resource.Id.RashodEE).Text + "\n\n" + pogreb.kwt_full;
            FindViewById<TextView>(Resource.Id.TimeWarm).Text = FindViewById<TextView>(Resource.Id.TimeWarm).Text + "\n" + pogreb.time_power;
            FindViewById<TextView>(Resource.Id.PriceEE).Text = FindViewById<TextView>(Resource.Id.PriceEE).Text + "\n" + pogreb.price_kWt;
            FindViewById<TextView>(Resource.Id.Pressure).Text = FindViewById<TextView>(Resource.Id.Pressure).Text + "\n\n" + pogreb.pressure;
            FindViewById<TextView>(Resource.Id.dateUpdate).Text = pogreb.date_hange;
            if (pogreb.heating == "1")
                FindViewById<Button>(Resource.Id.butsOnOff).SetBackgroundResource(Resource.Drawable.Power_On);


            Weather1 weather1 = Core_Weather_api.GetWeather().Result;
            FindViewById<TextView>(Resource.Id.buttWeather).Text = FindViewById<TextView>(Resource.Id.buttWeather).Text + "\n\n" + weather1.Temperature;

            //Обновление данных для главной страницы
            Button button = FindViewById<Button>(Resource.Id.Refresh);
            button.Click += delegate
             {
                 pogreb = Core.GetPogrebokData().Result;
                 weather1 = Core_Weather_api.GetWeather().Result;
                 FindViewById<TextView>(Resource.Id.TempMax).Text = TempMax + "\n\n" + pogreb.street_temp_max + " °C";
                 FindViewById<TextView>(Resource.Id.TempMin).Text = TempMin + "\n\n" + pogreb.street_temp_min + " °C";
                 FindViewById<TextView>(Resource.Id.TempPogrebok).Text = TempPogrebok + "\n\n" + pogreb.cellar_temp + " °C";
                 FindViewById<TextView>(Resource.Id.TempStreet).Text = TempStreet + "\n\n" + pogreb.street_temp_current + " °C";
                 FindViewById<TextView>(Resource.Id.TempHome).Text = TempHome + "\n\n" + pogreb.home_temp + " °C";
                 FindViewById<TextView>(Resource.Id.RashodEE).Text = RashodEE + "\n\n" + pogreb.kwt_full;
                 FindViewById<TextView>(Resource.Id.TimeWarm).Text = TimeWarm + "\n" + pogreb.time_power;
                 FindViewById<TextView>(Resource.Id.PriceEE).Text = PriceEE + "\n" + pogreb.price_kWt;
                 FindViewById<TextView>(Resource.Id.Pressure).Text = Pressure + "\n\n" + pogreb.pressure;
                 Save_state();
                 FindViewById<TextView>(Resource.Id.buttWeather).Text = PrognozTemp + "\n\n" + weather1.Temperature;
                 FindViewById<TextView>(Resource.Id.dateUpdate).Text = pogreb.date_hange;
                 if (pogreb.heating == "1")
                     FindViewById<Button>(Resource.Id.butsOnOff).SetBackgroundResource(Resource.Drawable.Power_On);
             };

            button = FindViewById<Button>(Resource.Id.buttWeather);
            button.Click += delegate
              {
                  Intent intent = new Intent(this, typeof(WeatherActivity));
                  StartActivity(intent);
                  OverridePendingTransition(Resource.Animation.trans, Resource.Animation.alpha);
              };

            
            button = FindViewById<Button>(Resource.Id.TempMin);
            button.Click += Temp_MinAlertDialog;

            button = FindViewById<Button>(Resource.Id.TempMax);
            button.Click += Temp_MaxAlertDialog;

            button = FindViewById<Button>(Resource.Id.countTurnOn);
            button.Click += delegate
            {
                setDate();

            };

            //button = FindViewById<Button>(Resource.Id.buttWeather);
            //button.Click += delegate
            //{
            //    Intent intent = new Intent(this, typeof(ShowPref));
            //    StartActivity(intent);
            //    OverridePendingTransition(Resource.Animation.trans, Resource.Animation.alpha);
            //};
        }
        //Сохранение в preference
        void Save_state()
        {
            sPref = GetSharedPreferences("MyPref", FileCreationMode.Private);

            ISharedPreferencesEditor ed = sPref.Edit();
            ed.PutString(Save_text, pogreb.street_temp_min);
            ed.Commit();
            Toast.MakeText(this, "Параметр Минимальная температура сохранена", ToastLength.Short).Show();

        }

        //Дата пикер метод появления
        public void setDate()
        {
            DateTime date = DateTime.Today;
            new DatePickerDialog(this, OnDataSet, date.Year, date.Month-1, date.Day).Show();
        }

        //Диалог отображения минимальной температуры
        private void Temp_MinAlertDialog(object sender, EventArgs e)
        {
            pogreb = Core.GetPogrebokData_temp().Result;
            AlertDialog.Builder adb = new AlertDialog.Builder(this);
            AlertDialog alertDialog = adb.Create();
            alertDialog.SetTitle("Минимальная температура");
            alertDialog.SetMessage("Минимальная температура на улице за месяц = " + pogreb.street_temp_min_byDate + " °C");
            alertDialog.SetButton2("OK", (s, ev) => { });
            alertDialog.SetButton("График за месяц", (s, ev) => { });
            alertDialog.Show();
        }

        //Диалог отображения максимальной температуры
        private void Temp_MaxAlertDialog(object sender, EventArgs e)
        {
            pogreb = Core.GetPogrebokData_temp().Result;
            AlertDialog.Builder adb = new AlertDialog.Builder(this);
            AlertDialog alertDialog = adb.Create();
            alertDialog.SetTitle("Максимальная температура");
            alertDialog.SetMessage("Максимальная температура на улице за месяц = " + pogreb.street_temp_max_byDate + " °C");
            alertDialog.SetButton2("OK", (s, ev) => { });
            alertDialog.SetButton("График за месяц", (s, ev) => { });
            alertDialog.Show();
        }
 
        //Функция сохранения выбор из датапикера
        private void OnDataSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            pogreb.DatePic = e.Date.ToLongDateString();
            string date;
            int month = e.Month + 1;
            date = e.Year.ToString() + "." + month.ToString() + "." + e.DayOfMonth.ToString();

            Intent intent = new Intent(this, typeof(DatePickerActivity));

            intent.PutExtra("fname", date);
            StartActivity(intent);

            //throw new NotImplementedException();
        }
    }
}

