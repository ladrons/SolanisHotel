function EditPhoneFormat() {
    var input = document.getElementById("phoneNumber");
/*    var deger = input.value.replace(/\D/g, ''); // Sadece rakamlarý al*/

    var phoneNumber = input.value.replace(/\D/g, ''); // Sadece rakamlarý al

    // Maksimum 10 rakam kontrolü
    phoneNumber = phoneNumber.substring(0, 12);

    // Baþýndaki '+90 ' kontrolü
    if (!phoneNumber.startsWith('90')) {
        phoneNumber = '90' + phoneNumber;
    }

    if (phoneNumber.length >= 5 && phoneNumber.length <= 8) {
        // Ýlk tireyi ekle
        phoneNumber = phoneNumber.replace(/(\d{5})(\d{1,3})/, '$1-$2');
    } else if (phoneNumber.length > 8) {
        // Ýkinci tireyi ekle
        phoneNumber = phoneNumber.replace(/(\d{5})(\d{3})(\d{1,4})/, '$1-$2-$3');
    }

    // Boþluk ekle
    phoneNumber = phoneNumber.replace(/-/g, ' ');

    input.value = '+90 ' + phoneNumber.substring(2); // Baþýndaki '90' kýsmýný atla
}