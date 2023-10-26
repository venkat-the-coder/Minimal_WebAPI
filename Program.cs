using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minimal_WebAPI.EntityLayer;
using Minimal_WebAPI.Repository;
using System.Reflection.Metadata.Ecma335;
using Minimal_WebAPI.Interfaces;

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
            //swagger Ui Suppport
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //creating a application object from builder.build()
            var app = builder.Build();

            //configuring the Http Request pipeline with middlewares
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //API Routes
            app.MapGet("api/HelloWorld", () => "welcome to hello world!!").WithTags("Sample");
            app.MapGet("api/Hello",() => Results.StatusCode(200)).WithTags("Sample");
            app.MapGet("api/NoContent",() => Results.NoContent()).WithTags("Sample");

            //API Routes with Params
            app.MapGet("api/Greet", (string name) => Results.Ok($"hello {name}")).WithTags("Sample");


            //API Routes fetching a data from repository method
            app.MapGet("api/Product",() => {
                List<Product> records = new ProductRepository().Get();
                if (records != null)
                {
                    return Results.Ok(records);
                }
                return Results.NotFound("No products Found");
            }).WithTags("productApi").Produces(200).Produces<List<Product>>().Produces(404);
            
            app.MapGet("/api/product/{id:int}", (int id) =>
            {
                Product record = new ProductRepository().GetById(id);
                if(record != null)
                {
                    return Results.Ok(record);
                }
                return Results.NotFound();
            }).WithTags("productApi").Produces(200).Produces<Product>().Produces(404);
             
            //Customer Routes
            app.MapGet("api/customer", () =>
            {
                List<Customer> customers = new CustomerRepository().Get();
                if (customers != null)
                {
                    return Results.Ok(customers);
                }
                return Results.NotFound();
            }).WithTags("CustomerApi").Produces(200).Produces<List<Customer>>();




            //with dependency injection

            app.MapGet("api/aAddWorksAPIDefaults", (AddWorksAPIDefaults settings) =>
               {
                   return Results.Ok(settings);

               }).WithTags("AddWorksAPIDefaults");


            app.MapGet("api/Product/Get", (IRepository<Product> productRepository) => {
                List<Product> records = productRepository.Get();
                if (records != null)
                {
                    return Results.Ok(records);
                }
                return Results.NotFound("No products Found");

            }).WithTags("productApiDI").Produces(200).Produces<List<Product>>().Produces(404);


            app.MapGet("api/customer/Get", (IRepository<Customer> customerRepository) =>
            {
                List<Customer> customers = customerRepository.Get();
                if (customers != null)
                {
                    return Results.Ok(customers);
                }
                return Results.NotFound();
            }).WithTags("CustomerApiDI").Produces(200).Produces<List<Customer>>();

            //Running the application
            app.Run();



            //Additional Data and Methods
        }
    }
}