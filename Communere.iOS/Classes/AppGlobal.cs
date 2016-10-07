using System;
using UIKit;

namespace Communere.iOS.Classes
{
    /// <summary>
    /// App global.
    /// </summary>
    public static class AppGlobal
    {

        /// <summary>
        /// Colors.
        /// </summary>
        public static class Colors
        {

            public static UIColor GreenColor => UIColor.FromRGB (59, 218, 188);
        }

        /// <summary>
        /// Message the specified vc, title and msg.
        /// </summary>
        /// <param name="vc">Vc.</param>
        /// <param name="title">Title.</param>
        /// <param name="msg">Message.</param>
        public static void Message (UIViewController vc, string title, string msg)
        {
            var alertcontroller = UIAlertController.Create (title, msg, UIAlertControllerStyle.Alert);
            alertcontroller.View.TintColor = UIColor.FromRGB (38, 150, 128);
            alertcontroller.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Cancel, null));
            vc.PresentViewController (alertcontroller, true, null);
        }
    }
}

