using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace NewsApp.Droid
{
    [Activity(Label = "NewsApp")]
    public class MainActivity : Activity
    {
        Button BtnBusiness, BtnSports, BtnScience, BtnTechnology;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Grid_layout);

            BtnBusiness = FindViewById<Button>(Resource.Id.btnbusiness);
            BtnBusiness.Click += (object sender, EventArgs e) =>
            {
                BtnBusiness_Click(sender, e);
            };
            BtnScience = FindViewById<Button>(Resource.Id.btnscience);
            BtnScience.Click += (object sender, EventArgs e) =>
            {
                BtnScience_Click(sender, e);
            };
            BtnSports = FindViewById<Button>(Resource.Id.btnsports);
            BtnSports.Click += (object sender, EventArgs e) =>
            {
                BtnSports_Click(sender, e);
            };
            BtnTechnology = FindViewById<Button>(Resource.Id.btntechnology);
            BtnTechnology.Click += (object sender, EventArgs e) =>
            {
                BtnTechnology_Click(sender, e);
            };
        }

        private void BtnTechnology_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Sports));
            intent.PutExtra("Category", "technology");
            StartActivity(intent);
            Finish();
        }

        private void BtnSports_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Sports));
            intent.PutExtra("Category", "sports");
            StartActivity(intent);
            Finish();
        }

        private void BtnScience_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Sports));
            intent.PutExtra("Category", "science");
            StartActivity(intent);
            Finish();
        }

        private void BtnBusiness_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Sports));
            intent.PutExtra("Category", "bussiness");
            StartActivity(intent);
            Finish();
        }
    }
}