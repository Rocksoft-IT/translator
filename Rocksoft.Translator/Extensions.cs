using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Rocksoft.Translator.Abstraction;
using Rocksoft.Translator.Configuration;
using Rocksoft.Translator.Integrations.AzureAiTranslator;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Configuration;
using Rocksoft.Translator.Services;

[assembly: InternalsVisibleTo("Rocksoft.Translator.Tests")]
namespace Rocksoft.Translator;

public static class Extensions
{
    public static IServiceCollection AddRocksoftAzureTranslator(this IServiceCollection services, TranslatorConfiguration configuration)
    {
        services.AddAzureAiTranslator(
            new AzureAiTranslatorConfiguration
            {
                SubscriptionKey = configuration.SubscriptionKey,
                SubscriptionRegion = configuration.SubscriptionRegion
            });
        
        services.AddTransient<IRocksoftTranslator, RocksoftAzureTranslator>();
        
        return services;
    }
}