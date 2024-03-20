using Microsoft.AspNetCore.Mvc;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.Results;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.COMMON.Tools;
using SolanisHotel.WebUI.Extensions;

namespace SolanisHotel.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        readonly ICustomerManager _customerManager;

        IPaymentManager _paymentMan;

        public PaymentController(IPaymentManager paymentMan, ICustomerManager customerManager)
        {
            _paymentMan = paymentMan;
            _customerManager = customerManager;
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
                    CustomerDTO = tempReservation.CustomerDTO, //Kayıtlı müşteri ise bilgileri / Değilse misafir nesnesi.
                    suitableRoomsDTO = tempReservation.SuitableRoomsDTO, //Seçilmiş odaların bilgileri.
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

            #region TempPaymentTool
            PaymentProcessor processor = new PaymentProcessor(); //Ödeme nesnesini çağır.
            PaymentResult pR = processor.ProcessPayment(pvm.PaymentDTO.CardInfo, tempReservation.ReservationDTO.TotalPrice); //Ödemeyi gerçekleştir ve sonucu dön.
            #endregion

            //Payment nesnesi yaratır, ödeme sonucuna göre mevcut rezervasyon bilgisi ile odaları günceller.
            tempReservation.PaymentDTO = await _paymentMan.CreatePayment(pR, tempReservation.ReservationDTO.Id);

            HttpContext.Session.SetObject("tempReservation", tempReservation);

            return pR.IsSuccessful ? RedirectToAction("PaymentSuccessful") : RedirectToAction("PaymentFailed");
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


//ReservationVM test = new ReservationVM
//{
//    CheckIn = DateTime.Now,
//    CheckOut = DateTime.Now.AddDays(1),

//    ResDTO = new ReservationDTO
//    {
//        NumberOfExtraBeds = 1,
//    },

//    PaymentDTO = new PaymentDTO
//    {
//        TotalAmount = 20,
//        TransactionCode = 123456
//    },

//    SuitableRoomsDTO = new List<RoomDTO>
//    {
//        new RoomDTO
//        {
//            RoomNumber = 101
//        },

//        new RoomDTO
//        {
//            RoomNumber = 102
//        }
//    }
//};