
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Rooms;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface IRoomAppService
    {
        Task<RequestResult<bool>> ExecuteUpdateRoomAsync(UpdateRoomRequest request);
    }
}
