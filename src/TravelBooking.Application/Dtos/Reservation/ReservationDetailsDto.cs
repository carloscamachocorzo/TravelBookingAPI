namespace TravelBooking.Application.Dtos.Reservation
{
    /// <summary>
    /// Represents the detailed information for a reservation.
    /// </summary>
    public class ReservationDetailsDto
    {
        /// <summary>
        /// The unique identifier for the reservation.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The unique identifier for the hotel associated with the reservation.
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// The name of the hotel where the reservation is made.
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// The unique identifier for the room reserved.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The name or number of the room reserved.
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// The name of the user who made the reservation.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The check-in date for the reservation.
        /// </summary>
        public DateOnly CheckInDate { get; set; }

        /// <summary>
        /// The check-out date for the reservation.
        /// </summary>
        public DateOnly CheckOutDate { get; set; }

        /// <summary>
        /// The number of guests for this reservation.
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// The total cost of the reservation.
        /// </summary>
        public decimal TotalCost { get; set; }
    }

}
