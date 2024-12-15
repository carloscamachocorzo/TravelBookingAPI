using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class Guests
{
    public int GuestId { get; set; }

    public int ReservationId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; }

    public string DocumentType { get; set; }

    public string DocumentNumber { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public virtual Reservations Reservation { get; set; }
}
