using System.ComponentModel.DataAnnotations;

namespace NutriPlat.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Usuario";

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
