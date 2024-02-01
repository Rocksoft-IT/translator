using System.Text;
using Newtonsoft.Json;
using Rocksoft.Translator.Integrations.AzureAiTranslator.Models;

namespace Rocksoft.Translator.Integrations.AzureAiTranslator.Clients;

internal class AzureAiTranslatorClient
{
    private readonly HttpClient _httpClient;

    public AzureAiTranslatorClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SupportedLanguages> GetSupportedLanguages()
    {
        var response = await _httpClient.GetAsync("/languages?api-version=3.0&scope=translation");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error while calling supported languages endpoint");
        }
        
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var supportedLanguages = JsonConvert.DeserializeObject<SupportedLanguages>(responseBody);

        if (supportedLanguages == null || supportedLanguages.Languages == null)
        {
            throw new Exception("Error while deserializing supported languages");
        }

        return supportedLanguages;
    }

    public async Task<TranslationResponse> PostTranslation(List<TranslationRequest> request, string sourceLanguageCode,
        string destinationLanguageCode)
    {
        var requestBody = JsonConvert.SerializeObject(request);
        
        
        using StringContent jsonContent = new(requestBody, Encoding.UTF8, "application/json");

        var url = $"translate?api-version=3.0&from={sourceLanguageCode}&to={destinationLanguageCode}";
        
        using HttpResponseMessage response = await _httpClient.PostAsync(url, jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error while calling translate endpoint");
        }
    
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        var translationResponse = JsonConvert.DeserializeObject<List<TranslationResponse>>(jsonResponse);

        if (translationResponse == null)
        {
            throw new Exception("Error while deserializing  translation response");
        }

        return translationResponse.First();
    }
}