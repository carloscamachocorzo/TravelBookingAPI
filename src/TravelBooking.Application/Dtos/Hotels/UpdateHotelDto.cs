namespace TravelBooking.Application.Dtos.Hotels
{
    public class UpdateHotelDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public bool Status { get; set; }

        public decimal BaseRate { get; set; }

        public decimal Tax { get; set; }
    }
}
