namespace TravelBooking.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password);
    }
}
