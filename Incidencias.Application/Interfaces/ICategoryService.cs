using Incidencias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetActiveCategories();
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
        public Task<bool> DeleteCategory(int id);
    }
}
