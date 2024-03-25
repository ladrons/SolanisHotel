using Microsoft.AspNetCore.Mvc;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.COMMON.Tools;
using SolanisHotel.WebUI.Extensions;

namespace SolanisHotel.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        readonly PaymentProcessor _processor;

        readonly ICustomerManager _customerManager;

        IPaymentManager _paymentMan;

        public PaymentController(IPaymentManager paymentMan, ICustomerManager customerManager, PaymentProcessor processor)
        {
            _paymentMan = paymentMan;
            _customerManager = customerManager;
            _processor = processor;
        }

        //----------//----------//

        [Route("Payment/MakePayment")]
        public IActionResult ProcessPayment()
        {
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");
            if (tempReservation != null)
            {
                PaymentVM pvm = new PaymentVM
                {
                    ReservationVM = tempReservation,

                    CustomerDTO = tempReservation.CustomerDTO, //Kayıtlı müşteri ise bilgileri / Değilse misafir nesnesi.
                    
                    PaymentInformation = new PaymentInformation
                    {
                        Amount = tempReservation.ReservationDTO.TotalPrice,
                    }
                };

                return View(pvm);
            }
            else { return RedirectToAction("Error", "Home"); }
        }

        [HttpPost]
        [Route("Payment/MakePayment")]
        public async Task<IActionResult> ProcessPayment(PaymentVM pvm)
        {
            //Session üzerinden mevcut verileri çek.
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");

            //Güncel müşteri nesnesini yeni veriler ile değiştir ve DB'de güncelle.
            tempReservation!.CustomerDTO = await _customerManager.UpdateCustomerIfGuest(pvm.CustomerDTO, tempReservation.ReservationDTO.CustomerId);

            //API tarafına ulaş ve sonucu al.
            PaymentResult pR = await _processor.SendPaymentRequest(pvm.PaymentInformation);

            //Payment nesnesi yaratır, ödeme sonucuna göre mevcut rezervasyon bilgisi ile odaları günceller.
            tempReservation.PaymentDTO = await _paymentMan.CreatePayment(pR, tempReservation.ReservationDTO.Id);

            HttpContext.Session.SetObject("tempReservation", tempReservation);

            return pR.Result ? RedirectToAction("PaymentSuccessful") : RedirectToAction("PaymentFailed");
        }

        //-----//-----//

        public IActionResult PaymentSuccessful()
        {
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");
            HttpContext.Session.Clear();

            return tempReservation != null ? View(tempReservation) : RedirectToAction("Error","Home");
        }

        public IActionResult PaymentFailed()
        {
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");
            HttpContext.Session.Clear();

            return tempReservation != null ? View(tempReservation) : RedirectToAction("Error", "Home");
        }

        //-----//-----//
    }
}