namespace Rocksoft.Translator.Abstraction;

public interface IRocksoftTranslator
{
    Task<string> Translate(string text, string sourceLanguage, string destinationLanguage);
}