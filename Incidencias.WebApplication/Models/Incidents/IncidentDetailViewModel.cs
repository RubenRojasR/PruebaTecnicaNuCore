using Incidencias.Domain.Entities;

namespace Incidencias.WebApplication.Models.Incidents
{
    public class IncidentDetailViewModel
    {
        public Incident Incident { get; set; } = null!;

        public List<IncidentHistory> History { get; set; } = [];
    }
}
