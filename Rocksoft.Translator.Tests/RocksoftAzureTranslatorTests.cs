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
    
    [Theory]
    [InlineData("Polish", "English")]
    [InlineData("English", "Polish")]
    [InlineData("English", "Bangla")]
    [InlineData("English", "Chinese")]
    [InlineData("English", "Ukrainian")]
    [InlineData("English", "Romanian")]
    [InlineData("English", "German")]
    [InlineData("English", "Norwegian")]
    [InlineData("English", "Russian")]
    [InlineData("English", "Spanish")]
    [InlineData("English", "French")]
    [InlineData("English", "Portuguese")]
    [InlineData("English", "Italian")]
    [InlineData("English", "Hindi")]
    [InlineData("English", "Arabic")]
    [InlineData("English", "Japanese")]
    [InlineData("English", "Hungarian")]
    [InlineData("English", "Finnish")]
    [InlineData("English", "Turkish")]
    [InlineData("English", "Urdu")]
    [InlineData("English", "Indonesian")]
    public async Task Translate_Should_Return_Translation(string sourceLanguage, string targetLanguage)
    {
        // Arrange
        await using ServiceProvider serviceProvider = Helpers.Services.CreateServiceCollection().BuildServiceProvider();
        var rocksoftTranslator = serviceProvider.GetRequiredService<RocksoftAzureTranslator>();
        
        // Act
        var translation = await rocksoftTranslator.Translate("Test", sourceLanguage, targetLanguage);
        
        // Assert
        translation.Should().NotBeEmpty();
    }
}