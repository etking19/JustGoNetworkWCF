//
//  RestApiManager.swift
//  Justlorry
//
//  Created by GlWoon on 22/11/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

import Foundation


class RestApiManager: NSObject {
    
   class func makeHTTPPutRequest(_ userId:String, onCompletion:@escaping (Bool)->Void) {
        let body =  [String: AnyObject]()
        if let identifier = UserDefaults.standard.string(forKey: UserId){
            let path = "https://api.justlorry.com/api/device?userId=\(userId)&newIdentifier=\(identifier)"
            let request = NSMutableURLRequest(url: URL(string: path)!)
            request.httpMethod = "PUT"
            
            do {
                let session = URLSession.shared
                let data = try JSONSerialization.data(withJSONObject: body, options: JSONSerialization.WritingOptions())
                let task = session.uploadTask(with: request as URLRequest, from: data) { (data, response, error) in
                    if let httpResponse = response as? HTTPURLResponse {
                        let statusCode = httpResponse.statusCode
                        if statusCode == 200 {
                            //TODO: check the data
                            onCompletion(true)
                        }
                    }
                }
                task.resume()
                
            } catch {
                return
            }

        }
        
        
    }
}
