using Microsoft.Extensions.DependencyInjection;
using TravelBooking.Application.Automapper;
using TravelBooking.Application.Services;
using TravelBooking.Application.Services.Interfaces;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Domain.Services;
using TravelBooking.Domain.Services.Interfaces;
using TravelBooking.Infraestructure.Repositories;
using TravelBooking.Infraestructure.Security;
using TravelBooking.Infraestructure.Services;

namespace TravelBooking.Application.DependecyInyection
{
    public static class DependencyInjectionProfile
    {
        /// <summary>
        /// Registers application services, repositories, domain services, and other dependencies into the service container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped<IHotelAppService, HotelAppService>(); // Hotel service
            services.AddScoped<IRoomAppService, RoomAppService>(); // Room service
            services.AddScoped<IReservationsAppService, ReservationsAppService>(); // Reservations service
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IAuthAppService, AuthAppService>();
            // Repositories
            services.AddScoped<IHotelRepository, HotelRepository>(); // Hotel repository
            services.AddScoped<IRoomRepository, RoomRepository>(); // Room repository
            services.AddScoped<IReservationsRepository, ReservationsRepository>(); // Reservations repository
            services.AddScoped<IUserRepository, UserRepository>(); // User repository

            // Domain Services
            services.AddScoped<IReservationNotifierService, ReservationNotifierService>(); // Reservation notifier service
            services.AddScoped<IJwtService, JwtService>();
            // Register email service
            services.AddTransient<IEmailService, EmailService>(); // Email service

            // Register AutoMapper
            services.AddAutoMapper(typeof(GlobalMapperProfile)); // AutoMapper configuration

            return services;
        }
    }

}
