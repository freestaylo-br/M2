using M2.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

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

    public async Task<List<Product>> GetProducts(
    string? searchTerm,
    bool isSortDescending,
    int? supplierId)
    {
        string url =
            $"api/Products?searchTerm={searchTerm}" +
            $"&isSortDescending={isSortDescending}";

        if (supplierId.HasValue)
        {
            url += $"&supplierId={supplierId}";
        }

        return await _client
            .GetFromJsonAsync<List<Product>>(url)
            ?? new List<Product>();
    }

    public async Task<List<ProductName>> GetProductNames()
    {
        return await _client
            .GetFromJsonAsync<List<ProductName>>
            ("api/ProductNames")
            ?? new List<ProductName>();
    }

    public async Task<List<Manufacturer>> GetManufacturers()
    {
        return await _client
            .GetFromJsonAsync<List<Manufacturer>>
            ("api/Manufacturer")
            ?? new List<Manufacturer>();
    }

    public async Task<List<Supplier>> GetSuppliers()
    {
        return await _client
            .GetFromJsonAsync<List<Supplier>>
            ("api/Suppliers")
            ?? new List<Supplier>();
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

    public async Task<List<Category>> GetCategories()
    {
        return await _client
            .GetFromJsonAsync<List<Category>>
            ("api/Categories")
            ?? new List<Category>();
    }

    public async Task<bool> UpdateProductAsync(
    Product product,
    FileResult? image)
    {
        var form =
            new MultipartFormDataContent();

        var json =
            JsonSerializer.Serialize(product);

        form.Add(
            new StringContent(json),
            "productJson");

        if (image != null)
        {
            var stream =
                await image.OpenReadAsync();

            form.Add(
                new StreamContent(stream),
                "image",
                image.FileName);
        }

        var response =
            await _client.PutAsync(
                $"api/Products/{product.ProductId}",
                form);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateProductAsync(
    Product product,
    FileResult? selectedImage)
    {
        var form = new MultipartFormDataContent();

        var json =
            System.Text.Json.JsonSerializer.Serialize(product);

        form.Add(
            new StringContent(json),
            "productJson");

        if (selectedImage != null)
        {
            var stream =
                await selectedImage.OpenReadAsync();

            form.Add(
                new StreamContent(stream),
                "image",
                selectedImage.FileName);
        }

        var response =
    await _client.PostAsync(
        "api/Products",
        form);

        if (!response.IsSuccessStatusCode)
        {
            var error =
                await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"API ERROR: {response.StatusCode}\n{error}");
        }

        return true;
    }

    public async Task<string?> DeleteProductAsync(
    int id)
    {
        var response =
            await _client.DeleteAsync(
                $"api/Products/{id}");

        if (response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadAsStringAsync();
    }
}