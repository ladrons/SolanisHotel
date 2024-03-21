var isReservationCanceled = false;
var continueClicked = false;

var countdownInterval;

$(document).ready(function () {
    startCountdown();

    handleReservationExit();

    $('#continueButton').click(function () {
        continueClicked = true;
    });

});

function handleReservationExit() {

    $(window).on('beforeunload', function () {
        clearInterval(countdownInterval);

        if (!isReservationCanceled) {
            if (!continueClicked) {
                cancelReservation();
            }
        }
        isReservationCanceled = true;
    });

}

function startCountdown() {
    var countdownSeconds = 300; // 5 dakika
    var countdownDisplay = document.getElementById("countdown");
    console.log("Countdown ACTIVE!");

    countdownInterval = setInterval(function () {
        var minutes = Math.floor(countdownSeconds / 60);
        var seconds = countdownSeconds % 60;

        countdownDisplay.textContent = minutes + " dakika " + seconds + " saniye"; // Ekranda geri sayýmý güncelle

        if (countdownSeconds == 0) {
            clearInterval(countdownInterval);

            isReservationCanceled = true;

            cancelReservation();
        }

        countdownSeconds--;
        console.log(countdownSeconds);
    }, 1000); // 1 saniye aralýklarla geri sayým yap
}


function cancelReservation() {
    $.ajax({
        url: '/Reservation/CancelReservation',
        type: 'POST',
        success: function () {
            console.log('Reservation canceled successfully');
            window.location.href = '/Home/Index';
        },
        error: function (xhr, status, error) {
            console.error('Error canceling reservation: ', error);
        }
    });
}