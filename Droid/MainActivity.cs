
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace custom_datepicker.Droid
{
    [Activity(Label = "custom_datepicker.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Active { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Active = this;

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}

