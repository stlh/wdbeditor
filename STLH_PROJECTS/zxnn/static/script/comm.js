/* comm.js */
(function () {
    loader = new Image();
    loader.src = '/img/ajax-loader.gif';
})()
//mask div
function MaskDivClass() {
    this._MaskDivId = 'stlh_mask_div';
    return this;
}

MaskDivClass.prototype.show = function () {
    var obj = $(this._MaskDivId);
    if (obj == null) {
        obj = this.writeElementToPage();
    }
    obj.style.display = 'block';
};

MaskDivClass.prototype.hide = function () {
    var obj = $(this._MaskDivId);
    if (obj != null) {
        obj.style.display = 'none';
    }
};

MaskDivClass.prototype.writeElementToPage = function () {
    var maskBox = document.createElement('div');
    maskBox.id = this._MaskDivId;
    var body = $$('body')[0];
    with (maskBox.style) {
        position = 'absolute';
        top = '0px';
        right = '0px';
        filter = 'alpha(opacity=50);';
        opacity = 0.5;
        backgroundColor = '#303030';
        height = body.clientHeight + 'px';
        width = body.clientWidth + 'px';
        display = 'none';
        zIndex = '5000';
    }
    /*maskBox.innerHTML += '<iframe style=\"width:'
        + body.clientWidth + 'px;'
        + ' height: '
        + body.clientHeight
        + 'px;'
        + 'filter: alpha(opacity=1); opacity=0.01\"></iframe>';*/
    body.appendChild(maskBox);

    return maskBox;
};

var MaskDiv = new MaskDivClass();

function showMenu() {
    var menu = $('menu');
    if (menu.style.display == 'none') {
        menu.style.display = 'block';
    }
    else {
        menu.style.display = 'none';
    }
}
