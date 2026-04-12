using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaBlazor.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Column("correo")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Correo { get; set; } = string.Empty;

        [Column("username")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;

        [Column("perfil")]
        [Required(ErrorMessage = "El perfil es obligatorio")]
        public string Perfil { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("imagen")]
        public string? Imagen { get; set; }

        [Column("numero_celular")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número celular debe tener exactamente 10 dígitos")]
        public string? NumeroCelular { get; set; }
    }
}