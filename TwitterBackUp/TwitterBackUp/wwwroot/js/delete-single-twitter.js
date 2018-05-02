$(document).ready(function () {
    $('#deleteSingleTwitterAccount').on('click', function () {
        var removeTwitterBtn = $(this);
        removeTwitterBtn.button('loading');
        var twitterId = $('#deleteSingleTwitterAccount').attr('data-name');

      
        console.log(twitterId);
        $.ajax({
            url: "/Twitter/DeleteTwitter?twitterId=" + twitterId,
            type: "POST",
        
            success: function () {
                removeTwitterBtn.html('Twitter Removed');
                removeTwitterBtn.attr("disabled", true);
                $(`#${twitterId}`).remove();
            }
        });
    });
});