using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Results;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface IPaymentManager : IManager<Payment>
    {
        Task<PaymentDTO> CreatePayment(PaymentResult paymentResult, int reservationId);
        Task<Payment> GeneratePayment(bool result, Reservation currentReservation, int transactionCode, DateTime paymentDate);
    }
}