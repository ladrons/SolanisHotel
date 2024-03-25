function EditPhoneFormat() {
    var input = document.getElementById("phoneNumber");
/*    var deger = input.value.replace(/\D/g, ''); // Sadece rakamlar� al*/

    var phoneNumber = input.value.replace(/\D/g, ''); // Sadece rakamlar� al

    // Maksimum 10 rakam kontrol�
    phoneNumber = phoneNumber.substring(0, 12);

    // Ba��ndaki '+90 ' kontrol�
    if (!phoneNumber.startsWith('90')) {
        phoneNumber = '90' + phoneNumber;
    }

    if (phoneNumber.length >= 5 && phoneNumber.length <= 8) {
        // �lk tireyi ekle
        phoneNumber = phoneNumber.replace(/(\d{5})(\d{1,3})/, '$1-$2');
    } else if (phoneNumber.length > 8) {
        // �kinci tireyi ekle
        phoneNumber = phoneNumber.replace(/(\d{5})(\d{3})(\d{1,4})/, '$1-$2-$3');
    }

    // Bo�luk ekle
    phoneNumber = phoneNumber.replace(/-/g, ' ');

    input.value = '+90 ' + phoneNumber.substring(2); // Ba��ndaki '90' k�sm�n� atla
}