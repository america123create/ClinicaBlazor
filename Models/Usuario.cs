using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaBlazor.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = "";

        [Column("correo")]
        public string Correo { get; set; } = "";

        [Column("username")]
        public string Username { get; set; } = "";

        [Column("password")]
        public string Password { get; set; } = "";

        [Column("perfil")]
        public string Perfil { get; set; } = "";

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("imagen")]
        public string? Imagen { get; set; }

        [Column("numero_celular")]
        public string? NumeroCelular { get; set; }
    }
}