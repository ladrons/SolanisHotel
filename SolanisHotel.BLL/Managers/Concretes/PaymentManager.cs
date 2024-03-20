using AutoMapper;
using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.BLL.Results;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Enums;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class PaymentManager : BaseManager<Payment>, IPaymentManager
    {
        readonly IMapper _mapper;

        readonly IReservationManager _reservationMan;
        readonly IReservationRoomManager _reservationRoomMan;

        readonly IPaymentRepository _paymentRep;

        public PaymentManager(IPaymentRepository paymentRep, IReservationManager reservationMan, IReservationRoomManager reservationRoomMan, IMapper mapper) : base(paymentRep)
        {
            _paymentRep = paymentRep;
            _reservationMan = reservationMan;
            _reservationRoomMan = reservationRoomMan;
            _mapper = mapper;
        }

        //----------//----------//

        public async Task<PaymentDTO> CreatePayment(PaymentResult paymentResult, int reservationId)
        {
            Reservation currentReservation = await _reservationMan.FindAsync(reservationId);

            Payment newPayment = await GeneratePayment(paymentResult.IsSuccessful, currentReservation, paymentResult.TransactionCode, paymentResult.PaymentDate);
            currentReservation = await _reservationMan.UpdateReservationStatusAndPerformActions(currentReservation, paymentResult.IsSuccessful);

            if (currentReservation.ReservationStatus == ReservationStatus.Cancelled)
            {
                await _reservationRoomMan.DeleteReservationRooms(currentReservation.ReservationRooms);
            }

            return MappingToPaymentDTO(newPayment);
        }

        //----------//

        public async Task<Payment> GeneratePayment(bool result, Reservation currentReservation, int transactionCode, DateTime paymentDate)
        {
            Payment newPayment = new Payment
            {
                TransactionCode = transactionCode,
                TotalAmount = currentReservation.TotalPrice,
                PaymentDate = paymentDate,

                Reservation = currentReservation,

                PaymentStatus = (result) ? PaymentStatus.Complete : PaymentStatus.Cancelled
            };

            await _paymentRep.AddAsync(newPayment);
            await _paymentRep.SaveChangesAsync();

            return newPayment;
        }

        //----------//

        public PaymentDTO MappingToPaymentDTO(Payment payment) => _mapper.Map<PaymentDTO>(payment);

        //-----Privates Methods-----//



        //-----Privates Methods-----//





        //-----Test Area-----//

        //-----Test Area-----//
    }
}