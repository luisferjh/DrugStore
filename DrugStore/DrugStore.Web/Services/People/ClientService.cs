using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.Client;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.People
{
    public class ClientService : IClientService
    {
        private readonly DbContextDrugStore _context;

        public ClientService(DbContextDrugStore context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClientViewModel>> List()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients.Select(c => new ClientViewModel
            {
                IdClient = c.IdClient,
                Name = c.Name,
                LastName = c.LastName,
                DocumentType = c.DocumentType,
                DocumentNumber = c.DocumentNumber,
                PhoneNumber = c.PhoneNumber,                
                Condition = c.Condition,                
            });
        }
        public async Task<ClientViewModel> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return null;
            }

            return new ClientViewModel
            {           
                IdClient = client.IdClient,
                Name = client.Name,
                LastName = client.LastName,
                DocumentType = client.DocumentType,
                DocumentNumber = client.DocumentNumber,
                PhoneNumber = client.PhoneNumber,
                Condition = client.Condition,
            };
        }

        public async Task<IEnumerable<SelectViewModel>> SelectClient()
        {
            var clients = await _context.Clients.ToListAsync();

            return clients.Select(c => new SelectViewModel {
                IdClient = c.IdClient,
                ClientName = c.Name
            });
        }

        public async Task AddClient(CreateViewModel clientModel)
        {
            Client client = new Client
            {
                Name = clientModel.Name,
                LastName = clientModel.LastName,
                DocumentType = clientModel.DocumentType,
                DocumentNumber = clientModel.DocumentNumber,
                PhoneNumber = clientModel.PhoneNumber,
                Condition = clientModel.Condition,
            };

            _context.Clients.Add(client);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateClient(UpdateViewModel clientModel)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c =>
            c.IdClient == clientModel.IdClient);

            client.IdClient = clientModel.IdClient;
            client.Name = clientModel.Name;
            client.LastName = clientModel.LastName;
            client.DocumentType = clientModel.DocumentType;
            client.DocumentNumber = clientModel.DocumentNumber;
            client.PhoneNumber = clientModel.PhoneNumber;
            client.Condition = clientModel.Condition;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteClient(Client client)
        {
            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ClientExists(int id)
        {
            return await _context.Clients.AnyAsync(c => c.IdClient == id);
        }
        public async Task<Client> SearchClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return null;
            }           

            return client;
        }

        public async Task ActivateClient(int id)
        {
            var client = await _context.Clients
              .FirstOrDefaultAsync(p => p.IdClient == id);

            client.Condition = true;

            await _context.SaveChangesAsync();
        }

        public async Task DeactivateClient(int id)
        {
            var client = await _context.Clients
               .FirstOrDefaultAsync(p => p.IdClient == id);

            client.Condition = false;

            await _context.SaveChangesAsync();
        }
    }
}
