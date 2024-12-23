using Swashbuckle.AspNetCore.Filters;
using TravelBooking.Application.Dtos.Hotels;

namespace TravelBooking.API.Examples
{
    public class CreateHotelsRequestDtoExample : IExamplesProvider<CreateHotelRequestsDto>
    {
        public CreateHotelRequestsDto GetExamples()
        {
            return new CreateHotelRequestsDto
            {
                Name = "hotel name",
                Address = "street 123",
                BaseRate = 100000m,
                City = "Bucaramanga",
                MaxCapacity = 120,
                Status = true,
                Tax = 0.10m
            };
        }
    }
}
