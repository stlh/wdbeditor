//
//  EditorController.h
//  WdbEditor
//
//  Created by Jiang YuJun on 09-9-5.
//  Copyright 2009 __MyCompanyName__. All rights reserved.
//

#import <Cocoa/Cocoa.h>

@interface EditorController : NSObject {
	IBOutlet NSTextField *itemIdField;
	IBOutlet NSTextField *itemNameField;
	IBOutlet NSTextField *itemModelIdField;
	IBOutlet NSTextField *filterField;
	IBOutlet NSTableView *tableView;
	IBOutlet NSMenuItem *miSave;
	
	IBOutlet NSButton *btnFilter;
	IBOutlet NSButton *btnChange;
	
	NSURL *fileURL;
	NSMutableArray *items;
	NSMutableArray *showItems;
	NSMutableArray *changedItems;
}

- (IBAction)openFile: (id)sender;
- (IBAction)saveFile: (id)sender;
- (IBAction)changeItemModelId: (id)sender;
- (IBAction)filterItems: (id)sender;

- (void)setControlState: (BOOL)isEnable;

@end
