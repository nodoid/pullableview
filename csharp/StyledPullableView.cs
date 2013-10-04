#import <Availability.h>
#import <UIKit/UIKit.h>
using System;

namespace Default.Namespace
{
    public class StyledPullableView : PullableView
    {
        //
        // Prefix header for all source files of the 'PullableView' target in the 'PullableView' project
        //

        #ifndef __IPHONE_4_0
            #warning "This project uses features only available in iOS SDK 4.0 and later."
        #endif

        #ifdef __OBJC__
        #endif

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
