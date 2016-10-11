//
//  SplashScreenVC.swift
//  Justlorry
//
//  Created by GlWoon on 05/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

class SplashScreenVC: UIViewController  {

    @IBOutlet weak var titleLabel: UILabel!

    override func viewDidLoad() {
        super.viewDidLoad()
        self.animation()
        self.titleLabel.text = splashTitle
        Timer.scheduledTimer(
            timeInterval: 2.5, target: self, selector: #selector(SplashScreenVC.showNextScreen), userInfo: nil, repeats: false
        )

    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    func animation(){
        UIView.animate(withDuration: 0.5, delay: 0.5, usingSpringWithDamping: 0.5, initialSpringVelocity: 0, options: .curveLinear, animations: {
            self.titleLabel.center.x = self.view.bounds.width - 100
            
            }, completion: nil)
        }
    
    func showNextScreen() {
        let mainVC = MainVC(nibName: "MainVC", bundle: nil)
        addViewController(mainVC, toView: self.view)
    }
}
