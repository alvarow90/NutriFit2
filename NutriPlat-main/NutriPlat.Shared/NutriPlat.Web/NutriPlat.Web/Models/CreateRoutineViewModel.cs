// NutriPlat.Web.Models/CreateRoutineViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace NutriPlat.Web.Models
{
    public class CreateRoutineViewModel
    {
        [Required(ErrorMessage = "El nombre de la rutina es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción de la rutina es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}
