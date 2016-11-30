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
    
    //!!!"http://52.40.249.160/justpartner/public/?identifier=" not support call back
    #if PARTNER
    let appId = "6d0d8142-7b77-42d8-9a62-5ddf8afc61f2"
    let url = "http://52.40.249.160/mobile/?identifier="
    #endif
    
    #if DRIVER
    let appId = "b159196f-17b0-41e1-b096-aa8573b3b505"
    let url = "http://52.40.249.160/justpartner/public/?identifier="
    #endif
    
    
    
    func setUrl(data: [AnyHashable: AnyObject]){
        if let category = data["category"] {
            print(category)
            //TODO: justLorryUrl = category
        }
    }
    
}
