using DrugStore.Data;
using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
using DrugStore.Web.Models.Store.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public class CategoryService : ICategoryService
    {
        private readonly DbContextDrugStore _context;

        public CategoryService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            var Categories = await _context.Categories.ToListAsync();
            return Categories.Select(c => new CategoryViewModel
            {
                IdCategory = c.IdCategory,
                Name = c.Name,
                Description = c.Description,
                Condition = c.Condition
            });
        }

        public async Task<CategoryViewModel> GetCategory(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return null;
            }

            return new CategoryViewModel
            {
                IdCategory = Category.IdCategory,
                Name = Category.Name,
                Description = Category.Description,
                Condition = Category.Condition
            };
        }

        public async Task AddCategory(CreateViewModel model)
        {           
            Category category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                Condition = model.Condition
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();            
        }

        public async Task UpdateCategory(UpdateViewModel model)
        {          
            var category = await _context.Categories.FirstOrDefaultAsync(c =>
            c.IdCategory == model.IdCategory);            

            category.Name = model.Name;
            category.Description = model.Description;
            category.Condition = model.Condition;
           
            await _context.SaveChangesAsync();
                                  
        }

        public async Task DeleteCategory(Category Pcategory)
        {          
            //var category = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(Pcategory);

            await _context.SaveChangesAsync();            
                        
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.IdCategory == id);
        }

        //searching for id, return object entity
        public async Task<Category> SearchCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            //return new CategoryViewModel {
            //    IdCategory = category.IdCategory,
            //    Name = category.Name,
            //    Description = category.Description,
            //    Condition = category.Condition
            //};

            return category;
        }
     
    }
}
