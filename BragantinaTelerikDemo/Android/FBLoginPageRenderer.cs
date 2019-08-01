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
using Newtonsoft.Json.Linq;
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

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,picture,email&access_token=ACCESS_TOKEN"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    var id = obj["id"].ToString().Replace("\"", "");
                    var name = obj["name"].ToString().Replace("\"", "");
                    var email = obj["email"].ToString().Replace("\"", "");
                    //var tel = obj["mobile_phone"].ToString().Replace("\"", "");
                    var picture = obj["picture"]["data"]["url"];

                    List<string> lista = new List<string>();
                    lista.Add(id);
                    lista.Add(name);
                    lista.Add(picture.ToString());


                    await App.NavigateToProfile(lista);
                }
                else
                {
                    await App.NavigateToProfile(null);
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}