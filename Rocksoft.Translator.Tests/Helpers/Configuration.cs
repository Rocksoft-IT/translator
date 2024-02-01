using System.Reflection;
using Microsoft.Extensions.Configuration;
using Rocksoft.Translator.Configuration;

namespace Rocksoft.Translator.Tests.Helpers;

public static class Configuration
{
    private static IConfigurationRoot GetIConfigurationRoot()
    {

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("testsettings.json", optional: false)
            .Build();
    }

    public static TranslatorConfiguration GetApplicationConfiguration()
    {
        var configuration = new TranslatorConfiguration();

        var iConfig = GetIConfigurationRoot();

        iConfig
            .GetSection("TranslatorConfig")
            .Bind(configuration);

        return configuration;
    }
}