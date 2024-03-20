using Microsoft.AspNetCore.Mvc;
using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.Results;
using SolanisHotel.BLL.ViewModels;
using SolanisHotel.WebUI.Extensions;
using System.Security.Claims;

namespace SolanisHotel.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        readonly ILogger<ReservationController> _logger;

        readonly IReservationManager _reservationMan;

        public ReservationController(IReservationManager reservationMan, ILogger<ReservationController> logger)
        {
            _reservationMan = reservationMan;
            _logger = logger;
        }

        //----------//----------//

        [Route("Reservation/Create")]
        public IActionResult MakeReservation() => View();

        [HttpPost]
        [Route("Reservation/Create")]
        public async Task<IActionResult> MakeReservation(ReservationVM rvm)
        {
            CustomerDTO customerDTO = new CustomerDTO
            {
                Email = User.FindFirstValue(ClaimTypes.Email)!
            };
            rvm.CustomerDTO = customerDTO;

            OperationResult oR = await _reservationMan.CreateReservation(rvm);
            if (oR.Success)
            {
                HttpContext.Session.SetObject("tempReservation", (ReservationVM)oR.Data!);

                return RedirectToAction("ShowReservation");
            }
            else
            {
                if (oR.Exception != null)
                {
                    TempData["ReservationError"] = $"{oR.Message}"; //Todo:Hata mesajını log olarak kaydedebilirim. (LowPriority)
                }
                else
                {
                    TempData["ReservationError"] = $"{oR.Message}";
                }
                return View();
            }
        }

        //-----//-----//

        [Route("Reservation/Show")]
        public IActionResult ShowReservation()
        {
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");
            return (tempReservation != null) ? View(tempReservation) : RedirectToAction("Error", "Home");
        }

        //-----//-----//

        [HttpPost]
        public async Task<IActionResult> CancelReservation()
        {
            ReservationVM? tempReservation = HttpContext.Session.GetObject<ReservationVM>("tempReservation");
            if (tempReservation != null)
            {
                HttpContext.Session.Clear();

                OperationResult oR = await _reservationMan.CancelReservation(tempReservation);

                return oR.Success ? RedirectToAction("Index", "Home") : RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        //----------//----------//
    }
}