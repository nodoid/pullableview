using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace pullableview
{
    public class StyledPullableView : PullableView
    {
        public StyledPullableView(RectangleF rect)
        {
            //if (this = base.InitWithFrame(frame))
            //{
            UIImageView imgView = new UIImageView(UIImage.FromFile("Graphics/background.png"));
            imgView.Frame = new RectangleF(0, 0, 320, 460);
            this.AddSubview(imgView);
            //}

            //return this;
        }
    }
}
