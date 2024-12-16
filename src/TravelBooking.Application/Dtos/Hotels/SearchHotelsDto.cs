namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to search for hotels.
    /// </summary>
    public class SearchHotelsDto
    {
        /// <summary>
        /// The check-in date for the hotel search. Optional.
        /// </summary>
        public DateOnly? CheckInDate { get; set; }

        /// <summary>
        /// The check-out date for the hotel search. Optional.
        /// </summary>
        public DateOnly? CheckOutDate { get; set; }

        /// <summary>
        /// The number of guests for the hotel booking. Optional.
        /// </summary>
        public int? NumberOfGuests { get; set; }

        /// <summary>
        /// The destination city for the hotel search.
        /// </summary>
        public string DestinationCity { get; set; }
    }

}
