using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
using DrugStore.Web.Models.Store.Laboratory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public interface ILaboratoryService
    {
        Task<IEnumerable<LaboratoryViewModel>> List();
        Task<LaboratoryViewModel> GetLaboratory(int id);
        Task AddLaboratory(CreateViewModel model);
        Task UpdateLaboratory(UpdateViewModel model);
        Task ActivateLab(int id);
        Task DeactivateLab(int id);
        Task DeleteLaboratory(Laboratory Plaboratory);
        Task<bool> LabExists(int id);
        Task<Laboratory> SearchLabById(int id);
    }
}
