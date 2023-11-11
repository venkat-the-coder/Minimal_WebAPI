using Microsoft.AspNetCore.Http.HttpResults;
using Minimal_WebAPI.BaseRouter;
using Minimal_WebAPI.EntityLayer;
using System.Text.Json;

namespace Minimal_WebAPI.RouterClasses
{
    public class LogTestRouter : BaseRouterClass
    {
        private readonly ILogger<LogTestRouter> _logger;

        public LogTestRouter(ILogger<LogTestRouter> logger)
        {
            UrlFragment = "api/LogTest";
            TagName = "Logtest";
            _logger = logger;
        }
        public override void AddRoutes(WebApplication app)
        {
            app.MapGet($"{UrlFragment}/WriteLogs", () => WriteLogMessage())
                .WithTags(TagName);

            app.MapGet($"{UrlFragment}/LogObject", () => LogObjectMethod())
                .WithTags(TagName);
        }

        protected virtual IResult WriteLogMessage()
        {
            _logger.LogCritical("this is critica error");
            _logger.LogDebug("debugger");
            _logger.LogInformation("this is information");
            _logger.LogTrace("Trace level entry");
            return Results.Ok();
        }

        protected virtual IResult LogObjectMethod()
        {
            Product product = new Product()
            {
                ProductID = 999,
                Name = "Test",
                ProductNumber = "Test456",
                StandardCost = 5M,
                Color = "Black"
            };

            var json = JsonSerializer.Serialize(product);
            _logger.LogInformation($"Product = {json}");
            return Results.Ok("OBJECT LOGGED");
        }
    }
}
