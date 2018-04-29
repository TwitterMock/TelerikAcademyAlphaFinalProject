$(document).on('click', '.tweet-details-btn', function () {
    var screenName = $(this).attr('data-twitter-srcname');
    var tweetId = $(this).attr('data-tweet-id');
    var queryString = `twitterScreenName=${screenName}&tweetId=${tweetId}`;

    $.ajax({
        dataType: "json",
        url: '/Tweet/RenderingDetails?' + queryString,
        type: 'GET',
        success: function (response) {
            var html = response.html;
            var isSaved = response.isSaved;

            $("#tweet-modal-body").html(html);
            var saveTweetBtn = $('#save-tweet-btn');

            if (isSaved) {
                saveTweetBtn.html('Tweet Saved');
                saveTweetBtn.removeClass('btn-primary').addClass('btn-success');
                saveTweetBtn.attr("disabled", true);
            }

            saveTweetBtn.attr('data-tweet-id', tweetId);
            $('#details-modal').modal();
        }
    });
});

$('#details-modal').on('hidden.bs.modal', function () {
    var saveTweetBtn = $('#save-tweet-btn');
    saveTweetBtn.button('reset');
    saveTweetBtn.attr("disabled", false);
    saveTweetBtn.removeClass('btn-success').addClass('btn-primary');
})