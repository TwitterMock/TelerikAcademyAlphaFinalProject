$(document).ready(function () {
    $('.category').on('click',
        function () {
            var selectedCategory = this.getAttribute('data-name');

            $.ajax({
                dataType: 'json',
                url: "/SearchSuggestions/GetSuggestions?input=" + selectedCategory,
                type: "GET",
                success: function (response) {
                    var resultTwits = [];
                    response.forEach(r => resultTwits.push(r.screen_name));
                                     
                    $("#suggestions").autocomplete({
                        source: resultTwits
                    });

                    $('.ui-menu.ui-widgetui-widget-content.ui-autocomplete.ui-front').addClass('')
                }
            });
        });
});

$(document).ready(function () {
    $('#dropdownMenuButton').on('click', function () {
        $('.dropdown-menu').show();
        $('.dropdown-menu').closest('li').toggleClass('active');

    });

});


$(document).ready(function(){
    ('#dropdownMenuButton').on('click', function () {

    $('.dropdown-menu').hide().delay(1000);

    });
});
       





