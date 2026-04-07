using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    private void SetAuthHeader(string? token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = token != null
            ? new AuthenticationHeaderValue("Bearer", token)
            : null;
    }

    public async Task<T?> GetAsync<T>(string endpoint, string? token = null)
    {
        try
        {
            SetAuthHeader(token);
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GET request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data, string? token = null)
    {
        try
        {
            SetAuthHeader(token);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "POST request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<bool> PutAsync(string endpoint, object data, string? token = null)
    {
        try
        {
            SetAuthHeader(token);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PUT request failed: {Endpoint}", endpoint);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string endpoint, string? token = null)
    {
        try
        {
            SetAuthHeader(token);
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DELETE request failed: {Endpoint}", endpoint);
            return false;
        }
    }
}