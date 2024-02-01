using Rocksoft.Translator.Integrations.AzureAiTranslator.Clients;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Models;

namespace Rocksoft.Translator.Integrations.AzureAiTranslator.Services;

internal class AzureAiTranslatorService : IAzureAiTranslatorService
{
    private readonly AzureAiTranslatorClient _client;
    
    public AzureAiTranslatorService(AzureAiTranslatorClient client)
    {
        _client = client;
    }

    public async Task<string> Translate(string text, string sourceLanguageCode, string destinationLanguageCode)
    {
        await ValidateLanguages(sourceLanguageCode, destinationLanguageCode);
        
        var request = new List<TranslationRequest>
        {
            new TranslationRequest
            {
                Text = text
            }
        };

        var response = await _client.PostTranslation(request, sourceLanguageCode, destinationLanguageCode);

        return response.Translations.First().Text;
    }

    private async Task ValidateLanguages(string sourceLanguageCode, string destinationLanguageCode)
    {
        var supportedLanguages = (await _client.GetSupportedLanguages()).Languages;

        if (supportedLanguages.Keys.All(l => l != sourceLanguageCode))
        {
            throw new Exception("Source language not supported");
        }
        
        if (supportedLanguages.Keys.All(l => l != destinationLanguageCode))
        {
            throw new Exception("Destination language not supported");
        }
    }
}