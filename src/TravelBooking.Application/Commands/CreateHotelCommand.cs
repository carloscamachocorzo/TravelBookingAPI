using MediatR;

namespace TravelBooking.Application.Commands
{
    public class CreateHotelCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public bool Status { get; set; }

        public decimal BaseRate { get; set; }

        public decimal Tax { get; set; }
        //public List<CreateRoomCommand> Rooms { get; set; } = new List<CreateRoomCommand>();
    }
}
