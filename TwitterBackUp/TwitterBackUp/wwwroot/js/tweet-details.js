$(document).on('click', '.tweet-details-btn', function () {
    var screenName = $(this).attr('data-twitter-srcname');
    var tweetId = $(this).attr('data-tweet-id');
    var queryString = `twitterScreenName=${screenName}&tweetId=${tweetId}`;

    $.ajax({
        dataType: "json",
        url: '/Tweet/Html?' + queryString,
        type: 'GET',
        success: function (data) {
            var html = data.html;
            $("#tweet-modal-body").html(html);
            $('#save-tweet-btn').attr('data-tweet-id', tweetId);
            $('#details-modal').modal();
        }
    });
});

$('#details-modal').on('hidden.bs.modal', function () {
    var saveTweetBtn = $('#save-tweet-btn');
    saveTweetBtn.button('reset');
    saveTweetBtn.removeClass('btn-success').addClass('btn-primary');
})