using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class EmergencyContacts
{
    public int EmergencyContactId { get; set; }

    public int ReservationId { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public virtual Reservations Reservation { get; set; }
}
