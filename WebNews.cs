using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using NewsApp.Droid.Model;

namespace NewsApp.Droid
{
    [Activity(Label = "News")]
    public class WebNews : Activity
    {
        WebView webView;
        string prev;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            string url = Intent.GetStringExtra("URL");
            prev = Intent.GetStringExtra("Category");
            SetContentView(Resource.Layout.layout_web);
            webView = (WebView)FindViewById<WebView>(Resource.Id.webView);
            webView.Settings.JavaScriptEnabled = true;
            webView.SetWebViewClient(new HybridWebViewClient());
            //getSupportActionBar().setDisplayHomeAsUpEnabled(true);
            webView.LoadUrl(url);


        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if(keyCode == Keycode.Back )
            {
                if (webView.CanGoBack())
                {
                    webView.GoBack();
                    return true;
                }
                else
                {
                    var intent = new Intent(this, typeof(Sports));
                    intent.PutExtra("Category", prev);
                    StartActivity(intent);
                    Finish();
                    return true;
                }
            }
           
            return base.OnKeyDown(keyCode, e);
        }
    }
}