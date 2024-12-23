using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to create a new hotel.
    /// </summary>
    public class CreateHotelRequestsDto
    {
        /// <summary>
        /// The name of the hotel.
        /// </summary>
        [Required(ErrorMessage = "The name is required.")]
        [MinLength(3, ErrorMessage = "The name must be at least 3 characters long.")]
        public required string Name { get; set; }

        /// <summary>
        /// The physical address of the hotel.
        /// </summary>
        [Required(ErrorMessage = "The address is required.")]
        [MinLength(5, ErrorMessage = "The address must be at least 5 characters long.")]
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
        [Range(1, double.MaxValue, ErrorMessage = "The base rate must be greater than 0.")]
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax applied to the base rate.
        /// </summary>
        [Range(0, 100, ErrorMessage = "The tax must be between 0 and 100.")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Represents the maximum capacity of the room or hotel.
        /// </summary>
        /// <value>
        /// The maximum number of guests the room or hotel can accommodate. This value can be null if not specified.
        /// </value>
        [Range(1, int.MaxValue, ErrorMessage = "The max capacity must be at least 1.")]
        public int? MaxCapacity { get; set; }
    }

}
