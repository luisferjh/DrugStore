using DrugStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using DrugStore.Web.Models.People.Role;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DrugStore.Entities.Users;

namespace DrugStore.Web.Services.People
{
    public class RoleService:IRoleService
    {
        private readonly DbContextDrugStore _context;
        public RoleService(DbContextDrugStore context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleViewModel>> List()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(r => new RoleViewModel
            {
                IdRole = r.IdRole,
                RoleName = r.RoleName,
                Description = r.Description,
                State = r.Condition
            });
        }

        public async Task<RoleViewModel> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return null;
            }

            return new RoleViewModel
            {
                IdRole = role.IdRole,
                RoleName = role.RoleName,
                Description = role.Description,
                State = role.Condition
            };
        }

        public async Task AddRole(CreateViewModel model)
        {
            var role = new Role
            {
                RoleName = model.RoleName,
                Description = model.Description,
                Condition = model.State
            };

            await _context.Roles.AddAsync(role);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(UpdateViewModel model)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.IdRole == model.IdRole);

            role.IdRole = model.IdRole;
            role.RoleName = model.RoleName;
            role.Description = model.Description;
            role.Condition = model.State;

            await _context.SaveChangesAsync();
        }

        public Task<Role> SearchRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RoleExists(int id)
        {
            return await _context.Roles.AnyAsync(r => r.IdRole == id);
        }

        public async Task DeactivateRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.IdRole == id);

            role.Condition = false;

            await _context.SaveChangesAsync();
        }

        public async Task ActivateRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.IdRole == id);

            role.Condition = true;

            await _context.SaveChangesAsync();
        }
    }
}
