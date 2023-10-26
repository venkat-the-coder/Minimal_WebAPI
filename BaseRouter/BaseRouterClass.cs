namespace Minimal_WebAPI.BaseRouter
{
    public abstract class BaseRouterClass
    {
        public BaseRouterClass()
        {
            UrlFragment = string.Empty;
            TagName = string.Empty; 
        }

        public string UrlFragment { get; set; }
        public string TagName { get; set; }


        public abstract void AddRoutes(WebApplication app);
    }
}
