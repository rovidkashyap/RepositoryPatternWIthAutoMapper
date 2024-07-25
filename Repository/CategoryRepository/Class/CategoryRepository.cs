using TestDtoInApi.DataContext;
using TestDtoInApi.Models;
using TestDtoInApi.Repository.BaseRepository.Class;
using TestDtoInApi.Repository.CategoryRepository.Interface;

namespace TestDtoInApi.Repository.CategoryRepository.Class
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyDbContext context) : base(context)
        {
            
        }
    }
}
