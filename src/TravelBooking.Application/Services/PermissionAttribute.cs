using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TravelBooking.Application.Constants;

namespace TravelBooking.Application.Services
{
    /// <summary>
    /// Attribute for enforcing permission-based authorization in controllers or actions.
    /// </summary>
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// The required permission for the resource.
        /// </summary>
        private readonly string _permission;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionAttribute"/> class.
        /// </summary>
        /// <param name="permission">The specific permission required to access the resource.</param>
        public PermissionAttribute(string permission)
        {
            _permission = permission;
        }

        /// <summary>
        /// Performs authorization based on the user's role and the required permission.
        /// </summary>
        /// <param name="context">The context for authorization, which includes the HTTP context and user claims.</param>
        /// <remarks>
        /// This method checks the user's role from the JWT token claims and verifies if the required permission is granted.
        /// If the user does not have the required permission, the request is blocked with a 403 Forbidden response.
        /// </remarks>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the user's role from the JWT token claims.
            var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            // Validate the user's permission.
            if (!RolePermissions.HasPermission(role, _permission))
            {
                context.Result = new ObjectResult(new { Message = "You do not have permission to perform this action." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }

}
