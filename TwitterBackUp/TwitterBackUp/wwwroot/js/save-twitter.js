$(document).on('click', '#save-twitter-btn', function () {
    var saveTwitterBtn = $(this);

    var screenName = saveTwitterBtn.attr('data-name');
    saveTwitterBtn.button('loading');

    $.ajax({
        url: "/Twitter/Save?screenName=" + screenName,
        type: "POST",

        success: function () {
            saveTwitterBtn.html('Successfully Saved');
            saveTwitterBtn.removeClass('btn-primary').addClass('btn-success');
        },

        error: function () {
            saveTwitterBtn.button('reset');
        }
    });
});
