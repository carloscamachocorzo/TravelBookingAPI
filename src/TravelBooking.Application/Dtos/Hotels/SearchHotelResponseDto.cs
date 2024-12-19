namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the response DTO for searching hotels.
    /// </summary>
    public class SearchHotelResponseDto
    {
        /// <summary>
        /// The name of the hotel.
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// The address of the hotel.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The city where the hotel is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The base rate for a room in the hotel, before taxes.
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax applied to the base rate of the room.
        /// </summary>
        public decimal Tax { get; set; }
        /// <summary>
        /// List of rooms in the hotel.
        /// </summary>
        public List<SearchHotelRoomResponseDto> Rooms { get; set; }
    }

}
