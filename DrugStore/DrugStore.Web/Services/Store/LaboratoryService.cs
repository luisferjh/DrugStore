using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugStore.Data;
using DrugStore.Entities.Store;
using DrugStore.Web.Models.Store;
using DrugStore.Web.Models.Store.Laboratory;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Web.Services.Store
{
    public class LaboratoryService : ILaboratoryService
    {
        //Brought context of database 
        private readonly DbContextDrugStore _context;
        public LaboratoryService(DbContextDrugStore context)
        {
            _context = context;
        }
        
        // creamos la implementacion de los metodos y consultamos a la base de datos
        // retornamos valores al controlador correspondiente

        // Get(id) consultamos un laboratorio por id
        public async Task<LaboratoryViewModel> GetLaboratory(int id)
        {
            var Lab = await _context.Laboratories.FindAsync(id);

            if (Lab == null)
            {
                return null;
            }

            return new LaboratoryViewModel {
                IdLaboratory = Lab.IdLaboratory,
                LaboratoryName = Lab.LaboratoryName,
                Description = Lab.Description,
                Condition = Lab.Condition
            };
        }

        //GET() traemos todos los datos de la base de datos como lista
        public async Task<IEnumerable<LaboratoryViewModel>> List()
        {
            var Lab = await _context.Laboratories.ToListAsync();

            return Lab.Select(l => new LaboratoryViewModel
            {
                IdLaboratory = l.IdLaboratory,
                LaboratoryName = l.LaboratoryName,
                Description = l.Description,
                Condition = l.Condition
            });
        }

        public async Task AddLaboratory(CreateViewModel model)
        {           
            Laboratory lab = new Laboratory
            {
                LaboratoryName = model.LaboratoryName,
                Description = model.Description,
                Condition = model.Condition
            };

            _context.Laboratories.Add(lab);

            await _context.SaveChangesAsync();                                      
        }

        public async Task UpdateLaboratory(UpdateViewModel model)
        {           
            var lab = await _context.Laboratories.FirstOrDefaultAsync(c =>
            c.IdLaboratory == model.IdLaboratory);
            
            lab.LaboratoryName = model.LaboratoryName;
            lab.Description = model.Description;
            lab.Condition = model.Condition;

            await _context.SaveChangesAsync();         
        }

        public async Task DeleteLaboratory(Laboratory Plaboratory)
        {        
            _context.Laboratories.Remove(Plaboratory);
           
            await _context.SaveChangesAsync();
                                 
        }

        //searching for id
        public async Task<bool> LabExists(int id)
        {
            return await _context.Laboratories.AnyAsync(e => e.IdLaboratory == id);
        }

        //searching for id, return object entity
        public async Task<Laboratory> SearchLabById(int id)
        {
            var lab = await _context.Laboratories.FindAsync(id);
            if (lab == null)
            {
                return null;
            }

            return lab;
            //return new LaboratoryViewModel
            //{
            //    IdLaboratory = lab.IdLaboratory,
            //    LaboratoryName = lab.LaboratoryName,
            //    Description = lab.Description,
            //    Condition = lab.Condition
            //};
        }
    }
}
