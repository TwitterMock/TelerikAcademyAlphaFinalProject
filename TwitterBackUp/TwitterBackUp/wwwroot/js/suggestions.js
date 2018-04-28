$(document).on('change', '#categories-options', function () {
    var selector = $(this);
    var selectedCategory = selector.find(":selected").val();
    selector.attr("disabled", true);

    $.ajax({
        dataType: 'json',
        url: "/Twitter/Suggestions?category=" + selectedCategory,
        type: "GET",
        success: function (data) {
            var suggestions = [];

            if (data) {
                data.forEach(r => suggestions.push(r.screen_name));
            } else {
                alert(data);
            }

            $("#search-input").autocomplete({
                source: suggestions
            });

            selector.attr("disabled", false);
        }
    });
});