using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryService(DbContextDrugStore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> ListAsync()
        {
            var Categories = await _context.Categories.ToListAsync();

            return _mapper.Map<List<CategoryViewModel>>(Categories);
         
        }

        public async Task<CategoryViewModel> GetAsync(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return null;
            }
            
            return _mapper.Map<CategoryViewModel>(Category);
       
        }

        public async Task AddAsync(CreateViewModel model)
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

        public async Task UpdateAsync(UpdateViewModel model)
        {          
            var category = await _context.Categories.FirstOrDefaultAsync(c =>
            c.IdCategory == model.IdCategory);            

            category.Name = model.Name;
            category.Description = model.Description;
            category.Condition = model.Condition;
           
            await _context.SaveChangesAsync();
                                  
        }

        public async Task DeleteAsync(Category Pcategory)
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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Category> t)
        {
            throw new NotImplementedException();
        }
    }
}
