using Foundation;
using System;
using UIKit;
using Communere.iOS.Classes;
using CoreGraphics;

namespace Communere.iOS
{
    public partial class VC_First : UIViewController
    {
        public VC_First (IntPtr handle) : base (handle)
        {
        }

        //Objects
        private UILabel genderLabel;
        private UIButton nextButton;
        private UIButton maleBtn, femaleBtn;
        private UITextField cellphoneTxt;
        private Gender gender = Gender.None;

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            //Set Bar Color to white
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

            //set some colors
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (29, 36, 65);

            //set Background Image
            UIGraphics.BeginImageContext (this.View.Frame.Size);
            var i = UIImage.FromFile ("Background.jpg");
            i = i.Scale (this.View.Frame.Size);
            this.View.BackgroundColor = UIColor.FromPatternImage (i);


            //add Title
            var label = new UILabel () {
                TextColor = AppGlobal.Colors.GreenColor,
                Text = "First",
                Font = UIFont.FromName (FontsName.RegularEn, 20f),
            };
            label.SizeToFit ();
            NavigationItem.TitleView = label;


            #region Objects
            //set genderLabel
            genderLabel = new UILabel (new CGRect (5, this.NavigationController.NavigationBar.Frame.Bottom + 20, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "What's your gender ?",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };

            //Gender Icons and Buttons
            maleBtn = new UIButton (new CGRect (View.Frame.GetMidX () - 75, genderLabel.Frame.Bottom + 20, 50, 50)) {
                BackgroundColor = UIColor.Clear,
            };
            maleBtn.SetImage (UIImage.FromFile ("Circled User Male Filled.png"), UIControlState.Normal);
            maleBtn.Layer.CornerRadius = maleBtn.Frame.Width / 2;


            femaleBtn = new UIButton (new CGRect (View.Frame.GetMidX () + 25, genderLabel.Frame.Bottom + 20, 50, 50)) {
                BackgroundColor = UIColor.Clear
            };
            femaleBtn.SetImage (UIImage.FromFile ("Circled User Female  Filled.png"), UIControlState.Normal);
            femaleBtn.Layer.CornerRadius = femaleBtn.Frame.Width / 2;


            //CellPhone TextField
            cellphoneTxt = new UITextField (new CGRect (View.Frame.Width / 4, femaleBtn.Frame.Bottom + (45), View.Frame.Width / 2, 30)) {
                ClearButtonMode = UITextFieldViewMode.Never,
                BorderStyle = UITextBorderStyle.None,
                ReturnKeyType = UIReturnKeyType.Next,
                AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None,
                Font = UIFont.FromName (FontsName.RegularEn, 16f),
                TextColor = AppGlobal.Colors.GreenColor,
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.FromWhiteAlpha (1f, .1f),
                TintColor = AppGlobal.Colors.GreenColor,
                KeyboardType = UIKeyboardType.PhonePad,
                AttributedPlaceholder = new NSAttributedString ("Cell Phone", UIFont.FromName (FontsName.RegularEn, 15f), UIColor.FromWhiteAlpha (1.0f, 0.5f))
            };
            cellphoneTxt.Layer.CornerRadius = cellphoneTxt.Frame.Height / 2;

            cellphoneTxt.ShouldBeginEditing += (textField) => {
                cellphoneTxt.Layer.BorderColor = AppGlobal.Colors.GreenColor.CGColor;
                cellphoneTxt.Layer.BorderWidth = 1f;
                return true;
            };


            //Next Page
            nextButton = new UIButton (new CGRect (View.Frame.Width / 2 - 60, cellphoneTxt.Frame.Bottom + 50, 120, 35)) {
                BackgroundColor = AppGlobal.Colors.GreenColor
            };
            nextButton.Layer.CornerRadius = nextButton.Frame.Height / 2;
            nextButton.SetImage (UIImage.FromFile ("Right.png"), UIControlState.Normal);

            #endregion


            //Add Tap Gesture
            var tap = new UITapGestureRecognizer (a => ShouldReturn ()) {
                CancelsTouchesInView = false,
            };
            View.AddGestureRecognizer (tap);

            //Add Objects to View
            View.AddSubviews (genderLabel, maleBtn, femaleBtn, cellphoneTxt, nextButton);


            //Buttons Event
            maleBtn.TouchUpInside += MaleBtn_TouchUpInside;
            femaleBtn.TouchUpInside += FemaleBtn_TouchUpInside;
            nextButton.TouchUpInside += NextButton_TouchUpInside;

        }


        /// <summary>
        /// tap on clean areas in View.
        /// </summary>
        /// <returns><c>true</c>, if return was shoulded, <c>false</c> otherwise.</returns>
        private bool ShouldReturn ()
        {
            cellphoneTxt.Layer.BorderWidth = 0f;
            cellphoneTxt.ResignFirstResponder ();
            return true;
        }


        /// <summary>
        /// Males the button touch up inside.
        /// Gender set to Male.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void MaleBtn_TouchUpInside (object sender, EventArgs e)
        {
            femaleBtn.BackgroundColor = UIColor.Clear;
            maleBtn.BackgroundColor = AppGlobal.Colors.GreenColor;
            gender = Gender.Male;
        }


        /// <summary>
        /// Females the button touch up inside.
        /// Gender set to Femal.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void FemaleBtn_TouchUpInside (object sender, EventArgs e)
        {
            maleBtn.BackgroundColor = UIColor.Clear;
            femaleBtn.BackgroundColor = AppGlobal.Colors.GreenColor;
            gender = Gender.Female;
        }


        /// <summary>
        /// Nexts the button touch up inside.
        /// Cell Phone simple Validity Check.
        /// Gender Selection Check.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void NextButton_TouchUpInside (object sender, EventArgs e)
        {
            bool err = false;
            err |= gender == Gender.None;

            if (err) {
                AppGlobal.Message (this, "", "Please Select your gender");
                return;
            }

            if (CellPhoneValidity ()) {
                AppGlobal.Message (this, "", "Cell Phone!?");
                return;
            }

            if (gender == Gender.Male) {
                var VC_Male = Storyboard.InstantiateViewController ("VC_SecondMale") as VC_SecondMale;
                VC_Male.CellPhone = cellphoneTxt.Text;
                NavigationController.PushViewController (VC_Male, true);
            }

            if (gender == Gender.Female) {
                var VC_Female = Storyboard.InstantiateViewController ("VC_SecondFemale") as VC_SecondFemale;
                VC_Female.CellPhone = cellphoneTxt.Text;
                NavigationController.PushViewController (VC_Female, true);
            }
        }



        /// <summary>
        /// Checking Number validity.
        /// </summary>
        /// <returns><c>true</c>, if phone validity was true, <c>false</c> otherwise.</returns>
        private bool CellPhoneValidity ()
        {
            bool err = false;

            if (string.IsNullOrEmpty (cellphoneTxt.Text) || cellphoneTxt.Text.Length < 11) {
                cellphoneTxt.Layer.BorderWidth = 1f;
                cellphoneTxt.Layer.BorderColor = UIColor.Red.CGColor;
                err = true;

            } else {
                switch (cellphoneTxt.Text.Length) {
                case 11:
                    if (cellphoneTxt.Text.Substring (0, 1) != "0") {

                        cellphoneTxt.Layer.BorderWidth = 1f;
                        cellphoneTxt.Layer.BorderColor = UIColor.Red.CGColor;
                        err = true;

                    }
                    break;
                case 13:
                    if (cellphoneTxt.Text.Substring (0, 1) != "+") {

                        cellphoneTxt.Layer.BorderWidth = 1f;
                        cellphoneTxt.Layer.BorderColor = UIColor.Red.CGColor;
                        err = true;

                    }
                    break;

                case 14:
                    if (cellphoneTxt.Text.Substring (0, 1) != "00") {

                        cellphoneTxt.Layer.BorderWidth = 1f;
                        cellphoneTxt.Layer.BorderColor = UIColor.Red.CGColor;
                        err = true;

                    }
                    break;

                default:

                    cellphoneTxt.Layer.BorderWidth = 1f;
                    cellphoneTxt.Layer.BorderColor = UIColor.Red.CGColor;
                    err = true;
                    break;
                }
            }

            return err;

        }
    }
}