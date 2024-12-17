namespace TravelBooking.Application.Dtos.Rooms
{
    /// <summary>
    /// Represents a DTO for updating the status of a room.
    /// </summary>
    public class UpdateRoomStatusDto
    {
        /// <summary>
        /// Indicates whether the room is enabled (available) or disabled (unavailable).
        /// true = enabled (available), false = disabled (unavailable).
        /// </summary>
        public bool IsEnabled { get; set; }
    }

}
