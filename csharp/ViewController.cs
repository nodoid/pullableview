#import <Availability.h>
#import <UIKit/UIKit.h>
using System;
#import <UIKit/UIKit.h>

namespace Default.Namespace
{
    public class ViewController : UIViewController, PullableViewDelegate
    {
        protected StyledPullableView pullDownView;
        protected StyledPullableView pullUpView;
        protected UILabel pullUpLabel;
        protected PullableView pullRightView;
        //
        // Prefix header for all source files of the 'PullableView' target in the 'PullableView' project
        //

        #ifndef __IPHONE_4_0
            #warning "This project uses features only available in iOS SDK 4.0 and later."
        #endif

        #ifdef __OBJC__
        #endif

        /**
        
           Sample view controller
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        void ViewDidLoad()
        {
            base.ViewDidLoad();
            CGFloat xOffset = 0;
            if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
            {
                xOffset = 224;
            }

            pullRightView = new PullableView(CGRectMake(0, 200, 200, 300));
            pullRightView.BackgroundColor = UIColor.LightGrayColor();
            pullRightView.OpenedCenter = CGPointMake(100, 200);
            pullRightView.ClosedCenter = CGPointMake(-70, 200);
            pullRightView.Center = pullRightView.ClosedCenter;
            pullRightView.Animate = false;
            pullRightView.HandleView.BackgroundColor = UIColor.DarkGrayColor();
            pullRightView.HandleView.Frame = CGRectMake(170, 0, 30, 300);
            this.View.AddSubview(pullRightView);
            UILabel label = new UILabel(CGRectMake(0, 0, 200, 30));
            label.BackgroundColor = UIColor.DarkGrayColor();
            label.TextColor = UIColor.WhiteColor();
            label.Text = "Pull me to the right!";
            label.Transform = CGAffineTransformMakeRotation(-M_PI_2);
            label.Center = CGPointMake(185, 150);
            pullRightView.AddSubview(label);
            label = new UILabel(CGRectMake(4, 120, 200, 30));
            label.BackgroundColor = UIColor.ClearColor();
            label.TextColor = UIColor.WhiteColor();
            label.Text = "I'm not animated";
            pullRightView.AddSubview(label);
            pullUpView = new StyledPullableView(CGRectMake(xOffset, 0, 320, 460));
            pullUpView.OpenedCenter = CGPointMake(160 + xOffset, this.View.Frame.Size.Height);
            pullUpView.ClosedCenter = CGPointMake(160 + xOffset, this.View.Frame.Size.Height + 200);
            pullUpView.Center = pullUpView.ClosedCenter;
            pullUpView.HandleView.Frame = CGRectMake(0, 0, 320, 40);
            pullUpView.TheDelegate = this;
            this.View.AddSubview(pullUpView);
            pullUpLabel = new UILabel(CGRectMake(0, 4, 320, 20));
            pullUpLabel.TextAlignment = UITextAlignmentCenter;
            pullUpLabel.BackgroundColor = UIColor.ClearColor();
            pullUpLabel.TextColor = UIColor.LightGrayColor();
            pullUpLabel.Text = "Pull me up!";
            pullUpView.AddSubview(pullUpLabel);
            label = new UILabel(CGRectMake(0, 80, 320, 64));
            label.TextAlignment = UITextAlignmentCenter;
            label.BackgroundColor = UIColor.ClearColor();
            label.TextColor = UIColor.WhiteColor();
            label.ShadowColor = UIColor.BlackColor();
            label.ShadowOffset = CGSizeMake(1, 1);
            label.Text = "I only go half-way up!";
            pullUpView.AddSubview(label);
            pullDownView = new StyledPullableView(CGRectMake(xOffset, 0, 320, 460));
            pullDownView.OpenedCenter = CGPointMake(160 + xOffset, 230);
            pullDownView.ClosedCenter = CGPointMake(160 + xOffset, -200);
            pullDownView.Center = pullDownView.ClosedCenter;
            this.View.AddSubview(pullDownView);
            label = new UILabel(CGRectMake(0, 200, 320, 64));
            label.TextAlignment = UITextAlignmentCenter;
            label.BackgroundColor = UIColor.ClearColor();
            label.TextColor = UIColor.WhiteColor();
            label.ShadowColor = UIColor.BlackColor();
            label.ShadowOffset = CGSizeMake(1, 1);
            label.Text = "Look at this beautiful linen texture!";
            pullDownView.AddSubview(label);
        }

        bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation interfaceOrientation)
        {
            return UIInterfaceOrientationIsPortrait(interfaceOrientation);
        }

        void PullableViewDidChangeState(PullableView pView, bool opened)
        {
            if (opened)
            {
                pullUpLabel.Text = "Now I'm open!";
            }
            else
            {
                pullUpLabel.Text = "Now I'm closed, pull me up again!";
            }

        }

    }
}
