using Incidencias.Application.Interfaces;
using Incidencias.Domain.Entities;
using Incidencias.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;


        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            category.IsActive = false;
            var result =  await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.AsNoTracking().ToListAsync();
        }

        public async Task<List<Category>> GetActiveCategories()
        {
            return await _context.Category.AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Category.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {

            var entity = await _context.Category.FindAsync(category.Id);

            if (entity == null)
                throw new Exception("La categoría no existe.");

            entity.Name = category.Name;
            entity.Description = category.Description;
            entity.IsActive = category.IsActive;

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
