function OnFormLoad() {
    var newItemBox = $('txtNewItem');
    if (newItemBox != null) {
        Event.observe(newItemBox, 'blur', function (event) {
            if (newItemBox.value == '') {
                newItemBox.style.color = 'silver';
                newItemBox.value = 'Write something here';
            }
        });
        Event.observe(newItemBox, 'focus', function (event) {
            newItemBox.style.color = 'Black';
            newItemBox.value = '';
        });
    }
}

Event.observe(window, 'load', OnFormLoad);
