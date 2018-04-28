$(document).ready(function() {
    $('.save-tweet-btn').on('click',
        function () {
            var saveTweetButton = $(this);
            saveTweetButton.button('loading');
            var tweetId = saveTweetButton.attr('data-id');
            
            $.ajax({
                url: "/Tweet/SaveAsync?id=" + tweetId,
                type: "POST",

                success: function () {
                    saveTweetButton.html('Successfully Saved');
                    saveTweetButton.removeClass('btn-primary').addClass('btn-success');
                },

                error: function () {
                    saveTweetButton.button('reset');
                }
            });
        });
});