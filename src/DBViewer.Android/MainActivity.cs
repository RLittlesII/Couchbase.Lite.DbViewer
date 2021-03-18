﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace DBViewer.Droid
{
    [Activity(Label = "DBViewer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            InitializePlugins(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            if (App.Current.Resources.TryGetValue("ColoredBackgroundMediumColor", out object mainColorObj))
            {
                var color = (Xamarin.Forms.Color)mainColorObj;
                Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color.ToHex()));
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitializePlugins(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Akavache.Registrations.Start(nameof(DBViewer));
            Couchbase.Lite.Support.Droid.Activate(this);
        }
    }
}