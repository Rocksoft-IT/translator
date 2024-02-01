using Microsoft.Extensions.DependencyInjection;
using Rocksoft.Translator.Services;

namespace Rocksoft.Translator.Tests;

public class RocksoftAzureTranslatorTests
{
    [Theory]
    [InlineData("English", "Test")]
    [InlineData("Test", "Polish")]
    [InlineData("Test", "Test")]
    public async Task Translate_Should_Throw_UnsupportedLanguageException(string sourceLanguage, string targetLanguage)
    {
        // Arrange
        await using ServiceProvider serviceProvider = Helpers.Services.CreateServiceCollection().BuildServiceProvider();
        var rocksoftTranslator = serviceProvider.GetRequiredService<RocksoftAzureTranslator>();
        
        // Act
        Func<Task<string>> output = async () => await rocksoftTranslator.Translate("Test", sourceLanguage, targetLanguage);
        
        // Assert
        await output.Should().ThrowAsync<Exception>().WithMessage("Unsupported language");
    }
    
    [Fact]
    public async Task Translate_Should_Return_Translation()
    {
        // Arrange
        await using ServiceProvider serviceProvider = Helpers.Services.CreateServiceCollection().BuildServiceProvider();
        var rocksoftTranslator = serviceProvider.GetRequiredService<RocksoftAzureTranslator>();
        
        // Act
        var translation = await rocksoftTranslator.Translate("Test", "English", "Polish");
        
        // Assert
        translation.Should().NotBeEmpty();
    }
}