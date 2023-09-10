using Microsoft.EntityFrameworkCore;
using taskmanagerapp.EfCore;
using taskmanagerapp.Utils;

namespace taskmanagerapp.Models
{
    public class DbHelper
    {
        private TaskManagerDbContext _dbContext;

        public DbHelper(TaskManagerDbContext dbContext) { _dbContext = dbContext; }

        // GET
        public List<TarefaModel> GetTarefas()
        {
            List<TarefaModel> response = new List<TarefaModel>();
            var dataList = _dbContext.Tarefas.ToList();
            dataList.ForEach(row => response.Add(new TarefaModel()
            {
                Id = row.Id,
                Titulo = row.Titulo,
                Descricao = row.Descricao,
                Estado = row.Estado,
                UsuarioId = row.UsuarioId,
            }));
            return response;
        }

        // GET By Id
        public TarefaModel GetTarefaById(int id)
        {
            var data = _dbContext.Tarefas.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (data == null) return null;

            TarefaModel response = new TarefaModel()
            {
                Id = data.Id,
                Titulo = data.Titulo,
                Descricao = data.Descricao,
                Estado = data.Estado,
                UsuarioId = data.UsuarioId,
            };
            return response;
        }

        // GET By Usuario Id
        public List<TarefaModel> GetTarefaByUsuarioId(int idUsuario)
        {
            List<TarefaModel> response = new List<TarefaModel>();
            var dataList = _dbContext.Tarefas.Where(d => d.UsuarioId.Equals(idUsuario)).ToList();
            dataList.ForEach(row => response.Add(new TarefaModel()
            {
                Id = row.Id,
                Titulo = row.Titulo,
                Descricao = row.Descricao,
                Estado = row.Estado,
                UsuarioId = row.UsuarioId,
            }));
            return response;
        }


        public bool CreateTarefa(TarefaModel tarefaModel)
        {
            Tarefa tarefa = new Tarefa();
            bool created = false;

            if (tarefaModel.Id == 0)
            {
                // POST
                tarefa.Titulo = tarefaModel.Titulo;
                tarefa.Descricao = tarefaModel.Descricao;
                tarefa.Estado = Estados.NAO_INICIADA;
                tarefa.Usuario = _dbContext.Usuarios.Where(u => u.Id.Equals(tarefaModel.UsuarioId)).FirstOrDefault();
                _dbContext.Tarefas.Add(tarefa);
                _dbContext.SaveChanges();
                created = true;
            }

            return created;
        }

        public bool UpdateTarefa(TarefaModel tarefaModel)
        {
            Tarefa tarefa = new Tarefa();
            bool updated = false;

            if (tarefaModel.Id > 0)
            {
                tarefa = _dbContext.Tarefas.Where(t => t.Id.Equals(tarefaModel.Id)).FirstOrDefault();

                if (tarefa != null)
                {
                    // PUT
                    tarefa.Titulo = tarefaModel.Titulo;
                    tarefa.Descricao = tarefaModel.Descricao;
                    // LOGICA PARA ATT ESTADO DA TAREFA AQUI
                    tarefa.Estado = tarefaModel.Estado;
                    _dbContext.SaveChanges();
                    updated = true;
                }
                
            }
            return updated;
        }

        public bool DeleteTarefa(int id)
        {
            var tarefa = _dbContext.Tarefas.Where(t => t.Id.Equals(id)).FirstOrDefault();
            bool deleted = false;

            if (tarefa != null)
            {
                _dbContext.Tarefas.Remove(tarefa);
                _dbContext.SaveChanges();
                deleted = true;
            }
            else
            {
                deleted = false;
            }

            return deleted;
        }
    }
}
