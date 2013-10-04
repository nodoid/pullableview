#import <Availability.h>
#import <UIKit/UIKit.h>
using System;
#import <UIKit/UIKit.h>

namespace Default.Namespace
{
    public class AppDelegate : UIResponder, UIApplicationDelegate
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
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        /**
        
           @author Fabio Rodella fabio@crocodella.com.br
        
           */
        public UIWindow Window {get; set;}

        public ViewController ViewController {get; set;}

        bool ApplicationDidFinishLaunchingWithOptions(UIApplication application, NSDictionary launchOptions)
        {
            this.Window = new UIWindow(UIScreen.MainScreen().Bounds());
            // Override point for customization after application launch.
            if (UIDevice.CurrentDevice().UserInterfaceIdiom() == UIUserInterfaceIdiomPhone)
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

        void ApplicationWillResignActive(UIApplication application)
        {
            /*
            
            Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
            
               Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
            
               */
        }

        void ApplicationDidEnterBackground(UIApplication application)
        {
            /*
            
               Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
            
               If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
            
               */
        }

        void ApplicationWillEnterForeground(UIApplication application)
        {
            /*
            
               Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
            
               */
        }

        void ApplicationDidBecomeActive(UIApplication application)
        {
            /*
            
               Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
            
               */
        }

        void ApplicationWillTerminate(UIApplication application)
        {
            /*
            
               Called when the application is about to terminate.
            
               Save data if appropriate.
            
               See also applicationDidEnterBackground:.
            
               */
        }

    }
}
