
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelBooking.Application.DependecyInyection;
using TravelBooking.Infraestructure.DataAccess.Contexts;
using TravelBooking.Infraestructure.Services;

namespace TravelBooking.API
{
    public class Program
    {
        private const string _APINAME = "Hotel management";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Configurar logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.AddFile("Logs/travel-{Date}.txt");
            });
            Microsoft.Extensions.Logging.ILogger loggerData = loggerFactory.CreateLogger<Program>();
            // Configura la cadena de conexi�n
            builder.Services.AddDbContext<TravelBookingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            // Agregar servicios de la capa de Application
            builder.Services.AddApplicationServices();
            // Cargar configuraci�n de SMTP desde appsettings.json
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"ULTRA GROUP - {_APINAME}",
                    Version = "v1",
                    Description = "Hotel management services",
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Camacho Corzo"
                    }
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
