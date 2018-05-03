$(document).on('change', '#categories-options', function () {
    var selector = $(this);
    var selectedCategory = selector.find(":selected").val();
    selector.attr("disabled", true);

    $.ajax({
        dataType: 'json',
        url: "/Twitter/SearchSuggestions?category=" + selectedCategory,
        type: "GET",
        success: function (response) {
            var suggestions = [];
            response.forEach(t => suggestions.push(t.screen_name));

            $("#search-input").autocomplete({
                source: suggestions
            });

            selector.attr("disabled", false);
        },
        error: function () {
            alert('pesho');
        }
    });
});