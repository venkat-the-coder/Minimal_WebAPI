using Minimal_WebAPI.BaseRouter;
using Minimal_WebAPI.EntityLayer;
using Minimal_WebAPI.Interfaces;
using Minimal_WebAPI.Repository;

namespace Minimal_WebAPI.RouterClasses
{
    public class ProductRouterClass : BaseRouterClass
    {
        private readonly IRepository<Product> _repository;

        public ProductRouterClass(IRepository<Product> repository)
        {
            UrlFragment = "api/Product";
            TagName = "ProductAPI";
            _repository = repository;
        }
        public override void AddRoutes(WebApplication app)
        {
            app.MapGet($"/{UrlFragment}", () => Get()
                ).WithTags(TagName).Produces(200).Produces<List<Product>>().Produces(404);

            app.MapGet($"/{UrlFragment}/{{id:int}}", (int id) => GetById(id)
            ).WithTags(TagName).Produces(200).Produces<Product>().Produces(404);
        }

        protected virtual IResult Get()
        {
            List<Product> records = _repository.Get();

            if (records != null)
            {
                return Results.Ok(records);
            }

            return Results.NotFound("No products Found");
        }


        protected virtual IResult GetById(int id)
        {
            Product record = _repository.GetById(id);
            if (record != null)
            {
                return Results.Ok(record);
            }
            return Results.NotFound();
        }
    }
}
