using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Rooms;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface IRoomAppService
    {
        Task<RequestResult<bool>> ExecuteUpdateRoomAsync(UpdateRoomRequest request);
        Task<RequestResult<bool>> UpdateRoomStatusAsync(int roomId, UpdateRoomStatusDto statusDto);
        Task<RequestResult<RoomResponseDto>> GetRoomDetailsAsync(int roomId);
    }
}
