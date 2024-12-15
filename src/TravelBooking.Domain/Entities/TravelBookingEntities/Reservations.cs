using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class Reservations
{
    public int ReservationId { get; set; }

    public int RoomId { get; set; }

    public int UserId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public int TotalGuests { get; set; }

    public DateTime ReservationDate { get; set; }

    public decimal TotalCost { get; set; }

    public virtual ICollection<EmergencyContacts> EmergencyContacts { get; set; } = new List<EmergencyContacts>();

    public virtual ICollection<Guests> Guests { get; set; } = new List<Guests>();

    public virtual Rooms Room { get; set; }

    public virtual Users User { get; set; }
}
