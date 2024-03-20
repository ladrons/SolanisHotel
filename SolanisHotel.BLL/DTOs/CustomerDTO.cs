﻿using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SolanisHotel.BLL.DTOs
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "E-Posta boş bırakılamaz!")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi giriniz!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Telefon boş bırakılamaz!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "İsim boş bırakılamaz!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim boş bırakılamaz")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Doğum tarihi boş bırakılamaz!")]
        public DateTime BirthDate { get; set; }
    }
}