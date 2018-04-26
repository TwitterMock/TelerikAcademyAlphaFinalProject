$(document).ready(function () {
    $('#saveAccount').on('click', function () {
        var sName = $('#saveAccount').attr('data-name');

        $.ajax({

            url: "/Twitter/SaveTwitterAccount?userScreenName=" + sName,
            type: "POST",

            success: function (response) {
                $('form').append(response);
            },

            error: function (response) {
                alert(JSON.stringify(response).Message);
            }
        });
    });
})



