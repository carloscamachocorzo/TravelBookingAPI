using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Reservation
{
    /// <summary>
    /// Represents the data transfer object (DTO) for creating a reservation.
    /// </summary>
    public class CreateReservationDto
    {
        /// <summary>
        /// The ID of the room to be reserved.
        /// </summary>
        [Required]
        public int RoomId { get; set; }

        /// <summary>
        /// The ID of the user making the reservation.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The check-in date for the reservation.
        /// </summary>
        [Required]
        public DateOnly CheckInDate { get; set; }

        /// <summary>
        /// The check-out date for the reservation.
        /// </summary>
        [Required]
        public DateOnly CheckOutDate { get; set; }

        /// <summary>
        /// The total number of guests in the reservation. Must be at least 1.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total guests must be at least 1.")]
        public int TotalGuests { get; set; }

        /// <summary>
        /// A list of emergency contacts associated with the reservation.
        /// </summary>
        public List<EmergencyContactDto>? EmergencyContacts { get; set; } = new List<EmergencyContactDto>();

        /// <summary>
        /// A list of guests associated with the reservation.
        /// </summary>
        public List<GuestDto>? Guests { get; set; } = new List<GuestDto>();
    }

    /// <summary>
    /// Represents an emergency contact associated with a reservation.
    /// </summary>
    public class EmergencyContactDto
    {
        /// <summary>
        /// The name of the emergency contact.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The phone number of the emergency contact.
        /// </summary>
        [Required]
        [Phone]
        public string Phone { get; set; }
    }


    /// <summary>
    /// Represents a guest associated with the reservation.
    /// </summary>
    public class GuestDto
    {
        /// <summary>
        /// The first name of the guest.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the guest.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The birth date of the guest.
        /// </summary>
        public DateOnly BirthDate { get; set; }

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// The type of document (e.g., passport, ID).
        /// </summary>
        [Required]
        public string DocumentType { get; set; }

        /// <summary>
        /// The document number (e.g., passport number, ID number).
        /// </summary>
        [Required]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// The email address of the guest.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the guest.
        /// </summary>
        public string PhoneNumber { get; set; }
    }

}
