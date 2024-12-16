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

            //Repositories
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            // Configura los servicios de MediatR (si no lo has hecho antes)
            //services.AddMediatR(typeof(Program).Assembly);
            return services;
        }
    }
}
