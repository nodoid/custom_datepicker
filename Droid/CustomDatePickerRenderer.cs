using custom_datepicker.Droid;
using Xamarin.Forms.Platform.Android;
using custom_datepicker;
using Xamarin.Forms;
using Android.Widget;
using System;
using Android.App;
using System.Collections.Generic;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace custom_datepicker.Droid
{
    public class CustomDatePickerRenderer : DatePickerRenderer, IModalDone
    {
        public bool ModalDone { get; set; }
        static Dialog dispModal;
        CustomDatePicker picker;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var dialog = e.NewElement;
                picker = dialog as CustomDatePicker;

                dialog.HorizontalOptions = LayoutOptions.Center;
                dialog.VerticalOptions = LayoutOptions.Center;

                CreateDialog(dialog);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                if (picker.IsVisible)
                    dispModal.Show();
            }
        }

        public void CreateDialog(Xamarin.Forms.DatePicker dialog)
        {
            dispModal = new Dialog(MainActivity.Active, Resource.Style.lightbox_dialog);
            var tmpDate = DateTime.Now;
            dispModal.SetContentView(Resource.Layout.Picker);
            var context = Forms.Context;
            var cYearPos = 0;
            var cMonth = DateTime.Now.Month;

            var txtTitle = dispModal.FindViewById<TextView>(Resource.Id.txtTitle);

            var btnDone = dispModal.FindViewById<Android.Widget.Button>(Resource.Id.btnDone);

            var pickDate = dispModal.FindViewById<Spinner>(Resource.Id.spinDate);
            var pickMonth = dispModal.FindViewById<Spinner>(Resource.Id.spinMonth);
            var pickYear = dispModal.FindViewById<Spinner>(Resource.Id.spinYear);

            var days = new List<int>();

            var c = 0;
            for (var i = dialog.MinimumDate.Date.Day; i < dialog.MaximumDate.Date.Day; i++)
            {
                if (dialog.MinimumDate.AddDays(c).DayOfWeek >= DayOfWeek.Monday && dialog.MinimumDate.AddDays(c).DayOfWeek <= DayOfWeek.Friday)
                    days.Add(dialog.MinimumDate.AddDays(c).Day);
                c++;
            }

            var monthsAll = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var daysInMonth = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(DateTime.Now.Year))
                daysInMonth[2] = 29;

            var months = new List<string>();

            var currentMonth = DateTime.Now.Month;
            months.Add(monthsAll[currentMonth - 1]);
            if (DateTime.Now.Day + 7 > daysInMonth[DateTime.Now.Month - 1])
                months.Add(monthsAll[currentMonth + 1]);
            var years = new List<int> { DateTime.Now.Year };

            if (DateTime.Now.Month == 12 && DateTime.Now.Day > 24)
                years.Add(DateTime.Now.Year + 1);

            var dateAdapter = new ArrayAdapter<int>(MainActivity.Active, Android.Resource.Layout.SimpleSpinnerItem, days);
            dateAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            pickDate.Adapter = dateAdapter;

            var monthAdapter = new ArrayAdapter<string>(MainActivity.Active, Android.Resource.Layout.SimpleSpinnerItem, months);
            monthAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            pickMonth.Adapter = monthAdapter;

            var yearAdapter = new ArrayAdapter<int>(MainActivity.Active, Android.Resource.Layout.SimpleSpinnerItem, years);
            yearAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            pickYear.Adapter = yearAdapter;

            txtTitle.Text = DateTime.Now.Date.ToString("D");

            pickDate.SetPopupBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.White));
            pickDate.TextAlignment = Android.Views.TextAlignment.Center;


            if (months.Count > 1)
                pickMonth.SetSelection(0);
            else
                pickMonth.Enabled = false;
            if (years.Count > 1)
                pickYear.SetSelection(0);
            else
                pickYear.Enabled = false;
            btnDone.Click += (sender, e) =>
            {
                MessagingCenter.Send<IModalDone, string>(this, "ModalClicked", tmpDate.Date.ToString("D"));
                dispModal.Dismiss();
                picker.IsVisible = false;
            };

            pickDate.ItemSelected += (sender, e) =>
            {
                tmpDate = new DateTime(years[cYearPos], cMonth, days[e.Position]);
                txtTitle.Text = tmpDate.ToString("D");
            };

            pickMonth.ItemSelected += (sender, e) =>
            {
                cMonth = e.Position != 0 ? DateTime.Now.Month : DateTime.Now.Month - 1;
                tmpDate = new DateTime(years[cYearPos], cMonth, days[e.Position]);
                txtTitle.Text = tmpDate.ToString("D");
            };

            pickYear.ItemSelected += (sender, e) =>
            {
                if (e.Position != 0)
                {
                    cYearPos += 1;
                    tmpDate = new DateTime(years[cYearPos], cMonth, days[e.Position]);
                    txtTitle.Text = tmpDate.ToString("D");
                }
            };

            // data is in, let's show the dialog box
            //dispModal.Show();
        }

        public static float ConvertDpToPixel(float dp)
        {
            var metrics = Forms.Context.Resources.DisplayMetrics;
            return dp * ((float)metrics.DensityDpi / 160f);
        }
    }
}

