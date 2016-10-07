using System;
using UIKit;

namespace Communere.iOS.Classes
{
    public class AppGlobal
    {
        public static class Colors
        {

            public static UIColor GreenColor => UIColor.FromRGB (59, 218, 188);
        }

        public static void Message (UIViewController vc, string title, string msg)
        {
            var alertcontroller = UIAlertController.Create (title, msg, UIAlertControllerStyle.Alert);
            alertcontroller.View.TintColor = UIColor.FromRGB (38, 150, 128);
            alertcontroller.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Cancel, null));
            vc.PresentViewController (alertcontroller, true, null);
        }
    }
}

