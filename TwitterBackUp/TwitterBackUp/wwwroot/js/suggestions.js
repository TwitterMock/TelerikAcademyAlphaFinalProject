$(document).on('change', '#categories-options', function () {
    var selectedCategory = $(this).find(":selected").text();

    $.ajax({
        dataType: 'json',
        url: "/SearchSuggestions/GetSuggestions?input=" + selectedCategory,
        type: "GET",
        success: function (response) {
            var suggestions = [];
            response.forEach(r => suggestions.push(r.screen_name));

            $("#search-input").autocomplete({
                source: suggestions
            });

            $('.ui-menu.ui-widgetui-widget-content.ui-autocomplete.ui-front').addClass('');
        }
    });
});