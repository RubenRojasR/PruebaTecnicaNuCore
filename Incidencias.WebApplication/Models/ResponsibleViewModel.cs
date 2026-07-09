using System.ComponentModel.DataAnnotations;

namespace Incidencias.WebApplication.Models
{
    public class ResponsibleViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required]  
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;
    }
}
