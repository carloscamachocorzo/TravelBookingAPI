namespace TravelBooking.Application.Dtos.Reservation
{
    public class ReservationResponseDto
    {
        public int ReservationId { get; set; }

        public int RoomId { get; set; }

        public int UserId { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public DateTime ReservationDate { get; set; }

        public decimal TotalCost { get; set; }

        public virtual ICollection<EmergencyContactsResponseDto> EmergencyContacts { get; set; } = new List<EmergencyContactsResponseDto>();
         
    }
}
