using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OstClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            string origins = ConfigurationManager.AppSettings["cors:Origins"];
            string headers = ConfigurationManager.AppSettings["cors:Headers"];
            string methods = ConfigurationManager.AppSettings["cors:Methods"];

            if (!string.IsNullOrEmpty(origins))
            {
                if (string.IsNullOrEmpty(headers)) headers = "*";
                if (string.IsNullOrEmpty(methods)) methods = "*";
                var cors = new EnableCorsAttribute(origins, headers, methods);
                config.EnableCors(cors);
            }

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
