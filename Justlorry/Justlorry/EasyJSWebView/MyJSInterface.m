//
//  MyJSInterface.m
//  Justlorry
//
//  Created by GlWoon on 22/11/2016.
//  Copyright © 2016 Justlorry. All rights reserved.
//

#import "MyJSInterface.h"
#import "partner-Swift.h"

@implementation MyJSInterface

- (void) StartGpsTracking: (NSString*) userId{
    JSEventManager *manager = [JSEventManager new];
    [manager startLocationTrackingWithUserId:userId];
}

@end
