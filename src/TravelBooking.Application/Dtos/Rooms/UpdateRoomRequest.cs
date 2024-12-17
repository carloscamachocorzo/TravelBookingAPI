namespace TravelBooking.Application.Dtos.Rooms
{
    /// <summary>
    /// Represents a request DTO to update the details of a room.
    /// </summary>
    public class UpdateRoomRequest
    {
        /// <summary>
        /// The unique identifier for the room.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The number or name of the room (e.g., "101", "Deluxe Suite").
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The type of the room (e.g., "Standard", "Deluxe", "Suite").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The location or floor of the room within the hotel (e.g., "3rd floor", "Near the pool").
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The base cost per night for the room before tax.
        /// </summary>
        public decimal BaseCost { get; set; }

        /// <summary>
        /// The tax rate applied to the room cost.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// The availability status of the room (e.g., available, occupied, etc.).
        /// </summary>
        public bool Status { get; set; }
    }

}
