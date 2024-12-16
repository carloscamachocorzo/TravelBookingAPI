namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the response data for creating a hotel.
    /// </summary>
    public class CreateHotelResponseDto
    {
        /// <summary>
        /// The unique identifier of the created hotel.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the created hotel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the created hotel.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The city where the created hotel is located.
        /// </summary>
        public string City { get; set; }
    }

}
