$(document).ready(function () {


    $('.deleteUser').on('click', function () {
        var data = $(this).attr('data-name');
        console.log(data)
        $.ajax({
            url: '/Admin/Home/DeleteUser?Id=' + data,
            type: 'POST',

            success: function () {

                $('.' + data).html('Deleted');
                $('.' + data).removeClass('btn btn-danger');
                $('.' + data).addClass('btn btn-success');

            }



        })

    })

})
