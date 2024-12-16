namespace TravelBooking.Application.Dtos.Hotels
{
    public class SearchHotelResponseDto
    {
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal BaseRate { get; set; }
        public decimal Tax { get; set; }
    }
}
