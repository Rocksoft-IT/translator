using System.Globalization;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Services;
using Rocksoft.Translator.Abstraction;

namespace Rocksoft.Translator.Services;

internal class RocksoftAzureTranslator : IRocksoftTranslator
{
    private readonly IAzureAiTranslatorService _translator;

    public RocksoftAzureTranslator(IAzureAiTranslatorService translator)
    {
        _translator = translator;
    }

    public async Task<string> Translate(string text, string sourceLanguage, string destinationLanguage)
    {
        var sourceLanguageCode = GetLanguageCode(sourceLanguage);
        var destinationLanguageCode = GetLanguageCode(destinationLanguage);

        return await _translator.Translate(text, sourceLanguageCode, destinationLanguageCode);
    }


    private string GetLanguageCode(string languageName)
    {
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var cultureInfo = cultures.FirstOrDefault(c => c.EnglishName.Contains(languageName));

        if (cultureInfo == null)
        {
            throw new Exception("Unsupported language");
        }
        
        return cultureInfo.TwoLetterISOLanguageName;
    }
}