using Incidencias.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Incidencias.WebApplication.Models.Incidents
{
    public class IncidentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        public int CategoryId { get; set; }

        public int ResponsibleId { get; set; }

        public Priority Priority { get; set; }

        public List<SelectListItem> Categories { get; set; } = [];

        public List<SelectListItem> Responsibles { get; set; } = [];
    }
}
