using Newtonsoft.Json;

namespace Rocksoft.Translator.Integrations.AzureAiTranslator.Models;

internal class SupportedLanguages
{
    [JsonProperty(PropertyName = "translation")]
    public Dictionary<string, LanguageInfo> Languages { get; set; }
}