namespace TravelBooking.Application.Dtos
{
    public class CreateRoomDto
    {

        public string Number { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public decimal BaseCost { get; set; }

        public decimal Tax { get; set; }

        public bool Status { get; set; }
    }
}
