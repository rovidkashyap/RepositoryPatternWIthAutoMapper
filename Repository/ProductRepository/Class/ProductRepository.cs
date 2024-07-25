using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TestDtoInApi.DataContext;
using TestDtoInApi.Models;
using TestDtoInApi.Models.Pagination.Products;
using TestDtoInApi.Repository.BaseRepository.Class;
using TestDtoInApi.Repository.ProductRepository.Interface;

namespace TestDtoInApi.Repository.ProductRepository.Class
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        public ProductRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await _context.Products.CountAsync();
            return new PaginatedList<Product>(products, totalCount, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName)
        {
            return await _context.Products
                                .Include(x => x.Category)
                                .Where(c => c.Category.CategoryName == categoryName)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsIncludeCategoryName()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
