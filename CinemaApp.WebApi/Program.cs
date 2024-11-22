namespace CinemaApp.WebApi
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Services.Data.Interfaces;
    using Services.Mapping;
    using Web.Infrastructure.Extensions;
    using Web.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer")!;
            string? cinemaWebAppOrigin = builder.Configuration.GetValue<string>("Client Origins:CinemaWebApp");

            // Add services to the container.
            builder.Services
                .AddDbContext<CinemaDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(cfg =>
            {
                cfg.AddPolicy("AllowAll", policyBld =>
                {
                    policyBld
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });

                if (!String.IsNullOrWhiteSpace(cinemaWebAppOrigin))
                {
                    cfg.AddPolicy("AllowMyServer", policyBld =>
                    {
                        policyBld
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithOrigins(cinemaWebAppOrigin);
                    });
                }
            });

            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServicesWebApi(typeof(IMovieService).Assembly);

            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            if (!String.IsNullOrWhiteSpace(cinemaWebAppOrigin))
            {
                app.UseCors("AllowMyServer");
            }

            app.MapControllers();

            app.Run();
        }
    }
}
