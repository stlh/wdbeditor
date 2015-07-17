(function () {
    var stlh_todone = {
        init: function () {
            document.write('<div id="stlh_todone"></div>');
            var widget = $('#stlh_todone');
            if (widget.length == 0) {
                alert('no conetnt fond');
            }
            widget.css('background-color', '#336699').css('height', '20em');
            var content = document.createElement('div');
            content.id = 'stlh_todone';
        },
    };
    stlh_todone.init();
})();
