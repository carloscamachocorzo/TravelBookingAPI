namespace TravelBooking.Application.Services.Interfaces
{
    public interface IAuthAppService
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
