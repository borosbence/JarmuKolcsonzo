using JarmuKolcsonzo.Model.DTOs;

namespace ApiClient.Repositories
{
    public interface IPagerRepository<T> : IGenericRepository<T>
    {
        Task<TableDTO<T>> GetAllAsync(
            int page = 1,
            int itemsPerPage = 20,
            string? search = null,
            string? sortBy = null,
            bool ascending = true);
    }
}
