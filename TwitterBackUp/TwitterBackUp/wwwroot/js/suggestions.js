$(document).ready(function () {
    $('.category').on('click', function () {
      var selectedCategory=this.getAttribute('data-name');
        console.log(selectedCategory);
        $.ajax({
            dataType: 'json',
            url: "/SearchSuggestions/GetSuggestions?input=" + selectedCategory,
            type: "GET",
       
            success: function (response) {
                console.log(response);
                $("#suggestions").autocomplete({
                    source: response
                });
            }
            
        });
    })

   
});
$(document).ready(function () {
    $('#dropdownMenuButton').on('click', function () {
        $('.dropdown-menu').show();

    })


})