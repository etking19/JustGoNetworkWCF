//
//  MainVC.swift
//  Justlorry
//
//  Created by GlWoon on 04/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

class MainVC: UIViewController, UIWebViewDelegate{

    @IBOutlet weak var webView: UIWebView!
    @IBOutlet weak var loadingView: UIView!
    @IBOutlet weak var activityIndicator: UIActivityIndicatorView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.showLoading()
        webView.delegate = self
        let myUrl = URL (string:  NetworkManager.sharedInstance.justLorryUrl)
        let requestObj = URLRequest(url: myUrl!)
        self.webView.scalesPageToFit = true
        self.webView.loadRequest(requestObj)

    }
    
    override func viewDidAppear(_ animated: Bool) {
        //self.showLoading()
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    func webViewDidFinishLoad(_ webView: UIWebView) {
        self.hideLoading()
    }
    
    //MARK: - Loading Indicator
    func showLoading() {
        activityIndicator.startAnimating()
        UIView.animate(withDuration: 0.1, animations: { () -> Void in
            self.loadingView.alpha = 1.0
        })
    }
    
    func hideLoading() {
        activityIndicator.stopAnimating()
        UIView.animate(withDuration: 0.1, animations: { () -> Void in
            self.loadingView.alpha = 0.0
        })
    }
}
