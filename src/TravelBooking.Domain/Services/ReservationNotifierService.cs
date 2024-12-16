using TravelBooking.Domain.Interfaces;
using TravelBooking.Domain.Services.Interfaces;
using TravelBooking.Infraestructure;

namespace TravelBooking.Domain.Services
{
    public class ReservationNotifierService : IReservationNotifierService
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
            await _emailService.SendEmailAsync(reservation.User.Email, $"Detalles de tu reserva {reservation.ReservationId}", emailContent);
        }

        private string GenerateEmailContent(Reservations reservation)
        {
            return $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333333;
                margin: 0;
                padding: 0;
            }}
            .email-container {{
                max-width: 600px;
                margin: auto;
                padding: 20px;
                border: 1px solid #dddddd;
                border-radius: 8px;
                background-color: #f9f9f9;
            }}
            .header {{
                text-align: center;
                background-color: #4CAF50;
                color: white;
                padding: 10px 0;
                border-radius: 8px 8px 0 0;
            }}
            .content {{
                padding: 20px;
            }}
            .details {{
                margin: 20px 0;
            }}
            .details p {{
                margin: 5px 0;
            }}
            .footer {{
                text-align: center;
                font-size: 0.9em;
                color: #666666;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='header'>
                <h2>¡Gracias por tu reserva, {reservation.User?.FirstName}!</h2>
            </div>
            <div class='content'>
                <p>Estamos emocionados de darte la bienvenida a nuestro hotel. Aquí tienes los detalles de tu reserva:</p>
                <div class='details'>
                    <p><strong>Fecha de entrada:</strong> {reservation.CheckInDate:yyyy-MM-dd}</p>
                    <p><strong>Fecha de salida:</strong> {reservation.CheckOutDate:yyyy-MM-dd}</p>
                    <p><strong>Habitación:</strong> {reservation.Room.Number}</p>
                    <p><strong>Total Huespedes:</strong> {reservation.TotalGuests}</p>
                    <p><strong>Precio total:</strong> {reservation.TotalCost:C}</p>
                </div>
                <p>Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>
                <p>¡Te esperamos pronto!</p>
            </div>
            <div class='footer'>
                <p>El equipo del hotel</p>
                <p><a href='https://www.hotel.com' style='color: #4CAF50; text-decoration: none;'>Visita nuestro sitio web</a></p>
            </div>
        </div>
    </body>
    </html>";
        }

    }
}
