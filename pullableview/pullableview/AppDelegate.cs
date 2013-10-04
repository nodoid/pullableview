using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.SlideoutNavigation;
using MonoTouch.Dialog;

namespace pullableview
{
    public class AppDelegate : UIResponder, UIApplicationDelegate
    {
        //
        // Prefix header for all source files of the 'PullableView' target in the 'PullableView' project
        //
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        public UIWindow Window { get; set; }

        public ViewController ViewController { get; set; }

        public override bool ApplicationDidFinishLaunchingWithOptions(UIApplication application, NSDictionary launchOptions)
        {
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
            // Override point for customization after application launch.
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                this.ViewController = new ViewController("ViewController_iPhone", null);
            }
            else
            {
                this.ViewController = new ViewController("ViewController_iPad", null);
            }

            this.Window.RootViewController = this.ViewController;
            this.Window.MakeKeyAndVisible();
            return true;
        }

        public override void ApplicationWillResignActive(UIApplication application)
        {
            /*
            
            Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
            
               Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
            
               */
        }

        public override void ApplicationDidEnterBackground(UIApplication application)
        {
            /*
            
               Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
            
               If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
            
               */
        }

        public override void ApplicationWillEnterForeground(UIApplication application)
        {
            /*
            
               Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
            
               */
        }

        public override void ApplicationDidBecomeActive(UIApplication application)
        {
            /*
            
               Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
            
               */
        }

        public override void ApplicationWillTerminate(UIApplication application)
        {
            /*
            
               Called when the application is about to terminate.
            
               Save data if appropriate.
            
               See also applicationDidEnterBackground:.
            
               */
        }
    }
}
