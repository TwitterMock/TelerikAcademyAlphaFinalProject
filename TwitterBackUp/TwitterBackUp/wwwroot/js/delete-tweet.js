$(document).on('click', '#delete-tweet-btn', function () {
    var removeTweetBtn = $(this);
    removeTweetBtn.button('loading');
    var tweetId = removeTweetBtn.attr('data-tweet-id');

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax({
        url: "/Tweet/Delete?tweetId=" + tweetId,
        type: "POST",
        data: {
            __RequestVerificationToken: token
        },
        success: function () {
            removeTweetBtn.html('Tweet Removed');
            removeTweetBtn.attr("disabled", true);
            $(`#${tweetId}`).remove();
        }
    });
});