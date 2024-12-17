namespace TravelBooking.Application.Dtos.Reservation
{
    /// <summary>
    /// Represents the response DTO for an emergency contact associated with a reservation.
    /// </summary>
    public class EmergencyContactsResponseDto
    {
        /// <summary>
        /// The unique identifier for the emergency contact.
        /// </summary>
        public int EmergencyContactId { get; set; }

        /// <summary>
        /// The unique identifier for the reservation associated with this emergency contact.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The full name of the emergency contact.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The phone number of the emergency contact.
        /// </summary>
        public string PhoneNumber { get; set; }
    }

}
