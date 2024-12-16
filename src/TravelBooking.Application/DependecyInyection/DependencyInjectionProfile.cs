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
            services.AddScoped<ICreateHotelCommandHandler, CreateHotelCommandHandler>();

            //Domain
            services.AddScoped<IHotelRepository, HotelRepository>();
            // Configura los servicios de MediatR (si no lo has hecho antes)
            //services.AddMediatR(typeof(Program).Assembly);
            return services;
        }
    }
}
