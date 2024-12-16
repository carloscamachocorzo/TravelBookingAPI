namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to update an existing hotel.
    /// </summary>
    public class UpdateHotelDto
    {
        /// <summary>
        /// The updated name of the hotel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The updated address of the hotel.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The updated city where the hotel is located.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The updated status of the hotel. Indicates whether the hotel is active or inactive.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The updated base rate of the hotel, excluding taxes.
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The updated tax applied to the base rate.
        /// </summary>
        public decimal Tax { get; set; }
    }

}
