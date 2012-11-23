$(document).ready(function () {
    //load realm list on region change
    $('#ddlRegion').change(function () {
        var self = $(this);
        var region = self.val();
        if(region != '') {
            var url = '/wow/get_realms?region=' + region;
            $.ajax({
            	url: url,
            	dataType: 'json',
            	success:  function(data) {
	                var realms = data.realms;
	                var jddlRealm = $('#ddlRealm');
	                jddlRealm.empty();
	                $.each(realms, function() {
	                    jddlRealm.append($('<option></option>').attr('value', this.slug).text(this.name));
	                });
	            }
            });
        }
    });
    
    //load user data
    $('#btnLoad').click(function() {
        var region = $('#ddlRegion').val();
        var realm = $('#ddlRealm').val();
        var name = $('#txtCharacterName').val();
        if(region != '' && realm != '' && name != '') {
            ///api/wow/character/test-realm/Peratryn?fields=items
            var url = '/wow/get_character_items?region=' + region + '&realm=' + realm + '&character=' + name;
            $.getJSON(url, function(data) {
                if(data.status === undefined) {
                    var items = data.items;
                    var jitemsTable = $('#itemsTable');
                    jitemsTable.empty();
                    // jitemsTable.append('<caption>Items</caption>')
                    // jitemsTable.append('<tr>' + '<td>' + items.head.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.neck.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.shoulder.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.back.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.chest.name + '</td>' + '</tr>');
                    // //jitemsTable.append('<tr>' + '<td>' + items.shirt.name + '</td>' + '</tr>');
                    // //jitemsTable.append('<tr>' + '<td>' + items.tabard.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.wrist.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.hands.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.waist.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.legs.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.feet.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.finger1.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.finger2.name + '</td>' + '</tr>');
                    // jitemsTable.append('<tr>' + '<td>' + items.trinket1.name + '</td>' + '</tr>');
                    //jitemsTable.append('<tr>' + '<td>' + items.trinket2.name + '</td>' + '</tr>');
                    for(var key in items) {
                    	if(items[key].id !== undefined) {
                    		jitemsTable.append('<tr>' + '<td>' + items[key].name + '</td>' + '</tr>');
                		}
                    }
                }
                else {
                    alert(data2.reason);
                }
            });
        }
        else {
            alert('empty');
        }
    })
});