using Incidencias.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Domain.Entities
{
    public class IncidentHistory
    {
        public int Id { get; set; }

        public int IncidentId { get; set; }

        public Incident Incident { get; set; } = null!;

        public IncidentStatus? PreviousStatus { get; set; }

        public IncidentStatus CurrentStatus { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? Comment { get; set; }
    }
}
