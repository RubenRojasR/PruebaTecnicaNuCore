using Incidencias.Application.Interfaces;
using Incidencias.Domain.Entities;
using Incidencias.Domain.Enums;
using Incidencias.Infraestructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Services
{
    public class IncidentService : IIncidentService
    {
        public readonly ApplicationDbContext _context;
        public IncidentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Incident> CreateIncident(Incident incident)
        {

            incident.Status = IncidentStatus.Nuevo;
            incident.CreatedDate = DateTime.Now;

            await _context.AddAsync(incident);
            await _context.SaveChangesAsync();
            incident.Code = setCode(incident.Id);
            await createIncidentHistory(incident.Id, incident.SolutionComment, incident.CreatedDate, IncidentStatus.Nuevo);
            return incident;
        }

        public async Task<bool> ChangeStatus(int id, IncidentStatus status, string? comment)
        {
            var entity = await _context.Incident.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Incidente no encontrado");
            }
            IncidentStatus previous = entity.Status;
            entity.Status = status;
            var result = await _context.SaveChangesAsync();
            await createIncidentHistory(entity.Id, comment, DateTime.Now, status, previous);
            return result > 0;

        }

        public async Task<List<Incident>> GetIncidents(int? status = null, int? priority = null)
        {

            var query = _context.Incident
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Responsible)
                .AsQueryable();


            if (status.HasValue)
            {
                query = query.Where(x =>
                    (int)x.Status == status.Value);
            }


            if (priority.HasValue)
            {
                query = query.Where(x =>
                    (int)x.Priority == priority.Value);
            }


            return await query
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Incident> GetIncidentById(int id)
        {
            return await _context.Incident
                .Include(x => x.History)
                .Include(x => x.Category)
                .Include(x => x.Responsible)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Incident> UpdateIncident(Incident incident)
        {
            var entity = _context.Incident.Find(incident.Id);
            if (entity == null)
            {
                throw new Exception("Incidente no encontrado");
            }
            IncidentStatus previous = entity.Status;
            entity.Status = incident.Status;
            entity.UpdatedDate = DateTime.Now;
            entity.SolutionComment = incident.SolutionComment;
            entity.CategoryId = incident.CategoryId;
            entity.Description = incident.Description;
            await _context.SaveChangesAsync();
            await createIncidentHistory(entity.Id, entity.SolutionComment, entity.UpdatedDate, entity.Status, previous);
            
            return entity;
        }

        private async Task createIncidentHistory(int incidentId, string? comment, DateTime date, IncidentStatus status, IncidentStatus? previousStatus = null)
        {
            IncidentHistory incidentHistory = new IncidentHistory
            {
                IncidentId = incidentId,
                Comment = comment,
                PreviousStatus = previousStatus,
                CurrentStatus = status,
                UpdatedDate = date
            };

            await _context.IncidentHistory.AddAsync(incidentHistory);
            await _context.SaveChangesAsync();
        }

        private string setCode(int id)
        {
            return string.Format("FOL-NC-{0}", id.ToString("D5"));
        }
    }
}
