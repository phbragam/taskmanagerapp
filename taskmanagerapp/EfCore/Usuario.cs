using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskmanagerapp.EfCore
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        [StringLength(200)]
        public string Senha { get; set; } = string.Empty;
    }
}
