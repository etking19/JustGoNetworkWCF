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
    var notificationFromForeground = false
    var notificationFromBackground = false

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
        OneSignal.setLogLevel(.LL_VERBOSE, visualLevel: .LL_NONE)

        OneSignal.registerForPushNotifications()
        //TODO: depend on bundle id
        OneSignal.initWithLaunchOptions(launchOptions, appId:  NetworkManager.sharedInstance.appId, handleNotificationReceived: { (notification) in
                self.notificationFromForeground = true
                let result = notification?.payload
                self.notificationMessage = (result?.title)!
                let body : String! = result!.body
                self.notificationMessage =  self.notificationMessage + "\n" + body
                print(self.notificationMessage)
            }, handleNotificationAction: { (result) in
                if self.notificationFromForeground {
                    self.notificationFromBackground = false
                } else {
                    self.notificationFromBackground = true
                }
                self.notificationFromForeground = false
                
                if let extraInfo = result?.notification.payload.additionalData{
                    NetworkManager.sharedInstance.setUrl(data: extraInfo as [AnyHashable : AnyObject])
                }
                
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

    }

    func applicationDidEnterBackground(_ application: UIApplication) {
    }

    func applicationWillEnterForeground(_ application: UIApplication) {

    }

    func applicationDidBecomeActive(_ application: UIApplication) {

    }

    func applicationWillTerminate(_ application: UIApplication) {
    }


}

