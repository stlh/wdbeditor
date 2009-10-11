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
	IBOutlet NSTextField *itemClassField;
	IBOutlet NSTextField *itemSubClassField;
	IBOutlet NSTextField *itemNameField;
	IBOutlet NSTextField *itemModelIdField;
	IBOutlet NSTableView *tableView;
	IBOutlet NSMenuItem *miSave;
	IBOutlet NSSearchField *itemNameSearchField;
	
	IBOutlet NSButton *btnChange;
	
	NSURL *fileURL;
	NSMutableArray *items;
	NSMutableArray *showItems;
	NSMutableArray *changedItems;
	
	NSMutableArray *itemClasss;
	NSMutableArray *qualitys;
	NSMutableArray *qualitysColor;
}

- (IBAction)openFile: (id)sender;
- (IBAction)saveFile: (id)sender;
- (IBAction)changeItemModelId: (id)sender;
- (IBAction)filterItems: (id)sender;

- (void)setControlState: (BOOL)isEnable;

@end
