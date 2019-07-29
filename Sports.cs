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
using Newtonsoft.Json;
using Com.Squareup.Picasso;
using Newtonsoft.Json.Linq;
using Org.Json;

using NewsApp.Droid.Model;

namespace NewsApp.Droid
{
    [Activity(Label = "Sports")]
    public class Sports : Activity
    {
        ListView mainList;
        NewsApi news = new NewsApi();
        string cat;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            cat = Intent.GetStringExtra("Category");   //        Intent.GetStringExtra("Category") ? ? "Data is not available";
            SetContentView(Resource.Layout.sport);
            this.Title = cat;
            mainList = (ListView)FindViewById<ListView>(Resource.Id.mainlistview);
            loadNews(cat);
            
        }
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();
                    return true;
            }

            return base.OnKeyDown(keyCode, e);
        }
        private void loadNews(string cat)
        {
            new GetNews(this, news).Execute(Common.Common.APIRequest(cat));
        }

        private class GetNews : AsyncTask<string, Java.Lang.Void, string>
        {
            private Sports activity;
            NewsApi news;
            List<Content> conlist = new List<Content>();
            public GetNews(Sports activity, NewsApi news)
            {
                this.activity = activity;
                this.news = news;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlString = @params[0];
                Helper.Helper http = new Helper.Helper();
                stream = http.GetHTTPData(urlString);
                return stream;
            }
            protected override void OnPostExecute(string result)
            {
                
                
                try
                {
                    base.OnPostExecute(result);
                    
                    if (result.Contains("Error:"))
                    {
                        return;
                    }
                    news = JsonConvert.DeserializeObject<NewsApi>(result);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception : " + ex.Message);
                    return;
                }
                //Add Data
                foreach (var item in news.articles)
                {
                    Content obj = new Content();
                    obj.date = item.publishedAt.ToString();
                    obj.desciption = item.description;
                    obj.imageurl = item.urlToImage;
                    obj.url = item.url;
                    obj.title = item.title;
                    conlist.Add(obj);
                }
                NewsAdapter adapter = new NewsAdapter(activity, conlist);
                activity.mainList.Adapter = adapter;
                activity.mainList.ItemClick += listView_ItemClick;
            }

            private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                var select = conlist[e.Position].url;
                var intent = new Intent(activity, typeof(WebNews));
                intent.PutExtra("URL", select);
                intent.PutExtra("Category", activity.cat);
                activity.StartActivity(intent);
                activity.Finish();
                //Android.Widget.Toast.MakeText(activity, select, Android.Widget.ToastLength.Long).Show();
            }
        }

        public class Content {

            public string date {get;set;}

            public string title {get;set;}

            public string desciption {get;set;}

            public string url { get; set; }
            public string imageurl { get; set; }



        }
        public class NewsAdapter : BaseAdapter<Content>
        {
            public List<Content> sList;
            private Context sContext;
            public NewsAdapter(Context context, List<Content> list)
            {
                sList = list;
                sContext = context;
            }
            public override Content this[int position]
            {
                get
                {
                    return sList[position];
                }
            }
            public override int Count
            {
                get
                {
                    return sList.Count;
                }
            }
            public override long GetItemId(int position)
            {
                return position;
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View row = convertView;
                try
                {
                    if (row == null)
                    {
                        row = LayoutInflater.From(sContext).Inflate(Resource.Layout.News, null, false);
                    }

                    TextView txtDate = row.FindViewById<TextView>(Resource.Id.txtdate);
                    TextView txtTitle = row.FindViewById<TextView>(Resource.Id.txttitle);
                    TextView txtDes = row.FindViewById<TextView>(Resource.Id.txtdescription);
                    //TextView txtImageURL = row.FindViewById<TextView>(Resource.Id.txturlToImage);
                    ImageView imageView = row.FindViewById<ImageView>(Resource.Id.imageview);

                    txtDate.Text = sList[position].date;
                    txtTitle.Text = sList[position].title;
                    txtDes.Text = sList[position].desciption;
                   // txtImageURL.Text = sList[position].url;
                    if (!string.IsNullOrEmpty(sList[position].imageurl))
                    {
                        Picasso.With(sContext.ApplicationContext).Load(Common.Common.GetImage(sList[position].imageurl)).Into(imageView);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally { }
                return row;
            }
        }
    }
}