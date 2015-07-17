$(document).ready(function () {
    $('#btnAddMessage').click(function (event) {
        var txtMessage = $('#txtMessage');
        var log_text = txtMessage.val();
        txtMessage.attr('disabled', 'disabled');

        var btnAdd = $('#btnAddMessage');
        btnAdd.attr('disabled', 'disabled');

        var imgLoading = $('#imgLoading');
        imgLoading.addClass('is-active').show();

        var itemList = $('#itemList');
        itemList.attr('disabled', true);

        $.post('/ilog/say'
        , { text: log_text }
        , function (data) {
            txtMessage.val('').focus().removeAttr('disabled');
            btnAdd.removeAttr('disabled');
            imgLoading.removeClass('is-active').hide();

            $.get('/ilog/ls'
                , function (data) {
                    itemList.html(data);
                    itemList.removeAttr('disabled');
                });
        }
        , 'text');
    });

    $('#txtMessage').keypress(function (e) {
        if (13 == e.which) {
            $('#btnAddMessage').click();
            return;
        }
    });
});
