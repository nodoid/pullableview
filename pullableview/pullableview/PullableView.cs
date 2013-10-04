using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;

namespace pullableview
{
    public interface PullableViewDelegate
    {
        /**
        
           Notifies of a changed state
        
           @param pView PullableView whose state was changed
        
           @param opened The new state of the view
        
           */
        void PullableViewDidChangeState(PullableView pView, bool opened);
    }

    public class PullableView : UIView
    {
        private PointF closedCenter;
        private PointF openedCenter;
        private PointF startPos;
        private PointF minPos;
        private PointF maxPos;
        private UIView handleView;
        private UIPanGestureRecognizer dragRecognizer;
        private UITapGestureRecognizer tapRecognizer;
        private bool opened;
        private bool verticalAxis;
        private bool toggleOnTap;
        private bool animate;
        private float animationDuration;
        private PullableViewDelegate theDelegate;

        /**
        
           Protocol for objects that wish to be notified when the state of a
        
           PullableView changes
        
           */
        /**
        
           Class that implements a view that can be pulled out by a handle, 
        
           similar to the Notification Center in iOS 5. This class supports
        
           pulling in the horizontal or vertical axis. This is determined by
        
           the values for openedCenter and closedCenter that you set: if
        
           both have the same x coordinate, the pulling should happen in the
        
           vertical axis, or the horizontal axis otherwise.
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           The view that is used as the handle for the PullableView. You
        
           can style it, add subviews or set its frame at will.
        
           */
        /**
        
           The point that defines the center of the view when in its closed
        
           state. You must set this before using the PullableView.
        
           */
        /**
        
           The point that defines the center of the view when in its opened
        
           state. You must set this before using the PullableView.
        
           */
        /**
        
           Gesture recognizer responsible for the dragging of the handle view.
        
           It is exposed as a property so you can change the number of touches
        
           or created dependencies to other recognizers in your views.
        
           */
        /**
        
           Gesture recognizer responsible for handling tapping of the handle view.
        
           It is exposed as a property so you can change the number of touches
        
           or created dependencies to other recognizers in your views.
        
           */
        /**
        
           If set to YES, tapping the handle view will toggle the PullableView.
        
           Default value is YES.
        
           */

        public PullableView()
        {
        }

        public bool ToggleOnTap
        {
            get
            {
                return toggleOnTap;
            }
            set
            {
                toggleOnTap = value;
                tapRecognizer.Enabled = value;
            }
        }

        /**
        
           If set to YES, the opening or closing of the PullableView will
        
           be animated. Default value is YES.
        
           */
        /**
        
           Duration of the opening/closing animation, if enabled. Default
        
           value is 0.2.
        
           */
        /**
        
           Delegate that will be notified when the PullableView changes state.
        
           If the view is set to animate transitions, the delegate will be
        
           called only when the animation finishes.
        
           */
        /**
        
           The current state of the `PullableView`.
        
           */
        /**
        
           Toggles the state of the PullableView
        
           @param op New state of the view
        
           @param anim Flag indicating if the transition should be animated
        
           */
        public UIView HandleView
        {
            get
            {
                return handleView;
            }
        }

        public PointF ClosedCenter
        {
            get
            {
                return closedCenter;
            }
            set
            {
                closedCenter = value;
            }
        }

        public PointF OpenedCenter
        {
            get
            {
                return openedCenter;
            }
            set
            {
                openedCenter = value;
            }
        }

        public UIPanGestureRecognizer DragRecognizer
        {
            get
            {
                return dragRecognizer;
            }
        }

        public UITapGestureRecognizer TapRecognizer
        {
            get
            {
                return tapRecognizer;
            }
        }

        public bool Animate
        {
            get
            {
                return animate;
            }
            set
            {
                animate = value;
            }
        }

        public float AnimationDuration
        {
            get
            {
                return animationDuration;
            }
            set
            {
                animationDuration = value;
            }
        }

        public PullableViewDelegate TheDelegate
        {
            get
            {
                return theDelegate;
            }
            set
            {
                theDelegate = value;
            }
        }

        public bool Opened
        {
            get
            {
                return opened;
            }
        }

        public PullableView(RectangleF frame) : base(frame)
        {
            animate = true;
            animationDuration = 0.2f;
            toggleOnTap = true;
            // Creates the handle view. Subclasses should resize, reposition and style this view
            handleView = new UIView(new RectangleF(0, frame.Size.Height - 40, frame.Size.Width, 40));
            this.AddSubview(handleView);
            dragRecognizer = new UIPanGestureRecognizer(this, new Selector("handleDrag"));
            dragRecognizer.MinimumNumberOfTouches = 1;
            dragRecognizer.MaximumNumberOfTouches = 1;
            handleView.AddGestureRecognizer(dragRecognizer);
            tapRecognizer = new UITapGestureRecognizer(this, new Selector("handleTap"));
            tapRecognizer.NumberOfTapsRequired = 1;
            tapRecognizer.NumberOfTouchesRequired = 1;
            handleView.AddGestureRecognizer(tapRecognizer);
            opened = false;
        }

        [Export("handleDrag")]
        void HandleDrag(UIPanGestureRecognizer sender)
        {
            switch (sender.State)
            {
                case UIGestureRecognizerState.Began:
                    startPos = this.Center;
                // Determines if the view can be pulled in the x or y axis
                    verticalAxis = closedCenter.X == openedCenter.X;
                // Finds the minimum and maximum points in the axis
                    if (verticalAxis)
                    {
                        minPos = closedCenter.Y < openedCenter.Y ? closedCenter : openedCenter;
                        maxPos = closedCenter.Y > openedCenter.Y ? closedCenter : openedCenter;
                    }
                    else
                    {
                        minPos = closedCenter.X < openedCenter.X ? closedCenter : openedCenter;
                        maxPos = closedCenter.X > openedCenter.X ? closedCenter : openedCenter;
                    }
                    break;
                case UIGestureRecognizerState.Changed:
                    PointF translate = sender.TranslationInView(this.Superview);
                    PointF newPos;
                // Moves the view, keeping it constrained between openedCenter and closedCenter
                    if (verticalAxis)
                    {
                        newPos = new PointF(startPos.X, startPos.Y + translate.Y);
                        if (newPos.Y < minPos.Y)
                        {
                            newPos.Y = minPos.Y;
                            translate = new PointF(0, newPos.Y - startPos.Y);
                        }

                        if (newPos.Y > maxPos.Y)
                        {
                            newPos.Y = maxPos.Y;
                            translate = new PointF(0, newPos.Y - startPos.Y);
                        }

                    }
                    else
                    {
                        newPos = new PointF(startPos.X + translate.X, startPos.Y);
                        if (newPos.X < minPos.X)
                        {
                            newPos.X = minPos.X;
                            translate = new PointF(newPos.X - startPos.X, 0);
                        }

                        if (newPos.X > maxPos.X)
                        {
                            newPos.X = maxPos.X;
                            translate = new PointF(newPos.X - startPos.X, 0);
                        }
                    }
                   
                    sender.SetTranslation(translate, this.Superview);
                    this.Center = newPos;
                    break;
                case UIGestureRecognizerState.Ended:
                    PointF vectorVelocity = sender.VelocityInView(this.Superview);
                    float axisVelocity = verticalAxis ? vectorVelocity.Y : vectorVelocity.X;
                    PointF target = axisVelocity < 0 ? minPos : maxPos;
                    bool op = target == openedCenter ? true : false;
                    this.SetOpenedAnimated(op, animate);
                    break;
            }

        }

        [Export("handleTap")]
        void HandleTap(UITapGestureRecognizer sender)
        {
            if (sender.State == UIGestureRecognizerState.Ended)
            {
                this.SetOpenedAnimated(!opened, animate);
            }

        }

        public void SetOpenedAnimated(bool op, bool anim)
        {
            opened = op;
            if (anim)
            {
                UIView.BeginAnimations(null, IntPtr.Zero);
                UIView.SetAnimationDuration(animationDuration);
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseOut);
                UIView.SetAnimationDelegate(this);
                UIView.SetAnimationDidStopSelector(new Selector("animationDidStop"));
            }

            this.Center = opened ? openedCenter : closedCenter;
            if (anim)
            {
                // For the duration of the animation, no further interaction with the view is permitted
                dragRecognizer.Enabled = false;
                tapRecognizer.Enabled = false;
                UIView.CommitAnimations();
            }
            else
            {
                if (this.RespondsToSelector(new Selector("pullableViewDidChangeState")))
                    theDelegate.PullableViewDidChangeState(this, true);

            }
            /*else {
            
               
            
               if ([delegate respondsToSelector:@selector(pullableView:didChangeState:)]) {
            
               [delegate pullableView:self didChangeState:opened];
            
               }
            
               }*/
        }

        [Export("animationDidStop")]
        void AnimationDidStopFinishedContext(string animationID, bool finished)
        {
            if (finished)
            {
                // Restores interaction after the animation is over
                dragRecognizer.Enabled = true;
                tapRecognizer.Enabled = toggleOnTap;
                if (this.RespondsToSelector(new Selector("pullableViewDidChangeState")))
                    theDelegate.PullableViewDidChangeState(this, false);
                /*if ([delegate respondsToSelector:@selector(pullableView:didChangeState:)]) {
                
                   [delegate pullableView:self didChangeState:opened];
                
                   }*/
            }

        }
    }
}
