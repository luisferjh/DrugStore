using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Users;
using DrugStore.Web.Models.People.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DrugStore.Web.Services.People
{
    public class UserService : IUserService
    {
        private readonly DbContextDrugStore _context;
        private readonly IConfiguration _config;

        public UserService(DbContextDrugStore context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IEnumerable<UserViewModel>> List()
        {            
            return await _context.Users
                .Include(r => r.Role)                               
                .Select(c => new UserViewModel
                {
                    IdUser = c.IdUser,
                    IdRole = c.IdRole,
                    role = c.Role.RoleName,
                    UserName = c.UserName,
                    DocumentType = c.DocumentType,
                    DocumentNumber = c.DocumentNumber,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    CreatedDate = EF.Property<DateTime>(c, "DateOn"),                
                    Email = c.Email,
                    Condition = c.Condition
                }).ToListAsync();
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserViewModel
            {
                IdUser = user.IdUser,
                IdRole = user.IdRole,
                UserName = user.UserName,
                DocumentType = user.DocumentType,
                DocumentNumber = user.DocumentNumber,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                CreatedDate = _context.Entry(user).Property<DateTime>("DateOn").CurrentValue,
                Email = user.Email,
                Condition = user.Condition,
            };
        }

        public async Task<UserProfileViewModel> UserProfileNav(int id)
        {
            var user = await _context.Users.
                Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.IdUser == id);
            if (user == null)
            {
                return null;
            }

            return new UserProfileViewModel
            {
                IdUser = user.IdUser,
                Name = user.UserName,                
                Role = user.Role.RoleName
            };
        }

        public async Task AddUser(CreateViewModel UserModel)
        {
            CreatePassword(UserModel.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            User user = new User
            {
                IdRole = UserModel.IdRole,
                UserName = UserModel.UserName,
                DocumentType = UserModel.DocumentType,
                DocumentNumber = UserModel.DocumentNumber,
                Address = UserModel.Address,
                PhoneNumber = UserModel.PhoneNumber,
                Email = UserModel.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                Condition = UserModel.State,
            };

            await _context.Users.AddAsync(user);

            _context.SaveChanges();
        }
         

        public async Task<UserLoginViewModel> Login(LoginViewModel model)
        {
            var user = await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            return new UserLoginViewModel
            {
                IdUser = user.IdUser,
                IdRole = user.IdRole,
                Role = user.Role.RoleName,
                UserName = user.UserName,
                DocumentType = user.DocumentType,
                DocumentNumber = user.DocumentNumber,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                State = user.Condition
            };
        }

        public string GenerateToken(UserLoginViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("IdUser", user.IdUser.ToString()),
                new Claim("Role", user.Role),
                new Claim("Name", user.UserName)
            };

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(80),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool CheckPassword(string password, byte[] passwordHashStored, byte[] passwordSaltStored)
        {
            using (var hmac = new HMACSHA512(passwordSaltStored))
            {
                var newPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashStored).SequenceEqual(new ReadOnlySpan<byte>(newPasswordHash));
            }
        }

        public async Task UpdateUser(UpdateViewModel UserModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == UserModel.IdUser);

            user.IdUser = UserModel.IdUser;
            user.IdRole = UserModel.IdRole;
            user.UserName = UserModel.UserName;
            user.DocumentType = UserModel.DocumentType;
            user.DocumentNumber = UserModel.DocumentNumber;
            user.Address = UserModel.Address;
            user.PhoneNumber = UserModel.PhoneNumber;
            user.Email = UserModel.Email;

            if (UserModel.Act_Password == true)
            {
                CreatePassword(UserModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            await _context.SaveChangesAsync();
        }  

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(u => u.IdUser == id);
        }
        public Task<User> SearchUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task ActivateUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id);

            user.Condition = true;

            await _context.SaveChangesAsync();
        }

        public async Task DeactivateUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id);

            user.Condition = false;

            await _context.SaveChangesAsync();
        }
    }
}
