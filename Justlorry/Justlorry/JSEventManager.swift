//
//  JSEventManager.swift
//  Justlorry
//
//  Created by GlWoon on 28/11/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import Foundation
import CoreLocation

@objc class JSEventManager:NSObject, LocationManagerDelegate {
    
    @objc func startLocationTracking(userId:String) {
        LocationManager._sharedInstance.delegate = self
        RestApiManager.makeHTTPPutRequest(userId as String) {success in
            if success {
                DispatchQueue.main.async(){
                LocationManager._sharedInstance.startTrackLocation()
                   Timer.scheduledTimer(timeInterval: 30, target: self, selector:#selector(JSEventManager.handleLocation(timer:)), userInfo: nil, repeats: true)
                }
                
            }
        }
        
    }
    
    func handleLocation(timer: Timer) {
        LocationManager._sharedInstance.startTrackLocation()
    }
    
    func updateLocation(latitude:CLLocationDegrees, longitude:CLLocationDegrees){
        //call url
        print("latitude:\(latitude), longitude:\(longitude)")
        LocationManager._sharedInstance.stopTracking()
    }
}
