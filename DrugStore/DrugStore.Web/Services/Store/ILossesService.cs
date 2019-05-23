using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
using DrugStore.Web.Models.Store.Losses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public interface ILossesService
    {
        Task<IEnumerable<LossesViewModel>> List();
        Task<LossesViewModel> GetLoss(int id);
        Task AddLoss(CreateViewModel loss);
        Task UpdateLoss(UpdateViewModel loss);
        Task DeleteLoss(Losses loss);
        Task<bool> LossExists(int id);
        Task<Losses> SearchLoss(int id);
    }
}
