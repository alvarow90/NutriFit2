using System.ComponentModel.DataAnnotations;

namespace NutriPlat.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
