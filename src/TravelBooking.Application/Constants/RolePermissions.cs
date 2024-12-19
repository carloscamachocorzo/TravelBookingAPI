namespace TravelBooking.Application.Constants
{
    /// <summary>
    /// Provides role-based permissions management.
    /// </summary>
    public static class RolePermissions
    {
        /// <summary>
        /// Dictionary containing predefined permissions for each user role.
        /// </summary>
        /// <remarks>
        /// Each key in the dictionary is a user role, and the value is a list of permissions associated with that role.
        /// </remarks>
        public static readonly Dictionary<string, List<string>> Permissions = new Dictionary<string, List<string>>
        {
            { UserRoles.Admin, new List<string>
                {
                    "CreateHotel",
                    "AssignRoomsToHotel",
                    "UpdateHotel",
                    "UpdateHotelStatus",
                    "GetAllHotels",
                    "UpdateRoom",
                    "UpdateRoomStatus",
                    "CreateUser",
                    "UpdateUser",
                    "GetAllUsers",
                    "GetAllReservations",
                    "SearchHotels"
                }
            },
            { UserRoles.TravelAgent, new List<string>
                {
                    "SearchHotels",
                    "CreateReservation",
                    "GetReservationById",
                    "NotifyReservation"
                }
            },
            { UserRoles.Traveler, new List<string>
                {
                    "SearchHotels",
                    "CreateReservation",
                    "GetReservationById"
                }
            }
        };

        /// <summary>
        /// Determines whether a given role has a specific permission.
        /// </summary>
        /// <param name="role">The role to check (e.g., Admin, TravelAgent, Traveler).</param>
        /// <param name="permission">The permission to verify.</param>
        /// <returns>
        /// <c>true</c> if the role has the specified permission; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// Example usage:
        /// <code>
        /// bool canManageUsers = RolePermissions.HasPermission(UserRoles.Admin, "ManageUsers");
        /// </code>
        /// </example>
        public static bool HasPermission(string role, string permission)
        {
            return Permissions.ContainsKey(role) && Permissions[role].Contains(permission);
        }
    }


}
