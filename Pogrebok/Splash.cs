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
    [Activity(Label = "Pogrebok", Icon = "@drawable/icon_pog",
        MainLauncher = true, Theme = "@style/Theme.Splash")]
    public class Splash:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //PogrebokV1 p_null = Core.GetPogrebokData().Result;
            Intent intetn = new Intent(this, typeof(MainActivity));
            StartActivity(intetn);
            Finish();
        }
    }
}