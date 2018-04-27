$(document).ready(function () {
    $('.currentTweetDetails').on('click', function () {
        var tId = $('.currentTweetDetails').attr('data-name');
       
        $.ajax({

            url: "/Tweet/SaveUserTweet?userTweetId=" + tId,
            type: "POST",

            success: function (response) {
            
                $('.modal-header').append(response);
            },

            error: function (response) {
                alert(JSON.stringify(response).Message);
            }
        });
    });
})