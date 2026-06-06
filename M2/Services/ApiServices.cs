using System.Net.Http.Json;
using M2.Models;

namespace M2.Services;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();

        _client.BaseAddress =
            new Uri("http://localhost:5156/");
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _client.GetFromJsonAsync<List<Product>>("api/Products");
    }

    public async Task<Staff?> Login(
        string login,
        string password)
    {
        var request = new
        {
            Login = login,
            Password = password
        };

        var response =
            await _client.PostAsJsonAsync(
                "api/Auth/login",
                request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<Staff>();
    }
}