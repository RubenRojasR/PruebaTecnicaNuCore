using Incidencias.Application.Interfaces;
using Incidencias.Domain.Entities;
using Incidencias.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Services
{
    public class ResponsibleService : IResponsibleService
    {

        public readonly ApplicationDbContext _context;
        public ResponsibleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Responsible> CreateResponsible(Responsible responsible)
        {
            await _context.AddAsync(responsible);
            await _context.SaveChangesAsync();
            return responsible;
        }

        public async Task<bool> DeleteResponsible(int id)
        {
            var responsible = await _context.Responsible.FindAsync(id);
            if (responsible == null)
            {
                return false;
            }
            responsible.IsActive = false;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Responsible> GetResponsibleById(int id)
        {
            return await _context.Responsible.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Responsible>> GetResponsibles()
        {
            return await _context.Responsible.AsNoTracking().ToListAsync();
        }

        public async Task<List<Responsible>> GetActiveResponsibles()
        {
            return await _context.Responsible.AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<Responsible> UpdateResponsible(Responsible responsible)
        {
            var entity = await _context.Responsible.FindAsync(responsible.Id);
            if (entity == null)
            {
                throw new Exception("El responsable no existe");
            }
            entity.Name = responsible.Name;
            entity.Email = responsible.Email;
            entity.IsActive = responsible.IsActive;
            await _context.SaveChangesAsync();
            return entity;
        }

        
    }
}
