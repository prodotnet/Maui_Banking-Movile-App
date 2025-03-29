using Banking_App.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Banking_App.Services
{

 public class ApiService
 {


        private readonly HttpClient _httpClient;

    public ApiService()
    {
        // Assuming you are running the API on localhost, use the appropriate base URL.
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7274/api/") 
        };
    }

    // User Registration
    public async Task<bool> RegisterUser(RegisterDto model)
    {
        var response = await _httpClient.PostAsJsonAsync("Authentication/Register", model);
        return response.IsSuccessStatusCode;
    }

    // User Login
    public async Task<string> LoginUser(LoginDto model)
    {
        var response = await _httpClient.PostAsJsonAsync("Authentication/Login", model);
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user.Jwt; // Return JWT token for future API requests
        }
        return null;
    }

    }
}
