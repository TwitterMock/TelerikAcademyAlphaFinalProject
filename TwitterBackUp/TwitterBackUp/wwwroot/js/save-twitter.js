$(document).on('click', '#save-twitter-btn', function () {
    var saveTwitterBtn = $(this);

    var screenName = saveTwitterBtn.attr('data-twitter-srcname');
    saveTwitterBtn.button('loading');

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    
    $.ajax({
        url: "/Twitter/Save?screenName=" + screenName,
        type: "POST",
        data: {
            __RequestVerificationToken: token
        },
        success: function () {
            saveTwitterBtn.html('Twitter Saved');
            saveTwitterBtn.removeClass('btn-primary').addClass('btn-success');
            saveTwitterBtn.attr("disabled", true);
        },

        error: function () {
            saveTwitterBtn.button('reset');
        }
    });
});
