namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to update the status of a hotel.
    /// </summary>
    public class UpdateHotelStatusDto
    {
        /// <summary>
        /// The updated status of the hotel. Indicates whether the hotel is active or inactive.
        /// </summary>
        public bool Status { get; set; }
    }

}
