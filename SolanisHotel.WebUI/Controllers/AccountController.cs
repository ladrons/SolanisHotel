using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.COMMON.Tools;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.WebUI.Controllers
{
    public class AccountController : Controller
    {
        ICustomerManager _customerMan;

        public AccountController(ICustomerManager customerMan)
        {
            _customerMan = customerMan;
        }

        //----------//----------//

        [Route("Account/Register")]
        public IActionResult Register() => View();

        [HttpPost]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(AccountVM avm)
        {
            if (ModelState.IsValid)
            {
                avm.CustomerDTO.Password = EncryptionService.SHA256Encrypt(avm.CustomerDTO.Password);                

                if (await _customerMan.RegisterOrUpdateCustomer(avm.CustomerDTO))
                {
                    TempData["RegistrationSuccessful"] = "Başarılı bir şekilde kayıt olundu!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["RegistrationFailed"] = "Girdiğiniz e-posta adresi zaten kullanımda. Lütfen başka bir e-posta adresi deneyin veya hesabınıza giriş yapın."; 
                    return View();
                }
            }
            return View();
        }

        //-----//-----//

        [Route("Account/LogIn")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("Account/LogIn")]
        public async Task<IActionResult> Login(AccountVM avm)
        {
            Customer foundCustomer = await _customerMan.AuthenticateCustomer(avm.CustomerDTO.Email, EncryptionService.SHA256Encrypt(avm.CustomerDTO.Password));
            if (foundCustomer != null)
            {
                await HttpContext.SignInAsync(AuthService.CreatePrincipal(foundCustomer));

                TempData["loginSuccessful"] = $"Hoşgeldiniz {foundCustomer.FirstName}";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["CustomerNotFound"] = "Geçersiz E-Posta veya Şifre";
                return View(avm);
            }
        }

        //-----//-----//

        [Authorize]
        [Route("Account/LogOut")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            TempData["logoutSuccessful"] = "Oturum kapatıldı";
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        //-----//-----//
    }
}