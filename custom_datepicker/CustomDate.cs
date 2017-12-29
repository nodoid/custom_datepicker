using System;
using Xamarin.Forms;
namespace custom_datepicker
{
    public class CustomDate : ContentPage
    {
        Entry editPicker;
        CustomDatePicker datePicker;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<IModalDone, string>(this, "ModalClicked", (sender, args) =>
            {
                editPicker.Placeholder = args;
            });
            base.OnAppearing();
        }

        public CustomDate()
        {
            datePicker = new CustomDatePicker
            {
                MinimumDate = DateTime.Now,
                MaximumDate = DateTime.Now.AddDays(7),
                IsVisible = false,
                IsEnabled = false
            };

            editPicker = new Entry
            {
                Placeholder = "Click me!",
            };

            editPicker.Focused += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    datePicker.IsEnabled = true;
                    datePicker.IsVisible = true;
                });
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                        editPicker,
                        datePicker
                    }
            };
        }
    }
}

