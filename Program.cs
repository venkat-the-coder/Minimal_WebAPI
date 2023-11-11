using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minimal_WebAPI.EntityLayer;
using Minimal_WebAPI.Repository;
using System.Reflection.Metadata.Ecma335;
using Minimal_WebAPI.Interfaces;
using Minimal_WebAPI.RouterClasses;
using Minimal_WebAPI.BaseRouter;
using Serilog;
namespace Minimal_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //create a web application builder
            var builder = WebApplication.CreateBuilder(args);

            //add and configure services with the builder
            builder.Services.AddScoped<AddWorksAPIDefaults, AddWorksAPIDefaults>();
            builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
            builder.Services.AddScoped<BaseRouterClass, ProductRouterClass>();
            builder.Services.AddScoped<BaseRouterClass, SimpleRouterClass>();
            builder.Services.AddScoped<BaseRouterClass, CustomerRouterClass>();
            builder.Services.AddScoped<BaseRouterClass, LogTestRouter>();

            //swagger Ui Suppport
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //serilog configuration

            builder.Host.UseSerilog((context, log) =>
            {
                log.WriteTo.Console();
                //log.WriteTo.File("Logs/Logs.text", rollingInterval: RollingInterval.Day);
            });

            //creating a application object from builder.build()
            var app = builder.Build();

            //configuring the Http Request pipeline with middlewares
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Minimal end point  to add route from all classes
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<BaseRouterClass>();
                foreach(var item in services)
                {
                    item.AddRoutes(app);
                }

                //Running the application
                app.Run();
            }

        }
    }
}