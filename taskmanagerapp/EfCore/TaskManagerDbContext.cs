using Microsoft.EntityFrameworkCore;
using taskmanagerapp.Utils;

namespace taskmanagerapp.EfCore
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Tarefa>().ToTable("Tarefas");

            int idUsuario = 1;
            int idTarefa = 1;

            modelBuilder.Entity<Usuario>().HasData(new
            {
                Id = idUsuario,
                Nome = "userteste",
                Email = "userteste@teste.com",
                Senha = "123userteste"
            });

            modelBuilder.Entity<Tarefa>().HasData(new
            {
                Id = idTarefa,
                Titulo = "Tarefa teste",
                Descricao = "Essa é uma tarefa de teste",
                Estado = Estados.NAO_INICIADA,
                UsuarioId = idUsuario
            });
        }
    }
}
