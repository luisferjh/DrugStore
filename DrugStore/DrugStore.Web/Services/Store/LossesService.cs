using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store.Losses;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.Store
{
    public class LossesService : ILossesService
    {
        private readonly DbContextDrugStore _context;
        public LossesService(DbContextDrugStore context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LossesViewModel>> List()
        {
            var losses = await _context.Losses.ToListAsync();

            return losses.Select(l => new LossesViewModel
            {
                IdLosses = l.IdLosses,
                DateLoss = l.DateLoss,
                Cause = l.Cause,
                State = l.State
            });
        }

        public async Task<LossesViewModel> GetLoss(int id)
        {
            var loss = await _context.Losses.FindAsync(id);
            if (loss == null)
            {
                return null;
            }

            return new LossesViewModel
            {
                IdLosses = loss.IdLosses,
                DateLoss = loss.DateLoss,
                Cause = loss.Cause,
                State = loss.State
            };
        }

        public async Task AddLoss(CreateViewModel lossModel)
        {
            Losses losses = new Losses
            {
                DateLoss = lossModel.DateLoss,
                Cause = lossModel.Cause,
                State = lossModel.State
            };

            _context.Losses.Add(losses);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoss(UpdateViewModel lossModel)
        {
            var loss = await _context.Losses.FirstOrDefaultAsync(l =>
            l.IdLosses == lossModel.IdLosses);

            loss.DateLoss = lossModel.DateLoss;
            loss.Cause = lossModel.Cause;
            loss.State = lossModel.State;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoss(Losses loss)
        {
            _context.Losses.Remove(loss);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> LossExists(int id)
        {
           return await _context.Losses.AnyAsync(l => l.IdLosses == id);
           
        }

        public async Task<Losses> SearchLoss(int id)
        {
            var loss = await _context.Losses.FindAsync(id);
            if (loss == null)
            {
                return null;
            }
            return loss;
        }

      
    }
}
