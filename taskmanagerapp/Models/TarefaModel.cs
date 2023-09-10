using taskmanagerapp.Utils;

namespace taskmanagerapp.Models
{
    public class TarefaModel
    {
        public int Id { get; set; } 
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public Estados Estado { get; set; }
        public int UsuarioId { get; set; }
    }
}
