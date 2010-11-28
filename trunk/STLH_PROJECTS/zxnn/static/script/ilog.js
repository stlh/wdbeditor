$(document).ready(function() {
    $('#btnAddMessage').click(function(event) {
      var txtMessage = $('#txtMessage');
      var log_text = txtMessage.val();
      txtMessage[0].disabled = true;
      $('#btnAddMessage')[0].disabled = true;
      var imgLoading = $('#imgLoading');
      imgLoading.show();
      txtMessage.enable = false;
      $.post('/ilog/say'
        , {text: log_text}
        , function(data) {
        $('#itemls').html(data);
        var txtMessage = $('#txtMessage').val('')[0];
        txtMessage.focus();
        txtMessage.disabled = false;
        $('#btnAddMessage')[0].disabled = false;
        var imgLoading = $('#imgLoading');
        var imgLoading = $('#imgLoading');
        imgLoading.hide();
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
