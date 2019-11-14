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
using BragantinaTelerikDemo.Portable;
using BragantinaTelerikDemo.Portable.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginFbView), typeof(BragantinaTelerikDemo.Android.FBLoginPageRenderer))]

namespace BragantinaTelerikDemo.Android
{
    public class FBLoginPageRenderer : PageRenderer
    {
        public FBLoginPageRenderer(Context context) : base(context)
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "903786106680680",
                //clientId: "2334801723209928",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    try
                    {
                        var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                        var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                        var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                        //var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/v5.0/me?fields=id%2Cname%2Cemail%2Cpicture&access_token=ACCESS_TOKEN"), null, eventArgs.Account);
                        //var response = await request.GetResponseAsync();

                        var client = new RestClient("https://graph.facebook.com/v5.0/me?fields=id%2Cname%2Cemail%2Cpicture&access_token=" + accessToken);
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Connection", "keep-alive");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", "graph.facebook.com");
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Accept", "*/*");
                        IRestResponse response = client.Execute(request);

                        var obj = JsonConvert.DeserializeObject<Facebook>(response.Content);
                        //var obj = JObject.Parse(response.GetResponseText());



                        var id = obj.id.Replace("\"", "");
                        var name = obj.name.Replace("\"", "");
                        var email = !string.IsNullOrEmpty(obj.email) ? obj.email.Replace("\"", "") : "";
                        var picture = obj.picture.data.url;

                        List<string> lista = new List<string>();
                        lista.Add(id);
                        lista.Add(name);
                        lista.Add(picture);
                        lista.Add(email);



                        await App.NavigateToProfile(lista);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    await App.NavigateToProfile(null);
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }

    class Facebook
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public picture picture { get; set; }

        public Facebook()
        {
            picture = new picture();
        }
    }


    class picture
    {
        public data data { get; set; }

        public picture()
        {
            data = new data();
        }
    }

    class data
    {
        public string url { get; set; }
    }
}