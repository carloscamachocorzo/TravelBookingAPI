using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Reservation;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Services
{
    public class ReservationsAppService: IReservationsAppService
    {
        private readonly IReservationsRepository _reservationsRepository;

        public ReservationsAppService(IReservationsRepository reservations)
        {
            _reservationsRepository = reservations;
        }
        public async Task<RequestResult<IEnumerable<ReservationResponseDto>>> ExecuteAsync()
        {
            try
            {
                var reservations = await _reservationsRepository.GetAllAsync();
                var result= reservations.Select(r => new ReservationResponseDto
                {
                    //Id = r.Id,
                    //GuestName = r.GuestName,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    RoomId = r.RoomId
                    //TotalPrice = r.TotalPrice
                });
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateSuccessful(result, new List<string> { "query done successfully" });
            }
            catch (Exception ex)
            {
                // Si ocurrió un error
                return RequestResult<IEnumerable<ReservationResponseDto>>.CreateError("Error al crear el hotel: " + ex.Message);
            }
           
        }
    }
}
