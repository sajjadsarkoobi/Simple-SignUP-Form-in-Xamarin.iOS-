using Foundation;
using System;
using UIKit;
using Communere.iOS.Classes;
using CoreGraphics;
using XamDialogs;

namespace Communere.iOS
{
    public partial class VC_SecondFemale : UIViewController
    {
        //prop
        public string CellPhone {
            get;
            set;
        }


        //Objects
        private UITextField degreeTxt, birthdayTxt;
        private UILabel degreeLabel, birthdayLabel;
        private UIButton birthdayBtn, nextButton;

        public VC_SecondFemale (IntPtr handle) : base (handle)
        {
        }

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
            //set Label
            degreeLabel = new UILabel (new CGRect (5, this.NavigationController.NavigationBar.Frame.Bottom + 20, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "What's your degree ?",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };


            //set DegreeTextField
            degreeTxt = new UITextField (new CGRect (View.Frame.Width / 4, degreeLabel.Frame.Bottom + (15), View.Frame.Width / 2, 30)) {
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
                //Text = "09373286067",
                AttributedPlaceholder = new NSAttributedString ("Your degree ?", UIFont.FromName (FontsName.RegularEn, 15f), UIColor.FromWhiteAlpha (1.0f, 0.5f))
            };
            degreeTxt.Layer.CornerRadius = degreeTxt.Frame.Height / 2;
            degreeTxt.ShouldReturn += (a => ShouldReturn ());
            degreeTxt.ShouldBeginEditing += (textField) => {
                degreeTxt.Layer.BorderColor = AppGlobal.Colors.GreenColor.CGColor;
                degreeTxt.Layer.BorderWidth = 1f;
                return true;
            };



            //Set Birthday Label
            birthdayLabel = new UILabel (new CGRect (5, degreeTxt.Frame.Bottom + 20, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "What's your Birthday ?",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };

            //set BirthTextField
            birthdayTxt = new UITextField (new CGRect (View.Frame.Width / 4, birthdayLabel.Frame.Bottom + (15), View.Frame.Width / 2, 30)) {
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
                AttributedPlaceholder = new NSAttributedString ("Select Date", UIFont.FromName (FontsName.RegularEn, 15f), UIColor.FromWhiteAlpha (1.0f, 0.5f))
            };
            birthdayTxt.Layer.CornerRadius = degreeTxt.Frame.Height / 2;


            //Birthday button
            birthdayBtn = new UIButton (birthdayTxt.Frame);


            //Next Page
            nextButton = new UIButton (new CGRect (View.Frame.Width / 2 - 60, birthdayBtn.Frame.Bottom + 50, 120, 35)) {
                BackgroundColor = AppGlobal.Colors.GreenColor
            };
            nextButton.Layer.CornerRadius = nextButton.Frame.Height / 2;
            nextButton.SetImage (UIImage.FromFile ("Right.png"), UIControlState.Normal);
            #endregion


            //Buttons Event
            birthdayBtn.TouchUpInside += BirthdayBtn_TouchUpInside;
            nextButton.TouchUpInside += NextButton_TouchUpInside;


            //Add Tap Gesture
            var tap = new UITapGestureRecognizer (a => ShouldReturn ()) {
                CancelsTouchesInView = false,
            };
            View.AddGestureRecognizer (tap);


            //Adding Objects to View
            View.AddSubviews (degreeLabel, degreeTxt, birthdayLabel, birthdayTxt, birthdayBtn, nextButton);

        }


        /// <summary>
        /// Birthday the button touch up inside.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void BirthdayBtn_TouchUpInside (object sender, EventArgs e)
        {
            var dialog = new XamDatePickerDialog (UIDatePickerMode.Date) {
                Title = "Date Picker",
                Message = "Please Pick a date",
                BlurEffectStyle = UIBlurEffectStyle.ExtraLight,
                CancelButtonText = "Cancel",
                ConstantUpdates = false,

            };

            dialog.SelectedDate = new DateTime (1987, 4, 23);

            dialog.ButtonMode = ButtonMode.OkAndCancel;

            dialog.ValidateSubmit = (DateTime data) => {
                return true;
            };

            dialog.OnSelectedDateChanged += (object s, DateTime ee) => {
                Console.WriteLine (ee.Date);
                birthdayTxt.Text = ee.Date.ToString ("yyyy MMMMM dd");
            };

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

            if (string.IsNullOrEmpty (degreeTxt.Text)) {
                degreeTxt.Layer.BorderWidth = 1f;
                degreeTxt.Layer.BorderColor = UIColor.Red.CGColor;
                AppGlobal.Message (this, "", "Please tell us your degree");
                return;
            };

            if (string.IsNullOrEmpty (birthdayTxt.Text)) {
                AppGlobal.Message (this, "", "Please select your Birthday");
                return;
            };

            var controller = this.Storyboard.InstantiateViewController ("VC_Third") as VC_Third;
            controller.Cellphone = CellPhone;
            controller.Degree = degreeTxt.Text;
            controller.Birtday = birthdayTxt.Text;
            controller.Gender = Gender.Female;
            NavigationController.PushViewController (controller, true);

        }

        /// <summary>
        /// tap on clean areas in View.
        /// </summary>
        /// <returns><c>true</c>, if return was shoulded, <c>false</c> otherwise.</returns>
        private bool ShouldReturn ()
        {
            degreeTxt.Layer.BorderWidth = 0f;
            degreeTxt.ResignFirstResponder ();
            return true;
        }
    }
}