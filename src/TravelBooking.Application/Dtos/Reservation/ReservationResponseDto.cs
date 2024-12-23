namespace TravelBooking.Application.Dtos.Reservation
{
    /// <summary>
    /// Represents a response DTO containing reservation details including emergency contacts.
    /// </summary>
    public class ReservationResponseDto
    {
        /// <summary>
        /// The unique identifier for the reservation.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The unique identifier for the room reserved.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The unique identifier for the user who made the reservation.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The check-in date for the reservation.
        /// </summary>
        public DateOnly CheckInDate { get; set; }

        /// <summary>
        /// The check-out date for the reservation.
        /// </summary>
        public DateOnly CheckOutDate { get; set; }

        /// <summary>
        /// The total number of guests for the reservation.
        /// </summary>
        public int TotalGuests { get; set; }

        /// <summary>
        /// The date and time when the reservation was created.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// The total cost of the reservation.
        /// </summary>
        public decimal TotalCost { get; set; }

        public ICollection<GuestsResponseDto> Guests { get; set; } = new List<GuestsResponseDto>();
        /// <summary>
        /// A collection of emergency contacts associated with the reservation.
        /// </summary>
        public ICollection<EmergencyContactsResponseDto> EmergencyContacts { get; set; } = new List<EmergencyContactsResponseDto>();
    }

}
