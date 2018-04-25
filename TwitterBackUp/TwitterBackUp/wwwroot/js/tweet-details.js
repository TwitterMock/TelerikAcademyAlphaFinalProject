//$(document).on('click', '.tweet-details', function () {
//    var tweetId = '980573232804884480';
//    var screenName = 'Trump';
//    var queryString = `twitterScreenName=${screenName}&tweetId=${tweetId}`;

//    $.ajax({
//        dataType: "json",
//        url: '/Tweet/GetTweetHtml?' + queryString,
//        type: 'GET',
//        success: function (data) {
//            var html = data.html;
//            $("#details-modal-body").html(html);
//            $('#details-modal').modal();
//        }
//    });

//});

var tweetDetails = function (screenName, tweetId) {
    var queryString = `twitterScreenName=${screenName}&tweetId=${tweetId}`;

    $.ajax({
        dataType: "json",
        url: '/Tweet/GetTweetHtml?' + queryString,
        type: 'GET',
        success: function (data) {
            var html = data.html;
            $("#details-modal-body").html(html);
            $('#details-modal').modal();
        }
    });

}