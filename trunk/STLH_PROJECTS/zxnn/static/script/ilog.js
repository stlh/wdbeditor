$(document).ready(function() {
    $('#btnAddMessage').click(function(event) {
        var txtMessage = $('#txtMessage');
        var log_text = txtMessage.val();
        txtMessage.attr('disabled', 'disabled');
        var btnAdd = $('#btnAddMessage');
        btnAdd.attr('disabled', 'disabled');
        var imgLoading = $('#imgLoading');
        imgLoading.show();
        $.post('/ilog/say'
        , {text: log_text}
        , function(data) {
            txtMessage.val('').focus().removeAttr('disabled');
            btnAdd.removeAttr('disabled');
            imgLoading.hide();
            $.get('/ilog/ls'
                , function (data) {
                    $('#itemls').html(data);
                });
            }
        , 'text');
    });

    $('#txtMessage').keypress(function(e) {
       if (13 == e.which) {
         $('#btnAddMessage').click();
         return;
       }
    });
});
