using Microsoft.Extensions.DependencyInjection;
using Rocksoft.Translator.Integrations.AzureAiTranslator;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Configuration;
using Rocksoft.Translator.Services;

namespace Rocksoft.Translator.Tests.Helpers;

public static class Services
{
    public static IServiceCollection CreateServiceCollection()
    {
        var config = Configuration.GetApplicationConfiguration();
        
        IServiceCollection services = new ServiceCollection();

        services.AddAzureAiTranslator(new AzureAiTranslatorConfiguration()
        {
            SubscriptionKey = config.SubscriptionKey,
            SubscriptionRegion = config.SubscriptionRegion
        });

        services.AddTransient<RocksoftAzureTranslator>();

        return services;
    }
}