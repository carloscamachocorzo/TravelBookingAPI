using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBooking.Application.Dtos.Hotels
{
    public class HotelDto
    {
        public int Id { get; set; }  // El Id del hotel, generalmente es autogenerado.
        public string Name { get; set; }  // Nombre del hotel.
        public string Address { get; set; }  // Dirección del hotel.
        public string City { get; set; }  // Ciudad donde está el hotel.
    }
}
