using Incidencias.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public IncidentStatus Status { get; set; }

        public string? SolutionComment { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int ResponsibleId { get; set; }

        public Responsible Responsible { get; set; }

        public ICollection<IncidentHistory> History { get; set; } = [];
    }
}
