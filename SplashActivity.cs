using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HeyApp
{
    [Activity(Label = "HeyApp",MainLauncher =true,Theme ="@style/MySplashTheme",NoHistory =true)]
    public class SplashActivity :Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
           // Thread.Sleep(3000);
            Finish();
            // Create your application here
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}