using TestDtoInApi.Models;
using TestDtoInApi.Models.Pagination.Products;
using TestDtoInApi.Repository.BaseRepository.Interface;

namespace TestDtoInApi.Repository.ProductRepository.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName);
        Task<IEnumerable<Product>> GetProductsIncludeCategoryName();

        // Pagination Properties
        Task<PaginatedList<Product>> GetProductsAsync(int pageNumber, int pageSize);
    }

    
}
