// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Aşağıdaki kodu yeni bir dosyaya taşıyacağım.
function EditPhoneFormat() {
    var input = document.getElementById("phoneNumber");
    var deger = input.value.replace(/\D/g, ''); // Sadece rakamları al

    // Başındaki '+90 ' kontrolü
    if (!deger.startsWith('90')) {
        deger = '90' + deger;
    }

    if (deger.length >= 5 && deger.length <= 8) {
        // İlk tireyi ekle
        deger = deger.replace(/(\d{5})(\d{1,3})/, '$1-$2');
    } else if (deger.length > 8) {
        // İkinci tireyi ekle
        deger = deger.replace(/(\d{5})(\d{3})(\d{1,4})/, '$1-$2-$3');
    }

    // Boşluk ekle
    deger = deger.replace(/-/g, ' ');

    input.value = '+90 ' + deger.substring(2); // Başındaki '90' kısmını atla
}
