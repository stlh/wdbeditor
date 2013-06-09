var BonusStats = [];
BonusStats[1] = '+%s Health';
// '2' => '+%s Mana',
// '3' => '+%s Agility',
// '4' => '+%s Strength',
BonusStats[5] = '+%s Intellect';
BonusStats[6] = '+%s Spirit';
BonusStats[7] =  '+%s Stamina';
// '46' => 'Equip: Restores %s health per 5 sec.',
// '44' => 'Equip: Increases your armor penetration rating by %s.',
// '38' => 'Equip: Increases attack power by %s.',
// '15' => 'Equip: Increases your shield block rating by %s.',
// '48' => 'Equip: Increases the block value of your shield by %s.',
// '19' => 'Equip: Improves melee critical strike rating by %s.',
// '20' => 'Equip: Improves ranged critical strike rating by %s.',
BonusStats[32] =  'Equip: Increases your critical strike rating by %s.';
// '21' => 'Equip: Improves spell critical strike rating by %s.',
// '25' => 'Equip: Improves melee critical avoidance rating by %s.',
// '26' => 'Equip: Improves ranged critical avoidance rating by %s.',
// '34' => 'Equip: Improves critical avoidance rating by %s.',
// '27' => 'Equip: Improves spell critical avoidance rating by %s.',
// //ITEM_MOD_DAMAGE_PER_SECOND_SHORT => 'Damage Per Second',
// '12' => 'Equip: Increases defense rating by %s.',
// '13' => 'Equip: Increases your dodge rating by %s.',
// '37' => 'Equip: Increases your expertise rating by %s.',
// '40' => 'Equip: Increases attack power by %s in Cat, Bear, Dire Bear, and Moonkin forms only.',
// '28' => 'Equip: Improves melee haste rating by %s.',
// '29' => 'Equip: Improves ranged haste rating by %s.',
BonusStats[36] = 'Equip: Increases your haste rating by %s.';
// '30' => 'Equip: Improves spell haste rating by %s.',
// '16' => 'Equip: Improves melee hit rating by %s.',
// '17' => 'Equip: Improves ranged hit rating by %s.',
// '31' => 'Equip: Increases your hit rating by %s.',
// '18' => 'Equip: Improves spell hit rating by %s.',
// '22' => 'Equip: Improves melee hit avoidance rating by %s.',
// '23' => 'Equip: Improves ranged hit avoidance rating by %s.',
// '33' => 'Equip: Improves hit avoidance rating by %s.',
// '24' => 'Equip: Improves spell hit avoidance rating by %s.',
// '43' => 'Equip: Restores %s mana per 5 sec.',
BonusStats[49] = 'Equip: Increases your mastery rating by %s.';
// '14' => 'Equip: Increases your parry rating by %s.',
// '39' => 'Equip: Increases ranged attack power by %s.',
// '35' => 'Equip: Increases your resilience rating by %s.',
// '41' => 'Equip: Increases damage done by magical spells and effects by up to %s.',
// '42' => 'Equip: Increases healing done by magical spells and effects by up to %s.',
// '47' => 'Equip: Increases spell penetration by %s.',
BonusStats[45] =  'Equip: Increases spell power by %s.';

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
        var items;
        if(region != '' && realm != '' && name != '') {
            ///api/wow/character/test-realm/Peratryn?fields=items
            var url = '/wow/get_character_items?region=' + region + '&realm=' + realm + '&character=' + name;
            $.getJSON(url, function(data) {
                if(data.status === undefined) {
                    items = data.items;
                    var jitemsTable = $('#itemsTable');
                    jitemsTable.empty();
                    jitemsTable.append('<tr id="itemTh"></tr>')
                    for(var key in items) {
                        if(items[key].id !== undefined) {
                            jitemsTable.append('<tr id="item' + items[key].id + '"></tr>');
                	    }
                    }

                    var stats_list = [];

                    for(var key1 in items) {
                        if(items[key1].id !== undefined) {
                            var urlItem = '/wow/get_item?region=' + region + '&itemId=' + items[key1].id;
                            $.getJSON(urlItem, function(data) {
                                $('#item' + data.id).data('IndividualItem', data);
                                for(var i=0; i < data.bonusStats.length; ++i) {
                                    var bonusStat = data.bonusStats[i];
                                    stats_list[bonusStat.stat] = bonusStat.stat;
                                }
                            }).done(function() {
                                var tl = [];
                                for(var s in stats_list) {
                                    tl.push(s);
                                }

                                var tds = '';
                                for(var i=0; i < tl.length; ++i) {
                                    tds += '<td></td>';
                                }
                                for(var key2 in items) {
                                    if(items[key2].id !== undefined) {
                                        var tti = items[key2];
                                        var trItem = $('#item' + tti.id);
                                        trItem.empty();
                                        trItem.append('<td>' + tti.name + '</td>')
                                        if(trItem.data().IndividualItem !== undefined) {
                                            var tds2 = '';
                                            var individualItem = trItem.data().IndividualItem;
                                            for(var i=0; i < tl.length; ++i) {
                                                var hasAmount = false;
                                                for(var j=0; j < individualItem.bonusStats.length; ++j) {
                                                    var bonusStat = individualItem.bonusStats[j];
                                                    if(bonusStat.stat == tl[i]) {
                                                        tds2 += '<td>' + bonusStat.amount + '</td>';
                                                        hasAmount = true;
                                                    }
                                                }

                                                if(!hasAmount) {
                                                    tds2 += '<td></td>';
                                                }
                                            }
                                            trItem.append(tds2);
                                        } 
                                        else {
                                            trItem.append(tds);
                                        }
                                    }
                                }

                                var ths = '<th></th>';
                                var itemTh = $('#itemTh');
                                for(var i=0; i < tl.length; ++i) {
                                    ths += '<th>' + BonusStats[tl[i]] + '</th>';
                                }
                                itemTh.empty();
                                itemTh.append(ths);
                            });
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