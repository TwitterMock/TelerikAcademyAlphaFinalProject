﻿$(document).ready(function () {
   

    $('.promoteUser').on('click', function () { 
    var data = $(this).attr('data-name');
    console.log(data)
    $.ajax({
        url: '/Admin/Home/PromoteUser?Id=' + data,
        type: 'POST',

        success: function () {

            $('#' + data).html('Admin');
            $('#' + data).removeClass('btn btn-success');
            $('#' + data).addClass('btn btn-danger');

        }



    })

    })
    
})
