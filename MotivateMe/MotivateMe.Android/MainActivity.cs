using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MotivateMe.Droid
{
    [Activity(Label = "MotivateMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            MessagingCenter.Subscribe<AchievementCalculator, string>(this, "Achievement", (sender, arg) =>
            {
                var icon = Resource.Drawable.ic_stat_thumb_up;
                switch ((string)arg)
                {
                    case "Gold":
                        icon = Resource.Drawable.ic_stat_thumb_up_gold;
                        break;
                    case "Silver":
                        icon = Resource.Drawable.ic_stat_thumb_up_silver;
                        break;
                    case "Bronze":
                        icon = Resource.Drawable.ic_stat_thumb_up_bronze;
                        break;
                }
                Console.WriteLine("Setting icon {0}, {1}", arg, icon);
                LocalNotificationsImplementation.NotificationIconId = icon;
            });

            
        }
    }
}