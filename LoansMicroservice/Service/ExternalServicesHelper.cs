using System.Text.Json;
using System.Text;
using System.Text.Encodings.Web;
using LoansMicroservice.Models;

namespace LoansMicroservice.Services
{
    public class ExternalServicesHelper
    {
        private readonly HttpClient _httpClient;
        private const string BOOKS_SERVICE_URL = "http://localhost:5001";

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public ExternalServicesHelper(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<BookCheckDto?> CheckBookAvailability(int bookId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BOOKS_SERVICE_URL}/api/Books/{bookId}");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                    throw new HttpRequestException($"Erro ao buscar Livro {bookId}: Status {response.StatusCode}");
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookCheckDto>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na comunicação com Books Service: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateBookQuantity(int bookId, int newQuantity)
        {
            try
            {
                var response = await _httpClient.PatchAsync(
                    $"{BOOKS_SERVICE_URL}/api/Books/{bookId}/quantity?newQuantity={newQuantity}",
                    new StringContent("", Encoding.UTF8, "application/json")
                );
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar quantidade no Books Service: {ex.Message}");
                return false;
            }
        }
    }
}
