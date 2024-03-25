using SolanisHotel.BLL.DTOs;
using SolanisHotel.COMMON.Tools;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IPaymentManager : IManager<Payment>
    {
        Task<PaymentDTO> CreatePayment(PaymentResult paymentResult, int reservationId);
        Task<Payment> GeneratePayment(bool result, Reservation currentReservation, int transactionCode, DateTime paymentDate);
    }
}