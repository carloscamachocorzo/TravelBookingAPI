namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents a request to create multiple rooms.
    /// </summary>
    public class CreateRoomsRequest
    {
        /// <summary>
        /// A list of rooms to be created.
        /// </summary>
        public List<CreateRoomDto> Rooms { get; set; }
    }

}
