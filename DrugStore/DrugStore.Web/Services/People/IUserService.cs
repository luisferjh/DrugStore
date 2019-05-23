using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.People
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> List();
        Task<UserViewModel> GetUser(int id);
        Task AddUser(CreateViewModel UserModel);
        Task UpdateUser(UpdateViewModel UserModel);
        Task DeleteUser(User user);
        Task<bool> UserExists(int id);
        Task<User> SearchUser(int id);
    }
}
