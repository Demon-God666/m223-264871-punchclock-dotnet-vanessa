using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;

namespace M223PunchclockDotnet.Service
{
    public class CategoryService
    {
        private DatabaseContext _databaseContext; 

        public CategoryService(DatabaseContext databaseContext){
            _databaseContext = databaseContext;
        }
        public Task<List<Category>> FindAll()
        {
            return _databaseContext.Categories.ToListAsync();
        }

        public async Task<Category> AddCategory(Category Category)
        {
            _databaseContext.Categories.Add(Category);
            await _databaseContext.SaveChangesAsync();

            return Category;
        }

        public async Task<Category?> FindById(int id)
        {
            return await _databaseContext.Categories.FindAsync(id);
        }

        public async Task DeleteCategory(Category Category)
        {
            _databaseContext.Categories.Remove(Category);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategory(Category Category)
        {
            _databaseContext.Categories.Update(Category);
            await _databaseContext.SaveChangesAsync();

            return Category;
        }
    }
}
