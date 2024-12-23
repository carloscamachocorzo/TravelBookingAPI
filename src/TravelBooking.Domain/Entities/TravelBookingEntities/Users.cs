﻿using System;
using System.Collections.Generic;

namespace TravelBooking.Infraestructure;

public partial class Users
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Role { get; set; }

    public bool Status { get; set; }

    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
}
