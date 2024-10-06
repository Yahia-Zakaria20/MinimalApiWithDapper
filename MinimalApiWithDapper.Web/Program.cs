
using Microsoft.EntityFrameworkCore;
using MinimalApiWithDapper.Web.Data;

namespace MinimalApiWithDapper.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            webApplicationBuilder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddDbContext<ApplicationsDbcontext>(options =>
          options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefultConnection")));

            var app = webApplicationBuilder.Build();

            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<ApplicationsDbcontext>();
            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                dbcontext.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();

                logger.LogError(string.Empty, ex.Message);
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.Run();
        }
    }
}
