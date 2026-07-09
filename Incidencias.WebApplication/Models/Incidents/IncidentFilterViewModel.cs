using Incidencias.Domain.Entities;

namespace Incidencias.WebApplication.Models.Incidents
{
    public class IncidentFilterViewModel
    {
        public int? Status { get; set; }

        public int? Priority { get; set; }

        public List<Incident> Incidents { get; set; } = [];
    }
}
