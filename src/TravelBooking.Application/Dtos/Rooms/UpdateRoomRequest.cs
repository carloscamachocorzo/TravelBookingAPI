namespace TravelBooking.Application.Dtos.Rooms
{
    public class UpdateRoomRequest
    {
        public int RoomId { get; set; }
        public string Number { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public decimal BaseCost { get; set; }

        public decimal Tax { get; set; }

        public bool Status { get; set; }
    }
}
