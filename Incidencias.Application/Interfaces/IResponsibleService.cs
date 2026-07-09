using Incidencias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Application.Interfaces
{
    public interface IResponsibleService
    {
        public Task<List<Responsible>> GetResponsibles();
        public Task<List<Responsible>> GetActiveResponsibles();
        public Task<Responsible> GetResponsibleById(int id);
        public Task<Responsible> CreateResponsible(Responsible responsible);
        public Task<Responsible> UpdateResponsible(Responsible responsible);
        public Task<bool> DeleteResponsible(int id);
    }
}
