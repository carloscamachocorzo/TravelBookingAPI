using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class Hotels
{
    public int HotelId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public bool Status { get; set; }

    public decimal BaseRate { get; set; }

    public decimal Tax { get; set; }

    public int MaxCapacity { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Rooms> Rooms { get; set; } = new List<Rooms>();
}
