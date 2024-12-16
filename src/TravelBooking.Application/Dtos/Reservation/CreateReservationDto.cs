using System.ComponentModel.DataAnnotations;

namespace TravelBooking.Application.Dtos.Reservation
{
    public class CreateReservationDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateOnly CheckInDate { get; set; }

        [Required]
        public DateOnly CheckOutDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total guests must be at least 1.")]
        public int TotalGuests { get; set; }

        public List<EmergencyContactDto>? EmergencyContacts { get; set; } = new List<EmergencyContactDto>();

        public List<GuestDto>? Guests { get; set; } = new List<GuestDto>();
    }
    public class EmergencyContactDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }

    public class GuestDto
    {
        [Required]     

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Gender { get; set; }
        [Required]
        public string DocumentType { get; set; }
        [Required]
        public string DocumentNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
