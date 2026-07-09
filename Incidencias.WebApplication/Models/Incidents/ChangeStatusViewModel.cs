using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Incidencias.WebApplication.Models.Incidents
{
    public class ChangeStatusViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";

        public string Title { get; set; } = "";

        public int CurrentStatus { get; set; }

        [Required]
        public int NewStatus { get; set; }

        public string? Comment { get; set; }

        public List<SelectListItem> Status { get; set; } = [];
    }
}
