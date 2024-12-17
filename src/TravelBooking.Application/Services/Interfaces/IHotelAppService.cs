using TravelBooking.Application.Common;
using TravelBooking.Application.Dtos.Hotels;

namespace TravelBooking.Application.Services.Interfaces
{

    /// <summary>  
    /// Defines the operations available for managing hotels.
    /// </summary>
    public interface IHotelAppService
    {
        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="request">The details of the hotel to be created.</param>
        /// <returns>The result containing the details of the created hotel.</returns>
        Task<RequestResult<CreateHotelResponseDto>> CreateHotel(CreateHotelDto request);

        /// <summary>
        /// Assigns rooms to an existing hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel.</param>
        /// <param name="request">The list of rooms to be assigned to the hotel.</param>
        /// <returns>The result indicating whether the operation was successful.</returns>
        Task<RequestResult<bool>> AssignRoomsToHotel(int hotelId, CreateRoomsRequest request);

        /// <summary>
        /// Updates the details of an existing hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel to be updated.</param>
        /// <param name="updateHotelDto">The updated details of the hotel.</param>
        /// <returns>The result indicating whether the operation was successful.</returns>
        Task<RequestResult<bool>> UpdateHotelAsync(int hotelId, UpdateHotelDto updateHotelDto);

        /// <summary>
        /// Updates the status of an existing hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel.</param>
        /// <param name="status">The new status of the hotel (active or inactive).</param>
        /// <returns>The result indicating whether the operation was successful.</returns>
        Task<RequestResult<bool>> UpdateHotelStatusAsync(int hotelId, bool status);

        /// <summary>
        /// Retrieves all hotels.
        /// </summary>
        /// <returns>A list of all existing hotels.</returns>
        Task<RequestResult<List<CreateHotelResponseDto>>> GetAllHotelsAsync();
        /// <summary>
        /// Searches for hotels based on the provided search criteria.
        /// </summary>
        /// <param name="searchHotelsDto">The DTO containing the search criteria, such as location, price range, etc.</param>
        /// <returns>A <see cref="RequestResult{List{SearchHotelResponseDto}}"/> containing the list of hotels that match the search criteria.</returns>

        Task<RequestResult<List<SearchHotelResponseDto>>> SearchHotelsAsync(SearchHotelsDto searchHotelsDto);
    }

}
