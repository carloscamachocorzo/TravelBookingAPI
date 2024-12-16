namespace TravelBooking.Application.Dtos.Hotels
{
    public class SearchHotelsDto
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfGuests { get; set; }
        public string DestinationCity { get; set; }
    }
}
