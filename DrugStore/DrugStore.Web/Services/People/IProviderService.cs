using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.People
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderViewModel>> List();
        Task<ProviderViewModel> GetProvider(int id);
        Task AddProvider(CreateViewModel providerModel);
        Task UpdateProvider(UpdateViewModel providerModel);
        Task DeleteProvider(Provider provider);
        Task<IEnumerable<SelectViewModel>> SelectProvider();
        Task<bool> ProviderExists(int id);
        Task<Provider> SearchProvider(int id);
    }
}
