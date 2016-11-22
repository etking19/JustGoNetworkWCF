//
//  PushManager.swift
//  Justlorry
//
//  Created by GlWoon on 22/11/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import Foundation
import UIKit

class PushManager {
    static let sharedInstance = PushManager()
    
    //MARK: - Init
    fileprivate init() {
    }
    
    
    func subscribePush() {
        //for first time launch the application
        if UserDefaults.standard.object(forKey: firstPushSubscribeUser) == nil{
            UserDefaults.standard.setValue(true, forKey: firstPushSubscribeUser)
        } else {
            self.checkNotificationRequest()
        }
        
    }
    
    func checkNotificationRequest(){
        let setting = UIApplication.shared.currentUserNotificationSettings?.types
        if setting?.rawValue == 0{
            self.showNotificationRequest()
        }
    }
    
    func showNotificationRequest() {
        // create the alert
        let alert = UIAlertController(title: NSLocalizedString("notification_title", comment: ""), message: NSLocalizedString("notification_message", comment: ""), preferredStyle: UIAlertControllerStyle.alert)
        
        // add the actions (buttons)
        alert.addAction(UIAlertAction(title: NSLocalizedString("notification_button_setting", comment: ""), style: UIAlertActionStyle.default, handler: { action in
            UIApplication.shared.openURL(URL(string:"app-settings:")!)
        }))
        alert.addAction(UIAlertAction(title: NSLocalizedString("notification_button_decline", comment: ""), style: UIAlertActionStyle.cancel, handler: nil))
        
        // show the alert
        let rootVC = (UIApplication.shared.delegate as! AppDelegate).rootVC
        rootVC?.present(alert, animated: true, completion: nil)
    }
}
