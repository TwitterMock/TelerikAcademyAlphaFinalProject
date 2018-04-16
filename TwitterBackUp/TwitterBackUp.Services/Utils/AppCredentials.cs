using Microsoft.Extensions.Configuration;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Utils
{
    public class AppCredentials : IAppCredentials
    {
        private readonly IConfiguration configuration;

        public AppCredentials(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ConsumerKey => configuration.GetSection("AppCredentials")["ConsumerKey"];
        public string ConsumerSecret => configuration.GetSection("AppCredentials")["ConsumerSecret"];
        public string BearerToken { get; set; }
    }
}