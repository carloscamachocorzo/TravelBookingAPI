using Swashbuckle.AspNetCore.Filters;
using TravelBooking.Application.Dtos.Hotels;

namespace TravelBooking.API.Examples
{
    /// <summary>
    /// Provides example data for creating a user request.
    /// </summary>
    public class CreateRoomsRequestDtoExample : IExamplesProvider<CreateRoomsRequest>
    {
        /// <summary>
        /// Generates an example instance of CreateUserRequestsDto.
        /// </summary>
        /// <returns>An example CreateUserRequestsDto object.</returns>
        public CreateRoomsRequest GetExamples()
        {
            return new CreateRoomsRequest
            {
                Rooms = new List<CreateRoomDto>
            {
                new CreateRoomDto
                {
                    Number = "101",
                    Type = "Suite",
                    Location = "Building A, Floor 1",
                    BaseRate = 200000,
                    Tax = 0.10m,  
                    Status = true,
                    MaxCapacity = 4
                },
                new CreateRoomDto
                {
                    Number = "102",
                    Type = "Double",
                    Location = "Building A, Floor 1",
                    BaseRate = 150000,
                    Tax = 0.10m,  
                    Status = false,
                    MaxCapacity = 2
                }
            }
            };
        }
    }

}
