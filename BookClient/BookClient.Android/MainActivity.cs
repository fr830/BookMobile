using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using Java.Lang;
using Java.Lang.Reflect;

namespace BookClient.Droid
{
    [Activity(Label = "BookClient.Views", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            // this.ActionBar.SetBackgroundDrawable((new ColorDrawable(Color.ParseColor("#FFFFFF"))));
            
            //SettingsLoader.Loader = new StreamLoader(this);
            //var data = await SettingsLoader.ImprovedLoadAsync();
        }
	}
}

