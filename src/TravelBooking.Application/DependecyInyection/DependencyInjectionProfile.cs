using Microsoft.Extensions.DependencyInjection;
using TravelBooking.Application.Automapper;
using TravelBooking.Application.Services;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Domain.Services;
using TravelBooking.Domain.Services.Interfaces;
using TravelBooking.Infraestructure.Repositories;
using TravelBooking.Infraestructure.Services;

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
            //Domain
            services.AddScoped<IReservationNotifierService, ReservationNotifierService>();
            // Registrar el servicio de correo
            services.AddTransient<IEmailService, EmailService>();
            // Registrar el AutoMapper
            services.AddAutoMapper(typeof(GlobalMapperProfile));

            return services;
        }
    }
}
