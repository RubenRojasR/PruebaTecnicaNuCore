using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Incidencias.WebApplication.Models.Incidents
{
    public class IncidentCreateViewModel
    {
        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Responsable")]
        public int ResponsibleId { get; set; }

        [Required]
        [Display(Name = "Prioridad")]
        public int Priority { get; set; }

        public List<SelectListItem> Categories { get; set; } = [];

        public List<SelectListItem> Responsibles { get; set; } = [];
    }
}
