using System;

using Xamarin.Forms;

namespace custom_datepicker
{
    public class App : Application
    {
        public static Size ScreenSize { get; set; }

        public App()
        {
            MainPage = new NavigationPage(new CustomDate());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

