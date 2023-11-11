using Minimal_WebAPI.BaseRouter;

namespace Minimal_WebAPI.RouterClasses
{
    public class SimpleRouterClass : BaseRouterClass
    {
        public SimpleRouterClass()
        {

            UrlFragment = "api/Simple";
            TagName = "Simple";
        }

        public override void AddRoutes(WebApplication app)
        {
            //API Simple Routes
            app.MapGet($"{UrlFragment}/HelloWorld", () => "welcome to hello world!!").WithTags(TagName);
            app.MapGet($"{UrlFragment}/Hello", () => Results.StatusCode(200)).WithTags(TagName);
            app.MapGet($"{UrlFragment}/NoContent", () => Results.NoContent()).WithTags(TagName);
            app.MapGet($"{UrlFragment}/Greet", (string name) => Results.Ok($"hello {name}")).WithTags(TagName);
        }
    }
}
