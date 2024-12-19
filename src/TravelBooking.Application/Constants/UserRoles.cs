namespace TravelBooking.Application.Constants
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string TravelAgent = "TravelAgent";
        public const string Traveler = "Traveler";

        public static readonly List<string> AllRoles = new List<string>
        {
            Admin,
            TravelAgent,
            Traveler
        };
    }

}
