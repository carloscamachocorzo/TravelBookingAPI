namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to create a new room.
    /// </summary>
    public class CreateRoomDto
    {
        /// <summary>
        /// The unique number identifying the room.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// The type of the room (e.g., Single, Double, Suite,Family).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The location of the room within the hotel.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// The base cost of the room, excluding taxes.
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax applied to the base cost of the room.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Indicates whether the room is available or not.
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Gets or sets the maximum number of guests that the room can accommodate.
        /// </summary>
        public int MaxCapacity { get; set; }
    }

}
