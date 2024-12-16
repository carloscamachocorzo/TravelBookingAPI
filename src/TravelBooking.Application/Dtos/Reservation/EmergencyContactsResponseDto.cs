namespace TravelBooking.Application.Dtos.Reservation
{
    public class EmergencyContactsResponseDto
    {
        public int EmergencyContactId { get; set; }

        public int ReservationId { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
