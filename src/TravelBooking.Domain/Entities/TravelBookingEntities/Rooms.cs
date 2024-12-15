using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class Rooms
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string Number { get; set; }

    public string Type { get; set; }

    public string Location { get; set; }

    public decimal BaseCost { get; set; }

    public decimal Tax { get; set; }

    public bool Status { get; set; }

    public virtual Hotels Hotel { get; set; }

    public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
}
