$(document).ready(function () {
    $('#deleteTwitterAdmin').on('click', function () {
        var removeTwitterBtn = $(this);
        removeTwitterBtn.button('loading');
        var twitterId = $('#deleteTwitterAdmin').attr('data-twitter');
        var userId = $('#deleteTwitterAdmin').attr('data-name');

        var url = "DeleteTwitterAdmin?userId=" + userId + "&twitterId=" + twitterId;
        console.log(url);
        $.ajax({
            url: url,
            type: "POST",

            success: function () {
                removeTwitterBtn.html('Twitter Removed');
                removeTwitterBtn.attr("disabled", true);
                $(`#${twitterId}`).remove();
            }
        });
    });
});

