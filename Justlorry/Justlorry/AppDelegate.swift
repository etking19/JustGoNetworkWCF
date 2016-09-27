//
//  AppDelegate.swift
//  Justlorry
//
//  Created by GlWoon on 04/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?
    var rootVC: RootVC! = RootVC(nibName:"RootVC",bundle:nil)
    var notificationMessage = ""
    var notificationFromBackground = false

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
        OneSignal.setLogLevel(.LL_VERBOSE, visualLevel: .LL_NONE)

        OneSignal.registerForPushNotifications()
        //TODO: depend on bundle id
        OneSignal.initWithLaunchOptions(launchOptions, appId: "6d0d8142-7b77-42d8-9a62-5ddf8afc61f2", handleNotificationReceived: { (notification) in
                self.notificationFromBackground = false
                let result = notification?.payload
                self.notificationMessage = (result?.title)!
                self.notificationMessage =  self.notificationMessage + "\n\(result?.body)"
                print(self.notificationMessage)
            }, handleNotificationAction: { (result) in
                self.notificationFromBackground = true                
            }, settings: [kOSSettingsKeyAutoPrompt : false, kOSSettingsKeyInAppAlerts : false])
        
        if #available(iOS 9.0, *){
           print("it is iOS 9.0 and above")
        } else {
            let array = ["UserAgent": "Mozilla/5.0 (iPod; U; CPU iPhone OS 4_3_3 like Mac OS X; ja-jp) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5"]
            UserDefaults.standard.register(defaults: array)
        }
       
        UIApplication.shared.setStatusBarHidden(false, with: UIStatusBarAnimation.fade)
        window = UIWindow(frame: UIScreen.main.bounds)
        window!.backgroundColor = UIColor.white
        window!.makeKeyAndVisible()
        window!.rootViewController = rootVC
        UIApplication.shared.statusBarStyle = UIStatusBarStyle.lightContent
        return true
    }
    
    func application(_ application: UIApplication, didReceiveRemoteNotification userInfo: [AnyHashable: Any]) {
        if let rootVC = self.rootVC , !notificationFromBackground {
            rootVC.showNotificationBanner(notificationMessage)
            notificationMessage = ""
        }
    }
    
    func application(_ application: UIApplication, didFailToRegisterForRemoteNotificationsWithError error: Error) {
        print("Failed to register:", error)
    }
    
    func applicationWillResignActive(_ application: UIApplication) {
        // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
        // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
    }

    func applicationDidEnterBackground(_ application: UIApplication) {
        // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
        // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    }

    func applicationWillEnterForeground(_ application: UIApplication) {
        // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
    }

    func applicationDidBecomeActive(_ application: UIApplication) {
        // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    }

    func applicationWillTerminate(_ application: UIApplication) {
        // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    }


}

