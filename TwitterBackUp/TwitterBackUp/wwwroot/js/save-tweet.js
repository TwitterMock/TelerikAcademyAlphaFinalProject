$(document).on('click', '#save-tweet-btn', function () {
    var saveTweetButton = $(this);
    saveTweetButton.button('loading');
    var tweetId = saveTweetButton.attr('data-tweet-id');

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    
    $.ajax({
        url: "/Tweet/Save?id=" + tweetId,
        type: "POST",
        data: {
            'key': 'some_data',
            __RequestVerificationToken: token
        },
        success: function () {
            saveTweetButton.html('Successfully Saved');
            saveTweetButton.removeClass('btn-primary').addClass('btn-success');
        },

        error: function () {
            saveTweetButton.button('reset');
        }
    });
});