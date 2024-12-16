namespace TravelBooking.Application.Dtos.Reservation
{
    public class ReservationDetailsDto
    {
        public int ReservationId { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string UserName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalCost { get; set; }
    }
}
