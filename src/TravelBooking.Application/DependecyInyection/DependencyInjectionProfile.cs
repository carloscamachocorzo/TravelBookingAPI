using Microsoft.Extensions.DependencyInjection;
using TravelBooking.Application.Services;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infraestructure.Repositories;

namespace TravelBooking.Application.DependecyInyection
{
    public static class DependencyInjectionProfile
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Application
            services.AddScoped<IHotelAppService, HotelAppService>();
            services.AddScoped<IRoomAppService, RoomAppService>();
            services.AddScoped<IReservationsAppService, ReservationsAppService>();

            //Repositories
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();
            
            return services;
        }
    }
}
