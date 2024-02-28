using JarmuKolcsonzo.Model.DTOs;
using System.Net.Http.Json;

namespace ApiClient.Repositories
{
    public class PagerAPIRepository<T> : GenericAPIRepository<T>, IPagerRepository<T>
    {
        public PagerAPIRepository(string path, string? baseUrl = null, DelegatingHandler? handler = null) : base(path, baseUrl, handler)
        {
        }

        public async Task<TableDTO<T>> GetAllAsync(int page = 1, int itemsPerPage = 20, string? search = null, string? sortBy = null, bool ascending = true)
        {
            // Hozzáadja a query paramétereket
            var queryParameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "itemsPerPage", itemsPerPage.ToString() }
            };
            if (search is not null)
            {
                queryParameters.Add("search", search);
            }
            if (sortBy is not null)
            {
                queryParameters.Add("sortBy", sortBy);
                queryParameters.Add("ascending", ascending.ToString());
            }
            // Összefűzi a paramétereket URL kódolásban
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            return await client.GetFromJsonAsync<TableDTO<T>>($"{_path}?{queryString}");
        }
    }
}
