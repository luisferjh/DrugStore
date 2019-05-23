using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.User;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.People
{
    public class UserService : IUserService
    {
        private readonly DbContextDrugStore _context;
        public UserService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserViewModel>> List()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(c => new UserViewModel
            {
                IdUser = c.IdUser,
                IdRole = c.IdRole,
                UserName = c.UserName,               
                DocumentType = c.DocumentType,
                DocumentNumber = c.DocumentNumber,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                PasswordHash = c.PasswordHash,
                Condition = c.Condition,
            });
        }

        public Task<UserViewModel> GetUser(int id)
        {
            throw new NotImplementedException();
        }
     
        public Task AddUser(CreateViewModel UserModel)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUser(UpdateViewModel UserModel)
        {
            throw new NotImplementedException();
        }
        public Task DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(int id)
        {
            throw new NotImplementedException();
        }
        public Task<User> SearchUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
