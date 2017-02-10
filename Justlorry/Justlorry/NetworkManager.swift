//
//  NetworkManager.swift
//  Justlorry
//
//  Created by GlWoon on 04/10/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import Foundation


class NetworkManager{
    static let sharedInstance = NetworkManager()

    private init(){
        
    }

    #if LORRY
    let appId = "6d0d8142-7b77-42d8-9a62-5ddf8afc61f2"
    let url = "https://m.justlorry.com/?identifier="
    #endif
    
    #if PARTNER
    let appId = "27513627-862c-4d0b-b032-fb1165577fcd"
    let url = "https://partner.justlorry.com/?identifier="
    #endif
    
    
    
    func setUrl(data: [AnyHashable: AnyObject]){
        if let category = data["category"] {
            print(category)
            //TODO: justLorryUrl = category
        }
    }
    
}
