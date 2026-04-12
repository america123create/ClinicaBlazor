using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaBlazor.Models
{
    public class PermisoPerfil
    {
        public int Id { get; set; }

        [Column("perfil_id")]
        public int PerfilId { get; set; }

        [Column("modulo_id")]
        public int ModuloId { get; set; }

        [Column("agregar")]
        public bool Agregar { get; set; }

        [Column("editar")]
        public bool Editar { get; set; }

        [Column("consultar")]
        public bool Consultar { get; set; }

        [Column("eliminar")]
        public bool Eliminar { get; set; }

        [Column("detalle")]
        public bool Detalle { get; set; }

        public Perfil? Perfil { get; set; }
        public Modulo? Modulo { get; set; }


    }
}