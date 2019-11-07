using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.People
{
    public interface IClientService
    {
        Task<IEnumerable<ClientViewModel>> List();
        Task<ClientViewModel> GetClient(int id);
        Task<ClientViewModel> GetClientByPhoneNumber(string phoneNumber);
        Task<IEnumerable<ClientViewModel>> ClientInSale(string name, string lastName);
        Task AddClient(CreateViewModel clientModel);
        Task UpdateClient(UpdateViewModel clientModel);
        Task ActivateClient(int id);
        Task DeactivateClient(int id);
        Task DeleteClient(Client client);
        Task<IEnumerable<SelectViewModel>> SelectClient(); 
        Task<bool> ClientExists(int id);
        Task<Client> SearchClient(int id);
    }
}
