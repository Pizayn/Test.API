using Newtonsoft.Json;
using System;
using Vienna.Test.API.Entites;

namespace Vienna.Test.API.Services.Implementation
{
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;

        public GithubService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<Monarch>> GetAllStats()
        {
            var response = await _httpClient.GetAsync($"kings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Monarch>>(content);

        }
    }
}
