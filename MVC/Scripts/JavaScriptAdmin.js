$("#DeleteProdBtn").on("click", function (event) {
    event.preventDefault();
    var id = $("#DeleteProdBtn").attr("data-id");
    $.ajax({
        url: "http://localhost:51306/api/products/" + id,
        type: 'DELETE',
        data: { id: id },
        success: function (data) {
            
                location.reload();
            
        }
    });
})