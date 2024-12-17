
using Microsoft.EntityFrameworkCore;
using TravelBooking.Application.DependecyInyection;
using TravelBooking.Infraestructure.DataAccess.Contexts;
using TravelBooking.Infraestructure.Services;

namespace TravelBooking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Configurar logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.AddFile("Logs/TravelBooking-{Date}.txt");
            });
            // Configura la cadena de conexión
            builder.Services.AddDbContext<TravelBookingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            // Agregar servicios de la capa de Application
            builder.Services.AddApplicationServices();
            // Cargar configuración de SMTP desde appsettings.json
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
