function itemOnCheck(e, itemKey) {
    var checkItem = e.currentTarget;
    if (checkItem.checked) {
        $.get('/todone/mask_as_done/'
            , { item_key: itemKey }
            );
    }
    else {
        $.get('/todone/mask_as_todo/'
            , { item_key: itemKey }
            );
    }
}

function addItem(e) {
    var text = $('#txtNewItem').val();
    if (text != '') {
        $.get('/todone/add_item/'
            , { item_text: text }
            , function (data) {
                var ul = $('#todoneItems');
                ul.append('<li><input type="checkbox">' + text + '</li>');
                $('#txtNewItem').val('')[0].focus();
            }
            );
    }
    else {
        alert('can not be empty!');
    }
}

function showNewProjectBox(event) {
    showDialogBox('newProjectBox');
    var txtNewProject = $('#txtNewProject')[0];
    txtNewProject.value = '';
    txtNewProject.focus();
    $('#newProjectBox').css('display', 'block');
}

function closeNewProjectBox(event) {
    MaskDiv.hide();
    $('#newProjectBox').css('display', 'none');
}

function showNewItemBox(e, projectId) {
    var c = $('#newItemBoxContent' + projectId)[0];
    var n = $('#newItemBox')[0];
    if (!c.hasChildNodes()) {
        c.appendChild(n);
    }
    c.parentNode.style.display = '';
    n.style.display = '';
    var txtNewItem = $('#txtNewItem')[0];
    txtNewItem.projectId = projectId;
    txtNewItem.value = '';
    txtNewItem.focus();
}

function closeNewItemBox(event) {
    var nib = $('#newItemBox');
    nib.css('display', 'none');
    nib.parent().parent().css('display', 'none');
    $('#newItemBox').appendTo($('#content'));
}

function deleteItem(e, itemId) {
    if (confirm('Do you want delete this item?')) {
        var params = 'item_id=' + itemId;
        $.get('/todone/delete_item/'
            , { item_id: itemId }
            , function (projectId) {
                $.get('/todone/get_item_list/'
                  , { project_id: projectId }
                  , function (html) {
                      $('#itemListContent' + projectId).html(html);
                  });
            }
            );

        var img = $('#itemLoading' + itemId);
        img.css('display', '');
        var c = $('#item' + itemId);
        c.css('display', 'none');
    }
}

function showProject(event, projectId) {
    if ($$('#div.currentProject')[0] != null) {
        $$('#div.currentProject')[0].removeClassName('currentProject');
    }
    var p = $(projectId);
    p.addClassName('currentProject');

    if ($$('#li.active')[0] != null) {
        $$('#li.active')[0].removeClassName('active');
    }
    d = Event.element(event);
    d.parentNode.addClassName('active');
}

function showItemInfo(itemId) {
    showDialogBox('itemInfoDialogBox');
    $('.itemInfoBox').css('display', 'none');
    $('item' + itemId + 'InfoBox').css('display', '');
}

function hideItemInfo(itemId) {
    /*var divId = 'item' + itemId + 'InfoBox';
      var div = $(divId);
      div.style.display = 'none';*/
}

function showNewCommentBox(itemId) {
    var c = $('#newCommentBoxContent' + itemId);
    var n = $('#newCommentBox');
    if (!c.hasChildNodes()) {
        c.appendChild(n);
    }

    c.style.display = '';
    n.style.display = '';

    $('#btnAddComment').itemId = itemId;
    $('#txtNewComment').value = '';
    $('#txtNewComment').focus();
}

function closeNewCommentBox(event) {
    $('#newCommentBox').style.display = 'none';
    $('#content').appendChild($('#newCommentBox'));
}

function addComment(event) {
    var itemId = Event.element(event).itemId;
    var params = 'text=' + encodeURIComponent($('#txtNewComment').value)
      + '&item_id=' + itemId;
    new Ajax.Request('/todone/add_comment/?' + params
        , {
            method: 'get'
        , onSuccess: function (transport) {
            var itemId = $('#btnAddComment').itemId;
            new Ajax.Updater('item' + itemId + 'InfoBox'
              , '/todone/get_item_info/?item_id=' + itemId
              , {
                  method: 'get'
              }
              );
        }
        });
    closeNewCommentBox(event);
    var loading = $('#commentLoading' + itemId);
    loading.style.display = 'block';
    var loadingText = $('#commentLoadingText' + itemId);
    loadingText.appendChild(document.createTextNode($('#txtNewComment').value));
}

function deleteComment(event, commentId) {
    var params = 'comment_id=' + commentId
    new Ajax.Request('/todone/delete_comment/?' + params
        , {
            method: 'get'
        , onSuccess: function (transport) {
            var itemId = itemComments + transport.responseText;
            new Ajax.Updater(itemId
              , '/todone/get_comment/?item_id=' + transport
              , {
                  method: 'get'
              }
              );
        }
        }
        );
}

function showDialogBox(boxId) {
    MaskDiv.show();
    with ($(boxId).style) {
        position = 'absolute';
        top = window.innerHeight / 4 + 'px';
        right = window.innerWidth / 4 + 'px';
        minWidth = window.innerWidth / 2 + 'px';
        miniHeight = window.innerHeight / 2 + 'px';
        zIndex = '6000';
        display = 'block';
    }
}

function closeDialogBox(boxId) {
    MaskDiv.hide();
    $(boxId).style.display = 'none';
}

function showLoading() {
    MaskDiv.show();
    var divLoad = $('#load');
    divLoad.style.display = "block";
}

function closeLoading() {
    MaskDiv.hide();
    var divLoad = $('#load');
    divLoad.style.display = "none";
}

function closeItemInfoDialogBox(ev) {
    $('#itemInfoDialogBox').style.display = 'none';
    MaskDiv.hide();
}

$(document).ready(function () {
    $('#txtNewItem').keypress(function (e) {
        switch (e.which) {
            case 13:
                addItem(e);
                return false;
                break;
            case 0:
                $('#txtNewItem').val('');
                break;
        }
    });
}
    );
