using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankRecordAPIClient
{
    public static class BankRecordConfiguration
    {
        public static void AddCashBankConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BankRecordOptions>(options =>
            {
                options.BaseAddress = configuration["BankRecordAPIClient:BaseAddress"];
                options.EndPoint = configuration["BankRecordAPIClient:EndPoint"];
            });
            services.AddHttpClient<IBankRecordClient, BankRecordClient>();

        }
    }
}