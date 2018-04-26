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