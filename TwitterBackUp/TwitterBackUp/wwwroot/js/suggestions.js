$(document).on('change', '#categories-options', function () {
    var selector = $(this);
    var selectedCategory = selector.find(":selected").text();
    selector.attr("disabled", true);

    $.ajax({
        dataType: 'json',
        url: "/Twitter/Suggestions?category=" + selectedCategory,
        type: "GET",
        success: function (response) {
            var suggestions = [];
            response.forEach(r => suggestions.push(r.screen_name));

            $("#search-input").autocomplete({
                source: suggestions
            });

            selector.attr("disabled", false);
        }
    });
});