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
                
                var splashTime = 10
                #if !LORRY
                    self.webView.addJavascriptInterfaces(MyJSInterface(), withName: "JSCall")
                    splashTime = 15
                #endif
                self.webView.loadRequest(requestObj)
                
                Timer.scheduledTimer(
                    timeInterval: TimeInterval(splashTime), target: self, selector: #selector(MainVC.hideSplashView), userInfo: nil, repeats: false
                )
            }
           
        })
    }
    
    override func viewWillAppear(_ animated: Bool) {
        #if LORRY
           // self.view.backgroundColor = UIColor(red: 79/255, green: 156/255, blue: 68/255, alpha: 1.0)
        #endif
        
        #if PARTNER
          //  self.view.backgroundColor = UIColor(red: 20.0/255.0, green: 153.0/255.0, blue: 72.0/255.0, alpha: 1.0)

        #endif
        

    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    func hideSplashView(){
        splashView.isHidden = true
    }
}
