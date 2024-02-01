namespace Rocksoft.Translator.Integrations.AzureAiTranslator.Services;

public interface IAzureAiTranslatorService
{
    Task<string> Translate(string text, string sourceLanguageCode, string destinationLanguageCode);
}