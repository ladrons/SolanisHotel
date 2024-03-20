//Oda say�s�/Misafir say�s� input kontrol
$(document).ready(function () {

    ReservationDateValidation();

    //Oda say�s�n� kontrol et.
    $("#roomCount").on("input", function () {
        var roomMax = parseInt($(this).attr("max"));
        var roomMin = parseInt($(this).attr("min"));

        var value = parseInt($(this).val());

        if (value > roomMax || value < roomMin) {
            $(this).val('');            
        }
    });

    //Misafir say�s�n� kontrol et.
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

    //CheckIn de�eri de�i�ti�inde;
    $("#checkIn").change(function () {
        // CheckIn de�erini al
        var checkInDate = new Date($("#checkIn").val());

        // CheckOut de�erini s�f�rla
        $("#checkOut").val("");

        // CheckOut'un minimum de�erini belirle
        $("#checkOut").attr("min", formatDate(checkInDate));
    });

    $("#checkOut").change(function () {
        // CheckIn ve CheckOut de�erlerini al
        var checkInDate = new Date($("#checkIn").val());
        var checkOutDate = new Date($("#checkOut").val());

        // Kontrol� yap
        if (checkOutDate < checkInDate) {
            alert("Check Out date cannot be before Check In date!");
            // �sterseniz hata mesaj�n� farkl� bir �ekilde g�sterebilirsiniz.
            // �rne�in, bir div i�ine ekleyerek sayfada g�stermek.
            // $("#error-message").text("Check Out date cannot be before Check In date!");
        }
    });

    // CheckOut de�eri girilirken CheckIn de�eri girilmediyse uyar� ver
    $("#checkOut").focusout(function () {
        if ($("#checkIn").val() === "") {
            alert("Please enter CheckIn value!");
        }
    });

    function formatDate(date) {
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();

        // Ay ve g�n de�erlerini d�zenle
        month = (month < 10) ? "0" + month : month;
        day = (day < 10) ? "0" + day : day;

        return year + "-" + month + "-" + day;
    }
}