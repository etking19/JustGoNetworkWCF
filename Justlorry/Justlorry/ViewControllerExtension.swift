//
//  ViewControllerExtension.swift
//  Justlorry
//
//  Created by GlWoon on 09/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import UIKit

extension UIViewController {
    
    func addViewController(_ viewController: UIViewController, toView: UIView) {
        
        addChildViewController(viewController);
        viewController.didMove(toParentViewController: self)
        
        viewController.view.translatesAutoresizingMaskIntoConstraints = false
        
        toView.addSubview(viewController.view)
        
        toView.addConstraints(NSLayoutConstraint.constraints(withVisualFormat: "H:|-0-[view]-0-|", options: NSLayoutFormatOptions(rawValue: 0), metrics: nil, views: ["view":viewController.view]))
        toView.addConstraints(NSLayoutConstraint.constraints(withVisualFormat: "V:|-0-[view]-0-|", options: NSLayoutFormatOptions(rawValue: 0), metrics: nil, views: ["view":viewController.view]))
        
        toView.layoutIfNeeded()
        toView.updateConstraints()
        viewController.view.layoutIfNeeded()
        viewController.view.updateConstraints()
    }
    
    func removeViewController(_ viewController: UIViewController) {
        viewController.willMove(toParentViewController: nil)
        viewController.view.removeFromSuperview()
        viewController.removeFromParentViewController()
    }
    
}
