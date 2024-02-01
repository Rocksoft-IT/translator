using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Clients;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Configuration;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Services;

[assembly: InternalsVisibleTo("Rocksoft.Translator")]
[assembly: InternalsVisibleTo("Rocksoft.Translator.Tests")]
namespace Rocksoft.Translator.Integrations.AzureAiTranslator;

internal static class Extensions
{
    public static IServiceCollection AddAzureAiTranslator(this IServiceCollection services, AzureAiTranslatorConfiguration configuration)
    {
        services.AddScoped<AzureAiTranslatorConfiguration>(s => configuration);
        
        services.AddHttpClient<AzureAiTranslatorClient>(c =>
        {
            c.BaseAddress = new Uri("https://api.cognitive.microsofttranslator.com");

            c.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuration.SubscriptionKey);
            
            c.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", configuration.SubscriptionRegion);
        });
        
        
        services.AddScoped<IAzureAiTranslatorService, AzureAiTranslatorService>();
        
        return services;
    }
}