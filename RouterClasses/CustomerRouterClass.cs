using Minimal_WebAPI.BaseRouter;
using Minimal_WebAPI.EntityLayer;
using Minimal_WebAPI.Interfaces;
using Minimal_WebAPI.Repository;

namespace Minimal_WebAPI.RouterClasses
{
    public class CustomerRouterClass : BaseRouterClass
    {
        private readonly IRepository<Customer> _repository;

        public CustomerRouterClass(IRepository<Customer> repository)
        {
            UrlFragment = "api/customer";
            TagName = "CustomerAPI-DI";
            _repository = repository;
        }

        public override void AddRoutes(WebApplication app)
        {
            //Customer Routes without DI
            app.MapGet($"{UrlFragment}/GetCustomers", () =>
            {
                List<Customer> customers = new CustomerRepository().Get();
                if (customers != null)
                {
                    return Results.Ok(customers);
                }
                return Results.NotFound();
            }).WithTags(TagName).Produces(200).Produces<List<Customer>>();

            //Customer Routes with DI
            app.MapGet($"{UrlFragment}/GetCustomerById", (int? Id) =>
            {
                if(Id == null)
                {
                    return Results.BadRequest();
                }
                Customer customer = _repository.GetById(Id);
                if (customer != null)
                {
                    return Results.Ok(customer);
                }
                return Results.NotFound();
            }).WithTags(TagName).Produces(200).Produces<List<Customer>>();
        }
    }
}
