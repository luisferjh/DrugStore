using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
using DrugStore.Web.Models.Store.Category;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public interface ICategoryService: ICreateRepository<CreateViewModel>, IReadRepository<CategoryViewModel>
    {
        //Task<IEnumerable<CategoryViewModel>> List();
        Task<CategoryViewModel> GetCategory(int id);
       // Task AddCategory(CreateViewModel category);
        Task UpdateCategory(UpdateViewModel category);
        Task DeleteCategory(Category Pcategory);
        Task<bool> CategoryExists(int id);
        Task<Category> SearchCategory(int id);
    }
}
