using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace pullableview
{
    public class AppDelegate : UIApplicationDelegate
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

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);
            // Override point for customization after application launch.
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                this.ViewController = new ViewController("ViewController_iPhone", false);
            }
            else
            {
                this.ViewController = new ViewController("ViewController_iPad", false);
            }

            this.Window.RootViewController = this.ViewController;
            this.Window.MakeKeyAndVisible();
            return true;
        }

        public override void DidEnterBackground(UIApplication application)
        {
            /*
            
               Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
            
               If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
            
               */
        }

        public override void WillEnterForeground(UIApplication application)
        {
            /*
            
               Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
            
               */
        }

        public override void OnActivated(UIApplication application)
        {
            /*
            
               Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
            
               */
        }

        public override void WillTerminate(UIApplication application)
        {
            /*
            
               Called when the application is about to terminate.
            
               Save data if appropriate.
            
               See also applicationDidEnterBackground:.
            
               */
        }
    }
}
