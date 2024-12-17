namespace TravelBooking.Application.Dtos.Rooms
{
    /// <summary>
    /// Represents a response DTO containing details about a room in a hotel.
    /// </summary>
    public class RoomResponseDto
    {
        /// <summary>
        /// The name or number of the room.
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// The name of the hotel where the room is located.
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// The type of the room (e.g., "Standard", "Deluxe").
        /// </summary>
        public string RoomType { get; set; }

        /// <summary>
        /// The maximum capacity of the room (i.e., how many people it can accommodate).
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// The rate of the room (e.g., price per night).
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Indicates whether the room is available for booking.
        /// </summary>
        public bool IsAvailable { get; set; }
    }

}
