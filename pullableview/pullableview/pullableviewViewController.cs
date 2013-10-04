using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace pullableview
{
    public class ViewController : UIViewController, PullableViewDelegate
    {
        private StyledPullableView pullDownView;
        private StyledPullableView pullUpView;
        private UILabel pullUpLabel;
        private PullableView pullRightView;

        public ViewController(string controller, bool t)
        {

        }

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            float xOffset = 0;
            if (!UserInterfaceIdiomIsPhone)
            {
                xOffset = 224;
            }

            pullRightView = new PullableView(new RectangleF(0, 200, 200, 300))
            {
                BackgroundColor = UIColor.LightGray,
                OpenedCenter = new PointF(100, 200),
                ClosedCenter = new PointF(-70, 200),
                Center = pullRightView.ClosedCenter,
                Animate = false,
            };
            pullRightView.HandleView.BackgroundColor = UIColor.DarkGray;
            pullRightView.HandleView.Frame = new RectangleF(170, 0, 30, 300);

            this.View.AddSubview(pullRightView);
            float rot = (float)(Math.PI / 2);
            UILabel label = new UILabel(new RectangleF(0, 0, 200, 30))
            {
                BackgroundColor = UIColor.DarkGray,
                TextColor = UIColor.White,
                Text = "Pull me to the right!",
                Transform = CGAffineTransform.MakeRotation(-rot),
                Center = new PointF(185, 150),
            };
            pullRightView.AddSubview(label);
            label = new UILabel(new RectangleF(4, 120, 200, 30))
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.White,
                Text = "I'm not animated"
            };
            pullRightView.AddSubview(label);
            pullUpView = new StyledPullableView(new RectangleF(xOffset, 0, 320, 460))
            {
                OpenedCenter = new PointF(160 + xOffset, this.View.Frame.Size.Height),
                ClosedCenter = new PointF(160 + xOffset, this.View.Frame.Size.Height + 200),
                Center = pullUpView.ClosedCenter,
                TheDelegate = this,
            };
            pullUpView.HandleView.Frame = new RectangleF(0, 0, 320, 40);
            this.View.AddSubview(pullUpView);
            pullUpLabel = new UILabel(new RectangleF(0, 4, 320, 20))
            {
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.LightGray,
                Text = "Pull me up!",
            };
            pullUpView.AddSubview(pullUpLabel);
            label = new UILabel(new RectangleF(0, 80, 320, 64))
            {
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.White,
                ShadowColor = UIColor.Black,
                ShadowOffset = new SizeF(1, 1),
                Text = "I only go half-way up!",
            };
            pullUpView.AddSubview(label);
            pullDownView = new StyledPullableView(new RectangleF(xOffset, 0, 320, 460))
            {
                OpenedCenter = new PointF(160 + xOffset, 230),
                ClosedCenter = new PointF(160 + xOffset, -200),
                Center = pullDownView.ClosedCenter,
            };
            this.View.AddSubview(pullDownView);
            label = new UILabel(new RectangleF(0, 200, 320, 64))
            {
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.White,
                ShadowColor = UIColor.Black,
                ShadowOffset = new SizeF(1, 1),
                Text = "Look at this beautiful linen texture!",
            };
            pullDownView.AddSubview(label);
        }

        [Export("pullableViewDidChangeState")]
        public void PullableViewDidChangeState(PullableView pView, bool opened)
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

