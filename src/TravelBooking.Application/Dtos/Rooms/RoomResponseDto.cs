namespace TravelBooking.Application.Dtos.Rooms
{
    public class RoomResponseDto
    {
        public string RoomName { get; set; }        
        public string HotelName { get; set; }
        public string RoomType { get; set; }  // Puede ser un tipo de habitación, por ejemplo, "Standard", "Deluxe"
        public int Capacity { get; set; }     // Número de personas que pueden alojarse
        public decimal Rate { get; set; }     // Tarifa de la habitación
        public bool IsAvailable { get; set; } // Disponibilidad de la habitación
    }
}
