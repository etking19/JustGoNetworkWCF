//
//  MainVC.swift
//  Justlorry
//
//  Created by GlWoon on 04/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

class MainVC: UIViewController {

    @IBOutlet weak var splashView: UIView!
    @IBOutlet weak var webView: EasyJSWebView!

    override func viewDidLoad() {
        super.viewDidLoad()
        
        OneSignal.idsAvailable({ (userId, pushToken) in
            print("UserId:%@", userId! as String)
            if (pushToken != nil) {
                print("pushToken:%@", pushToken! as String)
            }
            
            if UserDefaults.standard.string(forKey: UserId) == nil{
                UserDefaults.standard.set(userId, forKey: UserId)
            }
            
            DispatchQueue.main.async {
                PushManager.sharedInstance.subscribePush()
                self.splashView.isHidden = false
                let url = URL (string:  NetworkManager.sharedInstance.url + userId!)
                let requestObj = URLRequest(url: url!)
                self.webView.scalesPageToFit = true
                #if !LORRY
                    self.webView.addJavascriptInterfaces(MyJSInterface(), withName: "JSCall")
                #endif
                self.webView.loadRequest(requestObj)
                
                Timer.scheduledTimer(
                    timeInterval: 10, target: self, selector: #selector(MainVC.hideSplashView), userInfo: nil, repeats: false
                )
            }
           
        })
    }
    
    override func viewDidAppear(_ animated: Bool) {
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    func hideSplashView(){
        splashView.isHidden = true
    }
}
