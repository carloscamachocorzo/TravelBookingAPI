﻿namespace TravelBooking.Application.Dtos.Hotels
{
    /// <summary>
    /// Represents the data required to search for hotels.
    /// </summary>
    using System.ComponentModel.DataAnnotations;

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
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int? NumberOfGuests { get; set; }

        /// <summary>
        /// The destination city for the hotel search.
        /// </summary>
        [Required(ErrorMessage = "Destination city is required.")]
        [StringLength(50, ErrorMessage = "Destination city cannot exceed 50 characters.")]
        public string DestinationCity { get; set; }
    }


}
