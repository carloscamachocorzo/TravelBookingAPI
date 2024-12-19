using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Rooms;

namespace TravelBooking.Application.Services.Interfaces
{
    public interface IRoomAppService
    {
        /// <summary>
        /// Executes the update operation for a room based on the provided request.
        /// </summary>
        /// <param name="request">The room update request containing the details to be updated.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result contains a <see cref="RequestResult{T}"/> indicating whether the operation was successful.</returns>

        Task<RequestResult<bool>> ExecuteUpdateRoomAsync(int roomId, UpdateRoomRequest request);
        // <summary>
        /// Updates the status of a room.
        /// </summary>
        /// <param name="roomId">The identifier of the room whose status is to be updated.</param>
        /// <param name="statusDto">The data transfer object containing the new status for the room.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result contains a <see cref="RequestResult{T}"/> indicating whether the operation was successful.</returns>

        Task<RequestResult<bool>> UpdateRoomStatusAsync(int roomId, UpdateRoomStatusDto statusDto);
        // <summary>
        /// Retrieves the details of a room by its identifier.
        /// </summary>
        /// <param name="roomId">The identifier of the room to retrieve details for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result contains a <see cref="RequestResult{T}"/> with a <see cref="RoomResponseDto"/> that holds the room details.</returns>

        Task<RequestResult<RoomResponseDto>> GetRoomDetailsAsync(int roomId);
    }
}
