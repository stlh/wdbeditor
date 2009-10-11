//
//  EditorController.m
//  WdbEditor
//
//  Created by Jiang YuJun on 09-9-5.
//  Copyright 2009 __MyCompanyName__. All rights reserved.
//

#import "EditorController.h"


@implementation EditorController

- (void)resetSelectedItemBox {
	[itemIdField setStringValue: @""];
	[itemClassField setStringValue: @""];
	[itemSubClassField setStringValue: @""];
	[itemNameField setStringValue: @""];
	[itemModelIdField setStringValue: @""];
	[itemModelIdField setEnabled: NO];
	[btnChange setEnabled: NO];
}

- (void)setControlState: (BOOL)isEnable {
	[itemNameSearchField setEnabled: isEnable];
	[itemModelIdField setEnabled: isEnable];
	[btnChange setEnabled: isEnable];
	[tableView setEnabled: isEnable];
}

- (IBAction)openFile:(id)sender {
	NSArray *fileTypes = [NSArray arrayWithObject: @"wdb"];
	NSOpenPanel *openPanel = [NSOpenPanel openPanel];
	[openPanel setAllowsMultipleSelection: NO];
	
	if ([openPanel runModalForTypes: fileTypes] == NSOKButton) {
		NSArray* files = [openPanel URLs];
		fileURL = [[files objectAtIndex: 0] copy];
		NSFileHandle* inFile = [NSFileHandle fileHandleForReadingFromURL: fileURL error: NULL];
		NSData *fileSignatureData = [inFile readDataOfLength: sizeof(char) * 4];
		NSString *fileSignature = [[NSString alloc] initWithData: fileSignatureData encoding: NSUTF8StringEncoding];
		if (![fileSignature isEqualToString: @"BDIW"])
		{
			NSRunAlertPanel(NSLocalizedString(@"ERROR", nil)
							, NSLocalizedString(@"FILE_FORMAT_ERROR", nil), @"OK", nil, nil);
			[inFile closeFile];
			return;
		}
		NSData *fileVersion = [inFile readDataOfLength: sizeof(int)];
		//NSLog(@"File Version: %@", fileVersion);
		NSData *fileLanguageData = [inFile readDataOfLength: sizeof(char) * 4];
		NSString *fileLanguage = [[NSString alloc] initWithData: fileLanguageData encoding:NSASCIIStringEncoding];
		NSLog(@"File Language: %@", fileLanguage);
		NSData *headerUnkonw = [inFile readDataOfLength: 12];//4 * 3 unknow
		//NSlog(@"Header unkonw: %@", headerUnkonw);
		int itemId;
		[[inFile readDataOfLength: sizeof(int)] getBytes: &itemId length: sizeof(int)];
		int itemRecordLength;
		[[inFile readDataOfLength: sizeof(int)] getBytes: &itemRecordLength length: sizeof(int)];
		
		int itemCounts = 1;
		while (itemId != 0) {
			int offset = [inFile offsetInFile];
			NSData *data = [inFile readDataOfLength: itemRecordLength];
			//NSData *temp1 = [inFile readDataOfLength: 12];
			//NSData *s = [inFile readDataOfLength: 1];
			NSRange r;
			r.location = 0;
			r.length = 4;
			int itemClass;
			[data getBytes: &itemClass range: r];
			int itemSubClass;
			r.location = 4;
			r.length = 4;
			[data getBytes: &itemSubClass range: r];
			int itemNameLength = 1;
			char c;
			r.location = 12;
			r.length = 1;
			[data getBytes: &c range: r];
			r.location++;
			while (c != '\0') {
				[data getBytes: &c range: r];
				r.location++;
				++itemNameLength;
			}
			//[inFile seekToFileOffset: [inFile offsetInFile] - itemNameLength];
			r.location = 12;
			r.length = itemNameLength;
			NSString *itemName = [[NSString alloc] initWithData: [data subdataWithRange: r] encoding: NSUTF8StringEncoding];
			//NSData *temp2 = [inFile readDataOfLength: 3];
			int itemModelId;
			r.location = 12 + itemNameLength + 3;
			r.length = 4;
			[data getBytes: &itemModelId range: r];
			//[[inFile readDataOfLength: sizeof(int)] getBytes: &itemModelId length: sizeof(int)];
			//NSData *temp3 = [inFile readDataOfLength: (itemRecordLength - 19 - itemNameLength)];
			
			NSLog(@"%d Item ID: %d; Item Class: %d", itemCounts++, itemId, itemClass);
			NSMutableDictionary* item = [NSMutableDictionary dictionary];
			[item setObject: [NSString stringWithFormat: @"%d", itemId] forKey: @"Item Id"];
			[item setObject: [itemClasss objectAtIndex: itemClass] forKey: @"Item Class"];
			[item setObject: [NSString stringWithFormat: @"%d", itemSubClass] forKey: @"Item SubClass"];
			[item setObject: itemName forKey: @"Item Name"];
			[item setObject: [NSString stringWithFormat: @"%d", itemModelId] forKey: @"Item Model Id"];
			[item setObject: [NSString stringWithFormat: @"%d", (offset + r.location)] forKey: @"offset"];
			[items addObject: item];
			[showItems addObject: item];
			
			[[inFile readDataOfLength: sizeof(int)] getBytes: &itemId length: sizeof(int)];
			[[inFile readDataOfLength: sizeof(int)] getBytes: &itemRecordLength length: sizeof(int)];
		}
		
		[inFile closeFile];
		
		[tableView reloadData];
		
		[self setControlState: YES];
	}
}

- (id)init {
	items = [[NSMutableArray alloc] init];
	showItems = [[NSMutableArray alloc] init];
	changedItems = [[NSMutableArray alloc] init];
	
	itemClasss = [[NSMutableArray alloc] initWithObjects: @"Consumable"
				  , @"Container"
				  , @"Weapon"
				  , @"3"
				  , @"Armor"
				  , @"Reagent"
				  , @"Projectile"
				  , @"Trade Goods"
				  , @"8"
				  , @"Recipe"
				  , @"10"
				  , @"Quiver"
				  , @"Quest Item"
				  , @"Key"
				  , @"Permanent"
				  , @"Miscellaneous"
				  , @"16"
				  , nil];
	
	qualitys = [[NSMutableArray alloc] initWithObjects: @"Poor"
			   , @"Common"
			   , @"Uncommon"
			   , @"Rare"
			   , @"Epic"
			   , @"Legendary"
			   , @"Artifact"
			   , nil];
	
	qualitysColor = [[NSMutableArray alloc] initWithObjects: @"Gray"
					, @"White"
					, @"Green"
					, @"Blue"
					, @"Purple"
					, @"Orange"
					, @"Red"
					, nil];
	
	return self;
}

- (void)awakeFromNib {
}

- (void)tableViewSelectionDidChange:(NSNotification *)aNotification
{
	int rowIndex = [tableView selectedRow];
	if (rowIndex != -1)
	{
		id item = [showItems objectAtIndex: rowIndex];
		[itemIdField setStringValue: [item objectForKey: @"Item Id"]];
		[itemClassField setStringValue: [item objectForKey: @"Item Class"]];
		[itemSubClassField setStringValue: [item objectForKey: @"Item SubClass"]];
		[itemNameField setStringValue: [item objectForKey: @"Item Name"]];
		[itemModelIdField setStringValue: [item objectForKey: @"Item Model Id"]];
		
		[itemModelIdField setEnabled: YES];
		[btnChange setEnabled: YES];
	}
	else
	{
		[self resetSelectedItemBox];
	}
}

- (IBAction)filterItems: (id)sender
{
	NSString* filterString = [itemNameSearchField stringValue];
	if ([filterString length] != 0)
	{		
		[showItems removeAllObjects];
		int count = [items count];
		int i;
		for (i=0; i < count; ++i)
		{
			NSMutableDictionary* item = [items objectAtIndex: i];
			NSRange range = [[item objectForKey: @"Item Name"] rangeOfString: filterString];
			if (range.location != NSNotFound)
			{
				[showItems addObject: item];
			}
		}
		
		[tableView reloadData];
		[tableView deselectAll: NULL];
		[self resetSelectedItemBox];
	}
	else
	{
		if ([items count] != [showItems count])
		{
			[showItems removeAllObjects];
			
			int i;
			int count = [items count];
			for (i = 0; i < count; ++i)
			{
				[showItems addObject: [items objectAtIndex: i]];
			}
			
			[tableView reloadData];
			[tableView deselectAll: NULL];
			[self resetSelectedItemBox];
		}
	}
}

- (IBAction)saveFile: (id)sender
{
	NSFileHandle *outFile = [NSFileHandle fileHandleForWritingToURL: fileURL error: nil]; 
	int i;
	int count = [changedItems count];
	for (i=0; i < count; ++i)
	{
		id item = [changedItems objectAtIndex: i];
		int offset = [[item objectForKey: @"offset"] intValue];
		int modelId = [[item objectForKey: @"Item Model Id"] intValue];
		NSData *modelIdData = [[NSData alloc] initWithBytes: &modelId length: 4];
		[outFile seekToFileOffset: offset];
		[outFile writeData: modelIdData];
		[modelIdData release];
	}
	[changedItems removeAllObjects];
	
	[outFile closeFile];
}

- (IBAction)changeItemModelId: (id)sender
{
	id item;
	item = [showItems objectAtIndex: [tableView selectedRow]];
	[item setObject: [itemModelIdField stringValue] forKey: @"Item Model Id"];
	if (![changedItems containsObject: item])
	{
		[changedItems addObject: item];
	}
	
	[miSave setEnabled: YES];
}

- (int)numberOfRowsInTableView: (NSTableView *)tableView
{
	return [showItems count];
}

- (id)tableView: (NSTableView *)tableView
	objectValueForTableColumn:(NSTableColumn *)tableColumn
	row:(int)rowIndex
{
	id item, value;
	item = [showItems objectAtIndex: rowIndex];
	value = [item objectForKey: [tableColumn identifier]];
	return value;
}

@end
