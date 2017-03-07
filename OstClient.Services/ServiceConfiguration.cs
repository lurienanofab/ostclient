using System;
using System.Configuration;

namespace OstClient.Services
{
    public class ServiceConfiguration
    {
        public string ApiKey { get; set; }
        public Uri BaseAddress { get; set; }
        public string SmtpHost { get; set; }
        public string SystemEmail { get; set; }

        public static ServiceConfiguration Current
        {
            get
            {
                var apiKey = ConfigurationManager.AppSettings["ApiKey"];
                var baseAddr = ConfigurationManager.AppSettings["BaseAddress"];
                var smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                var systemEmail = ConfigurationManager.AppSettings["SystemEmail"];

                return new ServiceConfiguration()
                {
                    ApiKey = apiKey,
                    BaseAddress = new Uri(baseAddr),
                    SmtpHost = smtpHost,
                    SystemEmail = systemEmail
                };
            }
        }
    }
}
