namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the response details of a hotel room in a search operation.
    /// </summary>
    public class SearchHotelRoomResponseDto
    {
        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the type of the room (e.g., Standard, Deluxe, Suite).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the location of the room within the hotel.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the base rate for the room.
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the tax applicable to the room rate.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the status of the room indicating its availability.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the room in terms of the number of guests.
        /// </summary>
        public int MaxCapacity { get; set; }
    }

}
