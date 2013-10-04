using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace pullableview
{
    public interface PullableViewDelegate : object
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
        protected CGPoint closedCenter;
        protected CGPoint openedCenter;
        protected UIView handleView;
        protected UIPanGestureRecognizer dragRecognizer;
        protected UITapGestureRecognizer tapRecognizer;
        protected CGPoint startPos;
        protected CGPoint minPos;
        protected CGPoint maxPos;
        protected bool opened;
        protected bool verticalAxis;
        protected bool toggleOnTap;
        protected bool animate;
        protected float animationDuration;
        protected PullableViewDelegate theDelegate;

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

        public CGPoint ClosedCenter
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

        public CGPoint OpenedCenter
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

        PullableView(CGRect frame)
            : base (frame)
        {
            animate = true;
            animationDuration = 0.2;
            toggleOnTap = true;
            // Creates the handle view. Subclasses should resize, reposition and style this view
            handleView = new UIView(CGRectMake(0, frame.Size.Height - 40, frame.Size.Width, 40));
            this.AddSubview(handleView);
            dragRecognizer = new UIPanGestureRecognizer(this, @selector (handleDrag:));
            dragRecognizer.MinimumNumberOfTouches = 1;
            dragRecognizer.MaximumNumberOfTouches = 1;
            handleView.AddGestureRecognizer(dragRecognizer);
            tapRecognizer = new UITapGestureRecognizer(this, @selector (handleTap:));
            tapRecognizer.NumberOfTapsRequired = 1;
            tapRecognizer.NumberOfTouchesRequired = 1;
            handleView.AddGestureRecognizer(tapRecognizer);
            opened = false;
        }

        void HandleDrag(UIPanGestureRecognizer sender)
        {
            if (sender.State() == UIGestureRecognizerState.Began)
            {
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

            }
            else if (sender.State() == UIGestureRecognizerState.Changed)
            {
                CGPoint translate = sender.TranslationInView(this.Superview);
                CGPoint newPos;
                // Moves the view, keeping it constrained between openedCenter and closedCenter
                if (verticalAxis)
                {
                    newPos = CGPointMake(startPos.X, startPos.Y + translate.Y);
                    if (newPos.Y < minPos.Y)
                    {
                        newPos.Y = minPos.Y;
                        translate = CGPointMake(0, newPos.Y - startPos.Y);
                    }

                    if (newPos.Y > maxPos.Y)
                    {
                        newPos.Y = maxPos.Y;
                        translate = CGPointMake(0, newPos.Y - startPos.Y);
                    }

                }
                else
                {
                    newPos = CGPointMake(startPos.X + translate.X, startPos.Y);
                    if (newPos.X < minPos.X)
                    {
                        newPos.X = minPos.X;
                        translate = CGPointMake(newPos.X - startPos.X, 0);
                    }

                    if (newPos.X > maxPos.X)
                    {
                        newPos.X = maxPos.X;
                        translate = CGPointMake(newPos.X - startPos.X, 0);
                    }

                }

                sender.SetTranslationInView(translate, this.Superview);
                this.Center = newPos;
            }
            else if (sender.State() == UIGestureRecognizerStateEnded)
            {
                // Gets the velocity of the gesture in the axis, so it can be
                // determined to which endpoint the state should be set.
                CGPoint vectorVelocity = sender.VelocityInView(this.Superview);
                CGFloat axisVelocity = verticalAxis ? vectorVelocity.Y : vectorVelocity.X;
                CGPoint target = axisVelocity < 0 ? minPos : maxPos;
                bool op = CGPointEqualToPoint(target, openedCenter);
                this.SetOpenedAnimated(op, animate);
            }

        }

        void HandleTap(UITapGestureRecognizer sender)
        {
            if (sender.State() == UIGestureRecognizerState.Ended)
            {
                this.SetOpenedAnimated(!opened, animate);
            }

        }

        public void SetOpenedAnimated(bool op, bool anim)
        {
            opened = op;
            if (anim)
            {
                UIView.BeginAnimationsContext(null, null);
                UIView.SetAnimationDuration(animationDuration);
                UIView.SetAnimationCurve(UIViewAnimationCurveEaseOut);
                UIView.SetAnimationDelegate(this);
                UIView.SetAnimationDidStopSelector(@selector (animationDidStop:finished:context:));
            }

            this.Center = opened ? openedCenter : closedCenter;
            if (anim)
            {
                // For the duration of the animation, no further interaction with the view is permitted
                dragRecognizer.Enabled = false;
                tapRecognizer.Enabled = false;
                UIView.CommitAnimations();
            }

            /*else {
            
               
            
               if ([delegate respondsToSelector:@selector(pullableView:didChangeState:)]) {
            
               [delegate pullableView:self didChangeState:opened];
            
               }
            
               }*/
        }

        void AnimationDidStopFinishedContext(string animationID, NSNumber finished, ref void context)
        {
            if (finished)
            {
                // Restores interaction after the animation is over
                dragRecognizer.Enabled = true;
                tapRecognizer.Enabled = toggleOnTap;
                /*if ([delegate respondsToSelector:@selector(pullableView:didChangeState:)]) {
                
                   [delegate pullableView:self didChangeState:opened];
                
                   }*/
            }

        }

    }
}
