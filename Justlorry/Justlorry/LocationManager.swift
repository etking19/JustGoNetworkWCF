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
    private var delegate:LocationManagerDelegate?

    init(delegate:LocationManagerDelegate){
        super.init()
        locationManager = CLLocationManager()
        locationManager.distanceFilter = 5
        locationManager.desiredAccuracy = kCLLocationAccuracyBest
        locationManager.delegate = self
        self.delegate = delegate
    }
    
    func startTrackLocation() {
        let status = CLLocationManager.authorizationStatus()
        if status == .notDetermined || status == .denied || status == .authorizedWhenInUse {
            locationManager.requestAlwaysAuthorization()
            locationManager.requestWhenInUseAuthorization()
        }

        locationManager.startUpdatingLocation()
    }
    
    func stopTracking() {
        if let locationManager = self.locationManager{
            locationManager.stopUpdatingLocation()
        }
    }
    
    //MARK: -CLLocationManagerDelegate
    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        if let delegate = self.delegate{
            delegate.updateLocation(latitude: locations[0].coordinate.latitude, longitude: locations[0].coordinate.longitude)
        }
    }
    
}
