namespace SmartHospital.MVC.Services.Interfaces;

public interface IApiService
{
    Task<T?> GetAsync<T>(string endpoint, string? token = null);
    Task<T?> PostAsync<T>(string endpoint, object data, string? token = null);
    Task<bool> PutAsync(string endpoint, object data, string? token = null);
    Task<bool> DeleteAsync(string endpoint, string? token = null);
}