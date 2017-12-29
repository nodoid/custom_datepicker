using System;
using System.Collections.Generic;

using CoreAnimation;

using CoreGraphics;
using Foundation;
using UIKit;

namespace custom_datepicker.iOS
{
    public class MultiClass
    {
        public CGRect rect
        { get; set; }

        public string text
        { get; set; }

        public UITextAlignment alignment
        { get; set; }

        public bool isHeading
        { get; set; }

        public UIColor color
        { get; set; }

        public int lines
        { get; set; }
    }

    public static class UICreation
    {
        private static bool CheckFile(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        public static UIWebView MakeWebViewFragment(CGRect frame, string text)
        {
            var smallWebView = new UIWebView(frame);
            var txt = string.Format("<html><table>{0}</table></html>", text);
            smallWebView.LoadHtmlString(txt, new NSUrl(""));
            return smallWebView;
        }

        public static void MakeHeader(UILabel lbl, string text)
        {
            if (lbl != null)
            {
                lbl.Font = UIFont.BoldSystemFontOfSize(23f);
                lbl.TextAlignment = UITextAlignment.Center;
                lbl.AdjustsFontSizeToFitWidth = true;
                lbl.TextColor = UIColor.Black;
                lbl.Text = text;
            }
        }

        public static void MakeBarButtonItem(UIBarButtonItem item, string filename, UIBarButtonItemStyle style, bool text = false,
                                             float x = -1f, float y = -1f)
        {
            item.Style = style;
            CGSize scaled;

            if (!CheckFile(filename))
            {
#if DEBUG
                Console.WriteLine("Filename failed : {0}", filename);
#endif
                filename = "Graphics/Weather/dunno.png";
            }

            if (x != -1f && y != -1f)
                scaled = new CGSize(x, y);
            else
                scaled = UIImage.FromFile(filename).Size;

            if (scaled.Width > 30f)
                scaled.Width = 30f;
            if (scaled.Height > 30f)
                scaled.Height = 30f;

            if (!AppDelegate.Self.IsIPhone)
            {
                scaled.Height *= 2;
                scaled.Width *= 2;
            }

            if (!text)
            {
                item.Image = UIImage.FromFile(filename).Scale(scaled, AppDelegate.Self.IsRetina ? 2 : 1);
                item.ImageInsets = new UIEdgeInsets(2f, 2f, 2f, 2f);
            }
            else
                item.Title = filename;
        }

        public static UISegmentedControl MakeSegControl(CGRect box, string text1, string text2, int selected = -1)
        {
            var segControl = new UISegmentedControl()
            {
                Frame = box,
            };
            segControl.SetTitle(text1, 0);
            segControl.SetTitle(text2, 1);
            if (selected != -1)
                segControl.SelectedSegment = selected;
            return segControl;
        }

        public static UISegmentedControl MakeSegControl(CGRect box, List<string> segs, int selected = -1)
        {
            var segControl = new UISegmentedControl()
            {
                Frame = box,
            };
            for (int i = 0; i < segs.Count; ++i)
                segControl.SetTitle(segs[i], i);
            if (selected != -1)
                segControl.SelectedSegment = selected;
            return segControl;
        }

        public static void MakeSegControlNoRet(UISegmentedControl ct, List<string> segs, int selected = -1)
        {
            for (int i = 0; i < segs.Count; ++i)
                ct.SetTitle(segs[i], i);
            if (selected != -1)
                ct.SelectedSegment = selected;
        }

        public static UILabel MakeReturnLabel(CGRect box, UITextAlignment align, UIColor color, string text = "", int nolines = 1)
        {
            var tmpLabel = new UILabel()
            {
                Frame = box,
                Text = text,
                TextAlignment = align,
                BackgroundColor = UIColor.Clear,
                TextColor = color,
                AdjustsFontSizeToFitWidth = true,
                Lines = nolines,
            };
            return tmpLabel;
        }

        public static void MakeLabel(UIView view, CGRect box, string text, UITextAlignment align, UIColor color, bool isHeading = false, int lines = 1)
        {
            var tmpLabel = new UILabel()
            {
                Frame = box,
                Text = text,
                TextAlignment = align,
                BackgroundColor = UIColor.Clear,
                TextColor = color,
                AdjustsFontSizeToFitWidth = true,
                Lines = lines
            };
            if (isHeading)
                tmpLabel.Font = UIFont.BoldSystemFontOfSize(21f);
            view.Add(tmpLabel);
        }

        public static UILabel MakeLabelWithTag(UIView view, CGRect box, string text, UITextAlignment align, UIColor color, bool isHeading = false, int lines = 1,
                                               string tag = "")
        {
            var tmpLabel = new UILabel()
            {
                Frame = box,
                Text = text,
                TextAlignment = align,
                BackgroundColor = UIColor.Clear,
                TextColor = color,
                AdjustsFontSizeToFitWidth = true,
                Lines = lines,
                AccessibilityLabel = tag
            };
            if (isHeading)
                tmpLabel.Font = UIFont.BoldSystemFontOfSize(21f);
            //view.Add(tmpLabel);
            return tmpLabel;
        }

        public static UILabel MakeLabelWithTag(UIView view, CGRect box, string text, UITextAlignment align, UIColor color, bool isHeading = false, int lines = 1,
                                               int tag = 0)
        {
            var tmpLabel = new UILabel()
            {
                Frame = box,
                Text = text,
                TextAlignment = align,
                BackgroundColor = UIColor.Clear,
                TextColor = color,
                AdjustsFontSizeToFitWidth = true,
                Lines = lines,
                Tag = tag
            };
            if (isHeading)
                tmpLabel.Font = UIFont.BoldSystemFontOfSize(21f);
            //view.Add(tmpLabel);
            return tmpLabel;
        }

        public static void MakeLabels(UIView view, List<MultiClass> labels)
        {
            var lbls = new List<UILabel>();
            foreach (var l in labels)
            {
                var tmp = new UILabel()
                {
                    Frame = l.rect,
                    Text = l.text,
                    TextAlignment = l.alignment,
                    BackgroundColor = l.color,
                    AdjustsFontSizeToFitWidth = true,
                    Lines = l.lines
                };
                if (l.isHeading)
                    tmp.Font = UIFont.BoldSystemFontOfSize(21f);
                lbls.Add(tmp);
            }
            foreach (var lb in lbls)
                view.AddSubviews(lb);
        }

        public static UITextView MakeTextView(CGRect rectangle, UIReturnKeyType returnKey, UIKeyboardType kbType, bool withBorder = false, bool writeEnabled = true)
        {
            var txtView = new UITextView()
            {
                Frame = rectangle,
                KeyboardType = kbType,
                ReturnKeyType = returnKey,
                BackgroundColor = UIColor.White,
                Editable = writeEnabled
            };
            if (withBorder)
            {
                txtView.Layer.BorderColor = UIColor.Black.CGColor;
                txtView.Layer.BorderWidth = 0.5f;
            }
            return txtView;
        }

        public static UIView CreateUIView(CGRect rect, UIColor color)
        {
            var xOffset = App.ScreenSize.Width == 320 ? 0 : (App.ScreenSize.Width - rect.Width) / 2;
            var yOffset = (App.ScreenSize.Height / 2) - (rect.Height / 2);
            var view = new UIView(new CGRect(xOffset, yOffset, rect.Width, rect.Height))
            {
                BackgroundColor = color
            };
            return view;
        }

        public static UIView CreateUIView(CGPoint rect, UIColor color)
        {
            var xOffset = App.ScreenSize.Width == 320 ? 10 : (App.ScreenSize.Width - 300) / 2;
            var yOffset = (App.ScreenSize.Height / 2) - (rect.Y / 2);
            var view = new UIView(new CGRect(xOffset, yOffset, rect.X, rect.Y))
            {
                BackgroundColor = color
            };
            return view;
        }

        public static UIButton CreateButton(CGRect rect, UIButtonType type, string title)
        {
            var tmp = new UIButton(type)
            {
                Frame = rect
            };
            tmp.SetTitle(title, UIControlState.Normal);
            return tmp;
        }

        public static void CreateBlankSpace(UIView view, CGRect rect)
        {
            var imgView = new UIImageView(rect);
            view.AddSubview(imgView);
        }

        public static UIStepper CreateStepper(CGRect rect, float minValue, float maxValue, string tag = "", int step = 1, bool repeat = true)
        {
            var stepper = new UIStepper()
            {
                Frame = rect,
                MaximumValue = maxValue,
                MinimumValue = minValue,
                StepValue = step,
                AccessibilityLabel = tag,
                AutoRepeat = repeat
            };
            return stepper;
        }

        public static UIButton CreateTextButton(CGPoint where, string text, string tag = "")
        {
            var posn = new CGRect(where.X, where.Y, App.ScreenSize.Width - 10, 40);
            var topLabel = new UILabel(new CGRect(8, 10, 284, 21))
            {
                Text = text,
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(17f),
                AdjustsFontSizeToFitWidth = true,
                BackgroundColor = UIColor.FromRGB(130, 186, 132),
            };
            var myBtn = new UIButton(UIButtonType.Custom)
            {
                Frame = posn,
                BackgroundColor = UIColor.FromRGB(130, 186, 132),
                AccessibilityLabel = tag,
            };
            myBtn.Layer.BorderWidth = 0.8f;
            myBtn.Layer.BorderColor = UIColor.FromRGB(45, 176, 51).CGColor;
            myBtn.AddSubview(topLabel);
            return myBtn;
        }

        public static UIButton CreateTextButton(CGRect where, string text, string tag = "")
        {
            var topLabel = new UILabel(new CGRect(4, 4, where.Width - 8, 21))
            {
                Text = text,
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(17f),
                AdjustsFontSizeToFitWidth = true,
                BackgroundColor = UIColor.FromRGB(130, 186, 132),
            };
            var theButton = new UIButton(UIButtonType.Custom)
            {
                Frame = where,
                BackgroundColor = UIColor.FromRGB(130, 186, 132),
                AccessibilityLabel = tag,
            };
            theButton.Layer.BorderWidth = 0.8f;
            theButton.Layer.BorderColor = UIColor.FromRGB(45, 176, 51).CGColor;

            theButton.AddSubview(topLabel);
            return theButton;
        }

        public static void GiveButtonGradientFill(UIButton btn)
        {
            var gradient = new CAGradientLayer();

            gradient.Colors = new CoreGraphics.CGColor[]
            {
                UIColor.FromRGB(115, 181, 216).CGColor,
                UIColor.FromRGB(35, 101, 136).CGColor
            };

            gradient.Locations = new NSNumber[]
            {
                .5f,
                1f
            };

            gradient.Frame = btn.Layer.Bounds;
            btn.Layer.AddSublayer(gradient);
            btn.Layer.MasksToBounds = true;
        }

        public static UIButton CreateUIButton(this UIButton button)
        {
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.BackgroundColor = UIColor.FromRGB(130, 186, 132);
            button.Layer.CornerRadius = 10f;
            button.TitleLabel.TextColor = UIColor.White;
            button.Layer.BorderWidth = 0.8f;
            button.Layer.BorderColor = UIColor.FromRGB(45, 176, 51).CGColor;
            return button;
        }

        public static void CreateSwitch(UIView view, CGPoint position, int tag = 0)
        {
            var swch = new UISwitch(new CGRect(position.X, position.Y, 51, 31))
            {
                Tag = tag
            };
            view.AddSubview(swch);
        }

        public static UIView MakePrettyView(UIView vwView, float high = 0, float x = 16f, float y = 80f)
        {
            vwView.Layer.CornerRadius = 4f;
            vwView.BackgroundColor = UIColor.FromRGBA(255, 255, 255, 210);

            var divider = AppDelegate.Self.IsRetina ? 2 : 1;
            nfloat height = (nfloat)high;
            /*var width = vwView.Bounds.Width + x > (App.ScreenSize.Width / divider) ? vwView.Bounds.Width - (x * 2) : (App.ScreenSize.Width / divider) - 30;
            if (high == 0f)
                height = vwView.Bounds.Height + y > (App.ScreenSize.Height / divider) ? vwView.Bounds.Height - (y * 2) : (App.ScreenSize.Height / divider) - 150;*/

            var width = vwView.Bounds.Width + x > (App.ScreenSize.Width / divider) ? vwView.Bounds.Width - (x * 2) : vwView.Frame.Width;
            if (high == 0f)
                height = vwView.Bounds.Height + y > (App.ScreenSize.Height / divider) ? vwView.Bounds.Height - (y * 2) : vwView.Frame.Height;

            if (!AppDelegate.Self.IsIPhone)
            {
                x = (float)App.ScreenSize.Width - ((float)width / 2);
                //y = (float)App.ScreenSize.Height - ((float)height / 2);
            }
            vwView.Frame = new CGRect(x, y, AppDelegate.Self.IsIPhone ? 308 : width, height);
            vwView.Layer.BorderWidth = 1.4f;
            //vwView.Layer.BorderColor = UIColor.Red.CGColor;
            vwView.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            vwView.Layer.ShadowOpacity = 0.75f;
            return vwView;
        }
    }
}

