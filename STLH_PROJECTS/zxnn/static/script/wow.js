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
                    for(var key in items) {
                        if(items[key].id !== undefined) {
                            jitemsTable.append('<tr id="item' + items[key].id + '"></tr>');
                	    }
                    }

                    for(var key in items) {
                        if(items[key].id !== undefined) {
                            var item = items[key];
                            var urlItem = '/wow/get_item?region=' + region + '&itemId=' + items[key].id;
                            $.getJSON(urlItem, function(data) {
                                var trItem = $('#item' + data.id);
                                trItem.append('<td>' + data.name + '<td>')
                                var stats = [];
                                for(var i=0; i < data.bonusStats.length; i++) {
                                    var stat = data.bonusStats[i];
                                    stats[stat.stat] = stat;
                                }

                                console.log(stats);
                                for(var i=0; i < stats.length; i++) {
                                    var stat = stats[i];
                                    if(stat !== undefined) {
                                        trItem.append('<td>' + stat.stat + ': ' + stat.amount + '</td>');                                        
                                    }
                                }
                            })
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