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

namespace Pogrebok
{
    [Activity(Theme = "@style/Theme.MainPage")]
    public class ShowPref:Activity
    {
        //TextView text_load;
        ISharedPreferences sPref;

        protected string Save_text = "Temp_min";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PrefLayout);

            Load_text();

            //FindViewById<TextView>(Resource.Id.textPref);
        }

        void Load_text()
        {
            sPref = GetSharedPreferences("MyPref", FileCreationMode.Private);
            String savedText = sPref.GetString(Save_text, "");
            FindViewById<TextView>(Resource.Id.textPref).Text=savedText;
            Toast.MakeText(this, "Сашка, на этом экране загрузились данные из файла куда сохранились данные на первом экране", ToastLength.Long).Show();
        }

    }
}


