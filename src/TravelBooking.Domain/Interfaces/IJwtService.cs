﻿namespace TravelBooking.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);
        (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password);
    }
}