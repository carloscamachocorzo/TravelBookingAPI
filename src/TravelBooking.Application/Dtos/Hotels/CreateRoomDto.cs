namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to create a new room.
    /// </summary>
    using System.ComponentModel.DataAnnotations;

    public class CreateRoomDto
    {
        /// <summary>
        /// The unique number identifying the room.
        /// </summary>
        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(10, ErrorMessage = "Room number cannot exceed 10 characters.")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// The type of the room (e.g., Single, Double, Suite, Family).
        /// </summary>
        [Required(ErrorMessage = "Room type is required.")]
        [StringLength(20, ErrorMessage = "Room type cannot exceed 20 characters.")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The location of the room within the hotel.
        /// </summary>
        [Required(ErrorMessage = "Room location is required.")]
        [StringLength(50, ErrorMessage = "Room location cannot exceed 50 characters.")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// The base cost of the room, excluding taxes.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Base rate must be greater than 0.")]
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax applied to the base cost of the room.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Tax must be between 0 and 100 percent.")]
        public decimal Tax { get; set; }

        /// <summary>
        /// Indicates whether the room is available or not.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of guests that the room can accommodate.
        /// </summary>
        [Range(1, 20, ErrorMessage = "Max capacity must be between 1 and 20.")]
        public int MaxCapacity { get; set; }
    }


}
