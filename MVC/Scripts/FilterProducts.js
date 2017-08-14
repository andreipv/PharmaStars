function getProducts() {
    $('#products').html("");

    var checkedCategories = [];
    var checkedManufacturers = [];
    $('input:checkbox').each(function () {
        if ($(this).is(':checked')) {
            var arr = $(this).attr('id').split('+');
            if (arr[0] === "cat") {
                checkedCategories.push(arr[1]);
            } else {
                checkedManufacturers.push(arr[1]);
            }
        }
    });

    var send = {
        ManufacturerFilters: checkedManufacturers,
        CategoryFilters: checkedCategories
    };

    $.ajax({
        type: 'POST',
        url: 'http://localhost:51306/api/FilteredProducts',
        data: send,
        dataType: 'json',
        success: function (resultData) {
            $('#products').html("");
            $.each(resultData, function (key, value) {
                var str = '';
                $.each(value.Categories, function (key1, value1) {
                    str = str + " " + value1 + "<br />";
                });
                $('#products').append(
                    "<div class='col-md-4'>"
                    + "<div class='panel panel-default'>"
                    + "<div class='panel-heading'>"
                    + "<h3 class='panel-title'>" + value.Name + "</h3>"
                    + "</div>"
                    + "<div class='panel-body fixed-panel'>"
                    + "//future: imagine"
                    + "</div>"
                    + "<div class='panel-footer'>"
                    + "<b>Price:</b>" + value.Price + "<br />"
                    + "<b>Manufacturer:</b>" + value.Manufacturer + "<br />"
                    + "<b>Categories:</b> <br />"
                    + str
                    + "</div>"
                    + "</div>"
                    + "</div>");
            });
        },
        error: function (result) {
            alert('Fail ' + result.d);
        }
    });
}