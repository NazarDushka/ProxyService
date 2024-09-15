using Proxy.Interfaces;
using Proxy.Models;
using Serilog;
using System.Text.Json;

namespace Proxy.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        // Кеш для збереження користувачів у пам'яті
        private static readonly Dictionary<int, User> _userCache = new Dictionary<int, User>();

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserById(int id)
        {
            // Перевіряємо, чи є користувач в кеші
            if (_userCache.ContainsKey(id))
            {
                Log.Information($"User{id} was found in cache");
                return _userCache[id];
            }

           
            var response = await _httpClient.GetAsync($"https://reqres.in/api/users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Log.Information($"User{id} wasn't found in API");
                return null; // повертаємо null, якщо користувача не знайдено
            }

            
            var responseContent = await response.Content.ReadAsStringAsync();
            var userResponse = JsonSerializer.Deserialize<ReqresUserResponse>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            



            // Додаємо користувача в кеш
            // Логування додавання користувача в кеш
            Log.Information($"User{id} is adding in cache");
            _userCache[id] = userResponse.Data;

            return userResponse.Data;
        }

    }
}
