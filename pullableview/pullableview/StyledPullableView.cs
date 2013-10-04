using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace pullableview
{
    public class StyledPullableView : PullableView
    {
        //
        // Prefix header for all source files of the 'PullableView' target in the 'PullableView' project
        /**
        
           Subclass of PullableView that uses a background image
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */

        StyledPullableView(CGRect frame)
        {
            if ((this = base.initWithFrame(frame)))
            {
                UIImageView imgView = new UIImageView(UIImage.ImageNamed("background.png"));
                imgView.Frame = CGRectMake(0, 0, 320, 460);
                this.AddSubview(imgView);
            }

            return this;
        }
    }
}
