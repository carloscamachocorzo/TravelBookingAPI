namespace TravelBooking.Application.Dtos.Rooms
{
    /// <summary>
    /// Represents a request DTO to update the details of a room.
    /// </summary>
    using System.ComponentModel.DataAnnotations;

    public class UpdateRoomRequest
    {
        /// <summary>
        /// The number or name of the room (e.g., "101", "102").
        /// </summary>
        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(10, ErrorMessage = "Room number cannot exceed 10 characters.")]
        public string Number { get; set; }

        /// <summary>
        /// The type of the room (e.g., "Standard", "Deluxe", "Suite").
        /// </summary>
        [Required(ErrorMessage = "Room type is required.")]
        [StringLength(20, ErrorMessage = "Room type cannot exceed 20 characters.")]
        public string Type { get; set; }

        /// <summary>
        /// The location or floor of the room within the hotel (e.g., "3rd floor", "Near the pool").
        /// </summary>
        [Required(ErrorMessage = "Room location is required.")]
        [StringLength(50, ErrorMessage = "Room location cannot exceed 50 characters.")]
        public string Location { get; set; }

        /// <summary>
        /// The base cost per night for the room before tax.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Base rate must be greater than 0.")]
        public decimal BaseRate { get; set; }

        /// <summary>
        /// The tax rate applied to the room cost.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Tax must be between 0 and 100 percent.")]
        public decimal Tax { get; set; }

        /// <summary>
        /// The availability status of the room  
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of guests that the room can accommodate.
        /// </summary>
        [Range(1, 20, ErrorMessage = "Max capacity must be between 1 and 20.")]
        public int MaxCapacity { get; set; }
    }


}
