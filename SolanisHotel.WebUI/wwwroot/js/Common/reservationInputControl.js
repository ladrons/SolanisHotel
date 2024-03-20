//Oda sayýsý/Misafir sayýsý input kontrol
$(document).ready(function () {

    ReservationDateValidation();

    //Oda sayýsýný kontrol et.
    $("#roomCount").on("input", function () {
        var roomMax = parseInt($(this).attr("max"));
        var roomMin = parseInt($(this).attr("min"));

        var value = parseInt($(this).val());

        if (value > roomMax || value < roomMin) {
            $(this).val('');            
        }
    });

    //Misafir sayýsýný kontrol et.
    $("#guestCount").on("input", function () {
        var guestMax = parseInt($(this).attr("max"));
        var guestMin = parseInt($(this).attr("min"));

        var value = parseInt($(this).val());

        if (value > guestMax || value < guestMin) {
            $(this).val('');
        }
    });
});

var currentDate = new Date();

function ReservationDateValidation() {
    $("#checkIn").attr("min", formatDate(currentDate));

    //CheckIn deðeri deðiþtiðinde;
    $("#checkIn").change(function () {
        // CheckIn deðerini al
        var checkInDate = new Date($("#checkIn").val());

        // CheckOut deðerini sýfýrla
        $("#checkOut").val("");

        // CheckOut'un minimum deðerini belirle
        $("#checkOut").attr("min", formatDate(checkInDate));
    });

    $("#checkOut").change(function () {
        // CheckIn ve CheckOut deðerlerini al
        var checkInDate = new Date($("#checkIn").val());
        var checkOutDate = new Date($("#checkOut").val());

        // Kontrolü yap
        if (checkOutDate < checkInDate) {
            alert("Check Out date cannot be before Check In date!");
            // Ýsterseniz hata mesajýný farklý bir þekilde gösterebilirsiniz.
            // Örneðin, bir div içine ekleyerek sayfada göstermek.
            // $("#error-message").text("Check Out date cannot be before Check In date!");
        }
    });

    // CheckOut deðeri girilirken CheckIn deðeri girilmediyse uyarý ver
    $("#checkOut").focusout(function () {
        if ($("#checkIn").val() === "") {
            alert("Please enter CheckIn value!");
        }
    });

    function formatDate(date) {
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();

        // Ay ve gün deðerlerini düzenle
        month = (month < 10) ? "0" + month : month;
        day = (day < 10) ? "0" + day : day;

        return year + "-" + month + "-" + day;
    }
}