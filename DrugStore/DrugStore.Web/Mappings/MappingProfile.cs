using AutoMapper;
using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
//using DrugStore.Web.Models.Store.Category;
//using DrugStore.Web.Models.Store.Product;
using DrugStoreModelCategory = DrugStore.Web.Models.Store.Category;
using DrugStoreModelProduct = DrugStore.Web.Models.Store.Product;
using DrugStore.Web.Services.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Map Category
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, DrugStoreModelCategory.CreateViewModel>();

            // Map Product           
            CreateMap<Product, ProductViewModel>()
                .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(p => p.Laboratory, opt => opt.MapFrom(src => src.Laboratory.LaboratoryName));
            CreateMap<DrugStoreModelProduct.CreateViewModel, Product>();
            CreateMap<DrugStoreModelProduct.UpdateViewModel, Product>();
        }
    }
}
