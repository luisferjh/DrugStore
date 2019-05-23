using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.Provider;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.People
{
    public class ProviderService : IProviderService
    {
        private readonly DbContextDrugStore _context;
        public ProviderService(DbContextDrugStore context)
        {
            _context = context;
        }
        public async Task<ProviderViewModel> GetProvider(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return null;
            }

            return new ProviderViewModel
            {
                IdProvider = provider.IdProvider,
                ProviderName = provider.ProviderName,
                DocumentNumber = provider.DocumentNumber,
                Address = provider.Address,
                PhoneNumber = provider.PhoneNumber,
                Email = provider.Email                
            };
        }

        public async Task<IEnumerable<ProviderViewModel>> List()
        {
            var providers = await _context.Providers.ToListAsync();
            return providers.Select(p => new ProviderViewModel
            {
                IdProvider = p.IdProvider,
                ProviderName = p.ProviderName,
                DocumentNumber = p.DocumentNumber,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email
            });
        }
        public async Task AddProvider(CreateViewModel providerModel)
        {
            Provider provider = new Provider
            {                
                ProviderName = providerModel.ProviderName,
                DocumentNumber = providerModel.DocumentNumber,
                Address = providerModel.Address,
                PhoneNumber = providerModel.PhoneNumber,
                Email = providerModel.Email
            };

            _context.Providers.Add(provider);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateProvider(UpdateViewModel providerModel)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p =>
            p.IdProvider == providerModel.IdProvider);

            provider.IdProvider = providerModel.IdProvider;
            provider.ProviderName = providerModel.ProviderName;
            provider.DocumentNumber = providerModel.DocumentNumber;                       
            provider.Address = providerModel.Address;
            provider.PhoneNumber = providerModel.PhoneNumber;
            provider.Email = providerModel.Email;
          
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProvider(Provider provider)
        {
            _context.Providers.Remove(provider);

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SelectViewModel>> SelectProvider()
        {
            var providers = await _context.Providers.ToListAsync();

            return providers.Select(p => new SelectViewModel {
                IdProvider = p.IdProvider,
                ProviderName = p.ProviderName,
            });
        }

        public async Task<bool> ProviderExists(int id)
        {
            return await _context.Providers.AnyAsync(p => p.IdProvider == id);
        }

        public async Task<Provider> SearchProvider(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return null;
            }

            return provider;
        }
       
    }
}
