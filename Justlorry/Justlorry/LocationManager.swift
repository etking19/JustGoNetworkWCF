//
//  LocationManager.swift
//  Justlorry
//
//  Created by GlWoon on 27/09/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import CoreLocation

protocol LocationManagerDelegate {
    func updateLocation(latitude:CLLocationDegrees, longitude:CLLocationDegrees)
}

class LocationManager:NSObject, CLLocationManagerDelegate  {
    private var locationManager: CLLocationManager!
    var delegate:LocationManagerDelegate?
    static let _sharedInstance = LocationManager()
    
    func startTrackLocation() {
        locationManager = CLLocationManager()
        locationManager.distanceFilter = 10
        locationManager.desiredAccuracy = kCLLocationAccuracyBest
        locationManager.delegate = self
        
        let status = CLLocationManager.authorizationStatus()
        if status == .notDetermined || status == .denied || status == .authorizedWhenInUse {
            if #available(iOS 9.0, *) {
                locationManager.allowsBackgroundLocationUpdates = true
            } else {
                locationManager.requestAlwaysAuthorization()
            }
            locationManager.requestWhenInUseAuthorization()
        }
            
        locationManager.startUpdatingLocation()
    }
    
    func stopTracking() {
        if let locationManager = self.locationManager{
            locationManager.stopUpdatingLocation()
            self.locationManager = nil;
        }
    }
    
    //MARK: -CLLocationManagerDelegate
    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        if let delegate = self.delegate{
            delegate.updateLocation(latitude: locations[0].coordinate.latitude, longitude: locations[0].coordinate.longitude)
        }
    }
    
}
