using Foundation;
using System;
using UIKit;
using Communere.iOS.Classes;
using CoreGraphics;
using System.Collections.Generic;

namespace Communere.iOS
{
    public partial class VC_Third : UIViewController
    {
        public VC_Third (IntPtr handle) : base (handle)
        {
        }

        //prop
        public string Cellphone {
            get;
            set;
        }

        public Gender Gender {
            get;
            set;
        }

        public string Birtday {
            get;
            set;
        }

        public string Degree {
            get;
            set;
        }

        public string MilitaryStatus {
            get;
            set;
        }

        public string ContractType {
            get;
            set;
        }


        //Objects
        private UIView backView;
        private UIImageView imgView;
        private UIButton callButton;
        private UILabel genderLabel, phoneLabel, detailsALabel, detailsBLabel,
        detailsAinfoLabel, detailsBinfoLabel;

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
                Text = "Third",
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
            //Gender
            genderLabel = new UILabel (new CGRect (5, this.NavigationController.NavigationBar.Frame.Bottom + 20, View.Frame.Width - 10, 30)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = "Gender is :",
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White
            };

            imgView = new UIImageView (new CGRect (View.Frame.GetMidX () - 25, genderLabel.Frame.Bottom + 10, 50, 50)) {
                ContentMode = UIViewContentMode.ScaleAspectFill
            };


            //A background Layer for informations
            backView = new UIView (new CGRect (10, imgView.Frame.Bottom + 20, View.Frame.Width - 20, 200)) {
                BackgroundColor = UIColor.FromRGBA (30, 38, 69, 0.1f)
            };
            backView.Layer.CornerRadius = 5f;

            #region Info Objects
            phoneLabel = new UILabel (new CGRect (0, 10, View.Frame.Width, 20)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                Text = $"Phone Number : {Cellphone}",
                TextAlignment = UITextAlignment.Center,
                TextColor = AppGlobal.Colors.GreenColor
            };

            detailsALabel = new UILabel (new CGRect (5, phoneLabel.Frame.Bottom + 20, backView.Frame.Width, 20)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.White
            };

            detailsAinfoLabel = new UILabel (new CGRect (0, detailsALabel.Frame.Bottom + 10, View.Frame.Width, 20)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                TextAlignment = UITextAlignment.Center,
                TextColor = AppGlobal.Colors.GreenColor
            };

            detailsBLabel = new UILabel (new CGRect (5, detailsAinfoLabel.Frame.Bottom + 20, backView.Frame.Width, 20)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.White
            };

            detailsBinfoLabel = new UILabel (new CGRect (0, detailsBLabel.Frame.Bottom + 10, View.Frame.Width, 20)) {
                Font = UIFont.FromName (FontsName.RegularEn, 18f),
                TextAlignment = UITextAlignment.Center,
                TextColor = AppGlobal.Colors.GreenColor
            };
            #endregion


            callButton = new UIButton (new CGRect (View.Frame.Width / 2 - 60, backView.Frame.Bottom + 20, 120, 35)) {
                BackgroundColor = AppGlobal.Colors.GreenColor
            };
            callButton.Layer.CornerRadius = callButton.Frame.Height / 2;
            callButton.SetTitle ("Call", UIControlState.Normal);
            #endregion


            //add objects to info backview
            backView.AddSubviews (phoneLabel, detailsBLabel, detailsALabel, detailsAinfoLabel, detailsBinfoLabel);

            //add objects to main view
            View.AddSubviews (genderLabel, imgView, backView, callButton);


            //Button Event
            callButton.TouchUpInside += CallButton_TouchUpInside;

        }


        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);

            //Setting data
            imgView.Image = (this.Gender == Gender.Male) ? UIImage.FromFile ("Circled User Male Filled.png") : UIImage.FromFile ("Circled User Female  Filled.png");

            detailsALabel.Text = (this.Gender == Gender.Male) ? "Military service status :" : "Degree :";
            detailsAinfoLabel.Text = (this.Gender == Gender.Male) ? MilitaryStatus : Degree;
            detailsBLabel.Text = (this.Gender == Gender.Male) ? "Contract Type :" : "Birthday :";
            detailsBinfoLabel.Text = (this.Gender == Gender.Male) ? ContractType : Birtday;
        }

        /// <summary>
        /// Calls the button touch up inside.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void CallButton_TouchUpInside (object sender, EventArgs e)
        {

            var alertcontroller = UIAlertController.Create ("", $"Call to {Cellphone} ?", UIAlertControllerStyle.Alert);
            alertcontroller.View.TintColor = UIColor.FromRGB (38, 150, 128);
            alertcontroller.AddAction (UIAlertAction.Create ("Yes", UIAlertActionStyle.Default, a => {
                var url = new NSUrl ("tel:" + Cellphone);
                UIApplication.SharedApplication.OpenUrl (url);
                if (!UIApplication.SharedApplication.OpenUrl (url)) {
                    AppGlobal.Message (this, "Not supported", "Scheme 'tel:' is not supported on simulator");
                };
            }));
            alertcontroller.AddAction (UIAlertAction.Create ("No", UIAlertActionStyle.Cancel, null));
            PresentViewController (alertcontroller, true, null);



        }
    }
}