//
//  UIImageExtension.swift
//  Justlorry
//
//  Created by GlWoon on 07/02/2017.
//  Copyright © 2017 Justlorry. All rights reserved.
//

import Foundation
import UIKit


extension UIImageView {
    
    @IBInspectable var imageName: String {
        get {
            return image.debugDescription
        }
        set {
            image = ImageTypes.images[newValue]!
        }
    }
    
}
