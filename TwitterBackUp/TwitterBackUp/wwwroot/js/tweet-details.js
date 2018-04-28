$(document).on('click', '.tweet-details-btn', function () {
    var screenName = $(this).attr('data-name');
    var tweetId = $(this).attr('data-id');
    var queryString = `twitterScreenName=${screenName}&tweetId=${tweetId}`;

    $.ajax({
        dataType: "json",
        url: '/Tweet/Html?' + queryString,
        type: 'GET',
        success: function(data) {
            var html = data.html;
            $("#details-modal-body").html(html);
            $('#details-modal').modal();
            $('.save-tweet-btn').attr('data-id', tweetId);
        }
    });
});