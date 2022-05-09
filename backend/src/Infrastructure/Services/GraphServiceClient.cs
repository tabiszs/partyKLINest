using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.Infrastructure.Services
{
    internal class GraphServiceClient
    {
        public void RegisterService()
        {
            services.AddSingleton<IAuthenticationProvider>(o =>
            {
                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                     .Create(Configuration.GetSection("AzureB2C")["ClientId"])
                     .WithTenantId(Configuration.GetSection("AzureB2C")["TenantId"])
                     .WithClientSecret(Configuration[Configuration.GetSection("AzureB2C")["Secret"]])
                     .Build();

                return new ClientCredentialProvider(confidentialClientApplication);
            });
            services.AddSingleton(conf =>
                new GraphServiceClient(conf.GetService<IAuthenticationProvider>()));
        }
    }
}
