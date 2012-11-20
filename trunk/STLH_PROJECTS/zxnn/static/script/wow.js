$(document).ready(function () {
    $('#ddlRegion').change(function () {
        var self = $(this);
        $('#txtRealm').val(self.val());
        var region = self.val();
        if(region != '')
        {
            var url = '//' + region + '/api/wow/realm/status';
        }
        
        $.getJSON(url, function(data) {
        var items = [];

        $.each(data.realms, function(name, slug) {
            items.push('<li id="' + name + '">' + slug + '</li>');
        });

        $('<ul/>', {
        'class': 'my-new-list',
        html: items.join('')
        }).appendTo('body');
    });
    })
})