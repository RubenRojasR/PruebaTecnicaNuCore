using Incidencias.Domain.Entities;
using Incidencias.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Interfaces
{
    public interface IIncidentService
    {
        public Task<List<Incident>> GetIncidents(int? status = null, int? priority = null);
        public Task<Incident> GetIncidentById(int id);
        public Task<Incident> CreateIncident(Incident incident);
        public Task<Incident> UpdateIncident(Incident incident);

        public Task<bool> ChangeStatus(int id, IncidentStatus status, string? comment);

    }
}
