using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using MarriageAgency.Models;
using MarriageAgency.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;

namespace MarriageAgency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Запись действий в журнал с использованием пакета Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("MarriageAgencyWebApiLog-.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<MarriageAgencyContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MarriageAgency API",
                    Description = "Данные об брачном агенстве",
                    //TermsOfService = new Uri("https://go.microsoft.com/fwlink/?LinkID=206977"),
                    Contact = new OpenApiContact
                    {
                        Name = "Konstantin",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/kevenmusic/RPBDISLabs")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Настройка промежуточного ПО для Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarriageAgency API v1");
       
                });
            }

            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();



            //Инициализация базы данных
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    MarriageAgencyContext context = serviceProvider.GetRequiredService<MarriageAgencyContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while db initialization");
                }
            }

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
