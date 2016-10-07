using Foundation;
using System;
using UIKit;
using Communere.iOS.Classes;
using CoreGraphics;
using System.Collections.Generic;
using XamDialogs;

namespace Communere.iOS
{
    public partial class VC_SecondMale : UIViewController
    {
        public VC_SecondMale (IntPtr handle) : base (handle)
        {
        }

        //prop
        public string CellPhone {
            get;
            set;
        }


        //Objects
        private UITextField militaryTxt, jobTxt;
        private UILabel militaryLabel, jobLabel;
        private UIButton militaryBtn, jobBtn, nextButton;

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            //set Background Image
            UIGraphics.BeginImageContext (this.View.Frame.Size);
            var i = UIImage.FromFile ("Background.jpg");
            i = i.Scale (this.View.Frame.Size);
            this.View.BackgroundColor = UIColor.FromPatternImage (i);


            //add Title
            var label = new UILabel () {
                TextColor = AppGlobal.Colors.GreenColor,
                Text = "Second",
                Font = UIFont.FromName (FontsName.RegularEn, 20f),
            };
            label.SizeToFit ();
            NavigationItem.TitleView = label;

            //set Back btn
            NavigationItem.SetLeftBarButtonItem (
                new UIBarButtonItem (UIImage.FromFile ("BackModal.png")
                    , UIBarButtonItemStyle.Plain
                    , (sender, args) => {
                        // button was clicked
                        NavigationController.PopViewController (true);
                    })
                , true);
            NavigationItem.LeftBarButtonItem.TintColor = AppGlobal.Colors.GreenColor;



            #region Objects
            //Set military Label
            militaryLabel = new UILabel (new CGRect (5, this.NavigationController.NavigationBar.Frame.Bottom + 20, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "What's your military service status ?",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };


            //set militaryTxt
            militaryTxt = new UITextField (new CGRect (View.Frame.Width / 4, militaryLabel.Frame.Bottom + (15), View.Frame.Width / 2, 30)) {
                ClearButtonMode = UITextFieldViewMode.Never,
                BorderStyle = UITextBorderStyle.None,
                ReturnKeyType = UIReturnKeyType.Done,
                AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                Font = UIFont.FromName (FontsName.RegularEn, 16f),
                TextColor = AppGlobal.Colors.GreenColor,
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.FromWhiteAlpha (1f, .1f),
                TintColor = AppGlobal.Colors.GreenColor,
                KeyboardType = UIKeyboardType.Default,
                Enabled = false,
                AttributedPlaceholder = new NSAttributedString ("Select status", UIFont.FromName (FontsName.RegularEn, 15f), UIColor.FromWhiteAlpha (1.0f, 0.5f))
            };
            militaryTxt.Layer.CornerRadius = militaryTxt.Frame.Height / 2;

            //set military Btn
            militaryBtn = new UIButton (militaryTxt.Frame);


            //Set job Label
            jobLabel = new UILabel (new CGRect (5, this.militaryTxt.Frame.Bottom + 25, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "What's your Contract type ?",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };


            //set jobTxt
            jobTxt = new UITextField (new CGRect (View.Frame.Width / 4, jobLabel.Frame.Bottom + (15), View.Frame.Width / 2, 30)) {
                ClearButtonMode = UITextFieldViewMode.Never,
                BorderStyle = UITextBorderStyle.None,
                ReturnKeyType = UIReturnKeyType.Done,
                AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                Font = UIFont.FromName (FontsName.RegularEn, 16f),
                TextColor = AppGlobal.Colors.GreenColor,
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.FromWhiteAlpha (1f, .1f),
                TintColor = AppGlobal.Colors.GreenColor,
                KeyboardType = UIKeyboardType.Default,
                Enabled = false,
                AttributedPlaceholder = new NSAttributedString ("Select type", UIFont.FromName (FontsName.RegularEn, 15f), UIColor.FromWhiteAlpha (1.0f, 0.5f))
            };
            jobTxt.Layer.CornerRadius = jobTxt.Frame.Height / 2;

            //set Job Btn
            jobBtn = new UIButton (jobTxt.Frame);


            //Next Page
            nextButton = new UIButton (new CGRect (View.Frame.Width / 2 - 60, jobTxt.Frame.Bottom + 50, 120, 35)) {
                BackgroundColor = AppGlobal.Colors.GreenColor
            };
            nextButton.Layer.CornerRadius = nextButton.Frame.Height / 2;
            nextButton.SetImage (UIImage.FromFile ("Right.png"), UIControlState.Normal);
            #endregion


            //Buttons Event
            militaryBtn.TouchUpInside += MilitaryBtn_TouchUpInside;
            jobBtn.TouchUpInside += JobBtn_TouchUpInside;
            nextButton.TouchUpInside += NextButton_TouchUpInside;

            //Adding Objects to View
            View.AddSubviews (militaryLabel, militaryTxt, militaryBtn, jobLabel, jobTxt, jobBtn, nextButton);
        }


        /// <summary>
        /// Militaries the button touch up inside.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void MilitaryBtn_TouchUpInside (object sender, EventArgs e)
        {
            militaryTxt.Layer.BorderWidth = 0f;

            var dialog = new XamSimplePickerDialog (new List<String> () { "Done", "Exemption", "Going to", "Runaway" }) {
                Title = "Military Service",
                Message = "What is your status ?",
                BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
                CancelButtonText = "Cancel",
                ConstantUpdates = false,
            };

            dialog.OnSelectedItemChanged += (object s, string ee) => {
                Console.WriteLine (ee);
                militaryTxt.Text = ee;
            };

            dialog.SelectedItem = "Done";

            dialog.Show ();

        }


        /// <summary>
        /// Jobs the button touch up inside.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void JobBtn_TouchUpInside (object sender, EventArgs e)
        {
            jobTxt.Layer.BorderWidth = 0f;

            var dialog = new XamSimplePickerDialog (new List<String> () { "Full Time", "Part Time", "Free Lance" }) {
                Title = "Contract Type",
                Message = "Which one do you prefer ?",
                BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
                CancelButtonText = "Cancel",
                ConstantUpdates = false,
            };

            dialog.OnSelectedItemChanged += (object s, string ee) => {
                Console.WriteLine (ee);
                jobTxt.Text = ee;
            };

            dialog.SelectedItem = "Full Time";

            dialog.Show ();

        }


        /// <summary>
        /// Nexts the button touch up inside.
        /// after checking some information next page would call.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void NextButton_TouchUpInside (object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (militaryTxt.Text)) {
                AppGlobal.Message (this, "", "Please select your military status.");
                militaryTxt.Layer.BorderWidth = 1f;
                militaryTxt.Layer.BorderColor = UIColor.Red.CGColor;
                return;
            }

            if (string.IsNullOrEmpty (jobTxt.Text)) {
                AppGlobal.Message (this, "", "Please select Contract type.");
                jobTxt.Layer.BorderWidth = 1f;
                jobTxt.Layer.BorderColor = UIColor.Red.CGColor;
            }

            var controller = this.Storyboard.InstantiateViewController ("VC_Third") as VC_Third;
            controller.Cellphone = CellPhone;
            controller.MilitaryStatus = militaryTxt.Text;
            controller.ContractType = jobTxt.Text;
            controller.Gender = Gender.Male;
            NavigationController.PushViewController (controller, true);

        }
    }
}
