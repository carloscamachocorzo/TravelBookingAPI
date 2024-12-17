namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to create a new hotel.
    /// </summary>
    public class CreateHotelDto
    {
        /// <summary>
        /// The name of the hotel.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The physical address of the hotel.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// The city where the hotel is located.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Indicates whether the hotel is active or inactive.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The base rate for the hotel, excluding taxes.
        /// </summary>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax applied to the base rate.
        /// </summary>
        public decimal Tax { get; set; }
        /// <summary>
        /// Represents the maximum capacity of the room or hotel.
        /// </summary>
        /// <value>
        /// The maximum number of guests the room or hotel can accommodate. This value can be null if not specified.
        /// </value>
        public int? MaxCapacity { get; set; }
    }

}
