$(function () {
    if ($("#register_popup") != undefined) {
        $('#register_popup').modal('show');
    }

    if ($("#login_popup") != undefined) {
        $('#login_popup').modal('show');
    }

    // display message if modal still loaded i
    if ($("#details-Id").val() > 0) {
        var Id = $("#details-Id").val();
        CopyToModal(Id);
        $('#product_details_popup').modal('show');
    } //details
    // details anchor click - to load popup on catalogue
    $("a.btn-default").on("click", function (e) {
        var Id = $(this).attr("data-id");
        $("#results").text("");
        CopyToModal(Id);
    });

    if ($(".js-showbydefault-modal") != undefined) {
        $('.js-showbydefault-modal').modal('show');
    }
});
function CopyToModal(id) {
    var x = $('#h-name' + id).val();
    $('#full-name').text(x);
    var y = "/img/" + $('#h-graphic' + id).val();
    $("#details-graphic").attr("src", y);
    $("#details-description").text($('#h-descr' + id).val());
    $('#qty-on-hand').text($('#h-on-hand' + id).val());
    $('#details-price').text($('#h-msrp' + id).val());
    $("#qty").val("0");
    $("#details-id").val(id);
}