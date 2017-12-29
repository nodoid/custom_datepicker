using System;
using System.Collections.Generic;
using CoreGraphics;
using custom_datepicker;
using custom_datepicker.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace custom_datepicker.iOS
{
    public class CustomDatePickerRenderer : DatePickerRenderer, IModalDone
    {
        public bool ModalDone { get; set; }
        CustomDatePicker picker;
        UIView view;

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var dialog = e.NewElement;
                picker = dialog as CustomDatePicker;
                CreateDialog(dialog);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {
                if (picker.IsVisible)
                {
                    UIApplication.SharedApplication.KeyWindow.RootViewController.Add(view);
                    picker.IsVisible = false;
                }
            }
        }

        public void CreateDialog(DatePicker dialog)
        {
            view = new UIView(new CGRect(8, App.ScreenSize.Height / 2 - 70, App.ScreenSize.Width, 140));
            view = UICreation.MakePrettyView(view, 140, 32, (float)App.ScreenSize.Height / 2 - 70);

            var lblTitle = new UILabel(new CGRect(8, 4, 300, 24))
            {
                Text = DateTime.Now.Date.ToString("D"),
                TextColor = UIColor.Blue,
                TextAlignment = UITextAlignment.Center,
            };
            view.Add(lblTitle);

            var width = App.ScreenSize.Width - 20;
            var picker = new UIPickerView(new CGRect(8, 40, 300, 64))
            {
                Model = new DataModel(lblTitle, dialog)
            };

            var btnMid = new UIButton
            {
                Frame = new CGRect(width / 3, 100, 60, 30),
            };
            btnMid.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            btnMid.SetTitle("Done", UIControlState.Normal);
            view.Add(picker);
            view.Add(btnMid);

            btnMid.TouchUpInside += delegate
            {
                MessagingCenter.Send<IModalDone, string>(this, "ModalClicked", Convert.ToDateTime(lblTitle.Text).Date.ToString("D"));
                view.RemoveFromSuperview();
            };
        }
    }

    public class DataModel : UIPickerViewModel
    {
        UILabel lbl;
        DatePicker dialog;

        List<List<string>> data;
        List<string> months, days, years;

        int currentYear, currentMonth;

        public DataModel(UILabel lblText, DatePicker dia)
        {
            data = new List<List<string>>();
            lbl = lblText;
            dialog = dia;

            days = new List<string>();

            var c = 0;
            for (var i = dialog.MinimumDate.Date.Day; i < dialog.MaximumDate.Date.Day; i++)
            {
                if (dialog.MinimumDate.AddDays(c).DayOfWeek >= DayOfWeek.Monday && dialog.MinimumDate.AddDays(c).DayOfWeek <= DayOfWeek.Friday)
                    days.Add(dialog.MinimumDate.AddDays(c).Day.ToString());
                c++;
            }

            var monthsAll = new List<string> { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var daysInMonth = new List<int> { -1, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(DateTime.Now.Year))
                daysInMonth[2] = 29;

            months = new List<string>();

            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;

            months.Add(monthsAll[currentMonth - 1]);
            if (DateTime.Now.Day + 7 > daysInMonth[DateTime.Now.Month - 1])
                months.Add(monthsAll[currentMonth]);
            years = new List<string> { DateTime.Now.Year.ToString() };

            if (DateTime.Now.Month == 12 && DateTime.Now.Day > 24)
                years.Add((DateTime.Now.Year + 1).ToString());

            data.Add(days);
            data.Add(months);
            data.Add(years);
        }

        public override nint GetComponentCount(UIPickerView v)
        {
            return (nint)data.Count;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return (nint)data[(int)component].Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return data[(int)component][(int)row];
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            var three = picker.SelectedRowInComponent(2);
            var two = picker.SelectedRowInComponent(1);
            var one = picker.SelectedRowInComponent(0);

            Console.WriteLine("hello");

            lbl.Text = new DateTime(currentYear + (int)three, currentMonth + (int)two, Convert.ToInt32(days[(int)one])).ToString("D");
        }

        public override nfloat GetComponentWidth(UIPickerView picker, nint component)
        {
            return 100f;
        }
    }
}

