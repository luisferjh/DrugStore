using System;
using System.Collections.Generic;
using System.Linq;
using DrugStore.Web.Models.People.Role;
using DrugStore.Entities.Users;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.People
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> List();
        Task<RoleViewModel> GetRole(int id);
        Task AddRole(CreateViewModel model);
        Task UpdateRole(UpdateViewModel model);
        Task DeactivateRole(int id);
        Task ActivateRole(int id);
        Task<Role> SearchRoleById(int id);
        Task<bool> RoleExists(int id);
    }
}
