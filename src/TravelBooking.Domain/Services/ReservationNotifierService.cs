using TravelBooking.Domain.Interfaces;
using TravelBooking.Domain.Services.Interfaces;
using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Services
{
    public class ReservationNotifierService: IReservationNotifierService
    {
        private readonly IEmailService _emailService;

        public ReservationNotifierService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task NotifyReservationAsync(Reservations reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            var emailContent = GenerateEmailContent(reservation);
            await _emailService.SendEmailAsync(reservation.User.Email, "Detalles de tu reserva", emailContent);
        }

        private string GenerateEmailContent(Reservations reservation)
        {
            return $@"
            Hola {reservation.User?.FirstName},

            Gracias por tu reserva. Aquí están los detalles:
            - Fecha de entrada: {reservation.CheckInDate:yyyy-MM-dd}
            - Fecha de salida: {reservation.CheckOutDate:yyyy-MM-dd}
            - Habitación: {reservation.RoomId}
            - Precio total: {reservation.TotalCost:C}

            ¡Te esperamos!

            Saludos,
            El equipo del hotel.";
        }
    }
}
