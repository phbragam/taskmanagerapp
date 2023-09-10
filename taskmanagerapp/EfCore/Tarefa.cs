using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskmanagerapp.Utils;

namespace taskmanagerapp.EfCore
{

    [Table("tarefa")]
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Titulo { get; set; } = string.Empty;
        [StringLength(300)]
        public string? Descricao { get; set; } 
        public Estados Estado { get; set; } 

        [Required]
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; } 
    }
}
