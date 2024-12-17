using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Rooms;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure;

namespace TravelBooking.Application.Services
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomAppService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<RequestResult<bool>> ExecuteUpdateRoomAsync(UpdateRoomRequest request)
        {
            try
            {
                // Validar si la habitación existe
                var room = await _roomRepository.GetByIdAsync(request.RoomId);
                if (room == null)
                    throw new KeyNotFoundException($"No se encontró una habitación con ID {request.RoomId}");

                // Actualizar la habitación
                UpdateRoom(request, room);

                // Guardar los cambios
                await _roomRepository.UpdateAsync(room);
                return RequestResult<bool>.CreateSuccessful(true, new List<string> { "Room assignment done successfully" });

            }
            catch (Exception ex)
            {
                // Si ocurrió un error
                return RequestResult<bool>.CreateError("Error al crear el hotel: " + ex.Message);
            }
        }
        public async Task<RequestResult<bool>> UpdateRoomStatusAsync(int roomId, UpdateRoomStatusDto statusDto)
        {
            try
            {
                var room = await _roomRepository.GetByIdAsync(roomId);

                if (room == null)
                {
                    return RequestResult<bool>.CreateError($"Room with ID {roomId} not found.");
                }

                // Actualizar el estado de la habitación
                room.Status = statusDto.IsEnabled;

                // Guardar cambios en el repositorio
                await _roomRepository.UpdateAsync(room);

                return RequestResult<bool>.CreateSuccessful(true, new List<string> { "Room status updated successfully." });
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError($"An error occurred while updating the room status: {ex.Message}");
            }
        }
        public async Task<RequestResult<RoomResponseDto>> GetRoomDetailsAsync(int roomId)
        {
            try
            {
                // Obtener la habitación del repositorio
                var room = await _roomRepository.GetRoomByIdAsync(roomId);

                if (room == null)
                {
                    return RequestResult<RoomResponseDto>.CreateUnsuccessful(new List<string> { "Room not found" });
                }

                // Mapear la habitación a un DTO
                var roomDto = _mapper.Map<RoomResponseDto>(room);

                return RequestResult<RoomResponseDto>.CreateSuccessful(roomDto);
            }
            catch (Exception ex)
            {
                return RequestResult<RoomResponseDto>.CreateError($"Error retrieving room details: {ex.Message}");
            }
        }
        private void UpdateRoom(UpdateRoomRequest updateRoomRequest, Rooms rooms)
        {
            rooms.Number = updateRoomRequest.Number;
            rooms.Type = updateRoomRequest.Type;
            rooms.Location = updateRoomRequest.Location;
            rooms.BaseCost = updateRoomRequest.BaseCost;
            rooms.Tax = updateRoomRequest.Tax;
            rooms.Status = updateRoomRequest.Status;
        }
    }
}
