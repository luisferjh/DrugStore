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
        Task<UserLoginViewModel> Login(LoginViewModel model);
        Task AddUser(CreateViewModel UserModel);
        void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool CheckPassword(string password, byte[] passwordHashStored, byte[] passwordSaltStored);
        string GenerateToken(UserLoginViewModel user);
        Task UpdateUser(UpdateViewModel UserModel);       
        Task ActivateUser(int id);
        Task DeactivateUser(int id);
        Task<bool> UserExists(int id);
        Task<User> SearchUser(int id);
    }
}
