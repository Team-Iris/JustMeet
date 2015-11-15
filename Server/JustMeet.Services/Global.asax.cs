namespace JustMeet.Services
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load("JustMeet.Services"));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
