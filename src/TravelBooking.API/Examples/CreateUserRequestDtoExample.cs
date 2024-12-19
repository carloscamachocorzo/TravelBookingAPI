using Swashbuckle.AspNetCore.Filters;
using TravelBooking.Application.Dtos.Users;

namespace TravelBooking.API.Examples
{
    /// <summary>
    /// Provides example data for creating a user request.
    /// </summary>
    public class CreateUserRequestDtoExample : IExamplesProvider<CreateUserRequestsDto>
    {
        /// <summary>
        /// Generates an example instance of CreateUserRequestsDto.
        /// </summary>
        /// <returns>An example CreateUserRequestsDto object.</returns>
        public CreateUserRequestsDto GetExamples()
        {
            return new CreateUserRequestsDto
            {
                Username = "exampleUser",
                Password = "Example*123",  
                Role = "Admin",
                Email="correo@dominio.com",
                PhoneNumber="+57 300 000 0000"
                
            };
        }
    }

}
