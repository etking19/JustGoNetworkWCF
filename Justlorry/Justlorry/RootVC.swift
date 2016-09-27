//
//  rootVC.swift
//  Justlorry
//
//  Created by GlWoon on 09/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

class RootVC: UIViewController {

    @IBOutlet weak var contentView: UIView!
    @IBOutlet weak var notificationBanner: UIView!
    @IBOutlet weak var notificationMsg: UILabel!
    
    override func viewDidAppear(_ animated: Bool) {
        let splashScreen = SplashScreenVC(nibName: "SplashScreenVC", bundle: nil)
        addViewController(splashScreen, toView: contentView)
    }
    
    func showNotificationBanner(_ message:String) {
        notificationMsg.text = message
        UIView.animate(withDuration: 0.5, delay: 0, options: [UIViewAnimationOptions.allowAnimatedContent], animations: { () -> Void in
            self.notificationBanner.alpha = 1.0
        }) { (flag) -> Void in
            DispatchQueue.main.asyncAfter(deadline: DispatchTime.now() + Double(Int64(4 * NSEC_PER_SEC)) / Double(NSEC_PER_SEC), execute: { () -> Void in
                UIView.animate(withDuration: 0.5, delay: 0, options: [UIViewAnimationOptions.allowAnimatedContent], animations: { () -> Void in
                    self.notificationBanner.alpha = 0.0
                    }, completion: { (flag) -> Void in
                        
                })
            })
        }
    }
}
