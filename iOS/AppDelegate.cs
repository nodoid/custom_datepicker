using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;

namespace custom_datepicker.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static AppDelegate Self { get; set; }

        public bool IsRetina { get; private set; }

        public bool IsIPhone { get; private set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppDelegate.Self = this;

            global::Xamarin.Forms.Forms.Init();
            App.ScreenSize = new Size(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            IsIPhone = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
            IsRetina = UIScreen.MainScreen.RespondsToSelector(new Selector("scale")) ? true : false;
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

