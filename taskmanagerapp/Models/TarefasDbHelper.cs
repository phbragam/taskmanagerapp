using Microsoft.EntityFrameworkCore;
using taskmanagerapp.EfCore;
using taskmanagerapp.Utils;

namespace taskmanagerapp.Models
{
    public class TarefasDbHelper
    {
        private TaskManagerDbContext _dbContext;

        public TarefasDbHelper(TaskManagerDbContext dbContext) { _dbContext = dbContext; }

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
            var usuarioId = _dbContext.Usuarios.Where(u => u.Id.Equals(tarefaModel.UsuarioId)).FirstOrDefault();
            bool created = false;

            if (tarefaModel.Id == 0 && usuarioId != null)
            {
                tarefa.Titulo = tarefaModel.Titulo;
                tarefa.Descricao = tarefaModel.Descricao;
                tarefa.Estado = Estados.NAO_INICIADA;
                tarefa.Usuario = usuarioId;
                _dbContext.Tarefas.Add(tarefa);
                _dbContext.SaveChanges();
                created = true;
            }

            return created;
        }

        public bool UpdateTarefa(TarefaModel tarefaModel)
        {
            bool updated = false;

            if (tarefaModel.Id > 0)
            {
                var tarefa = _dbContext.Tarefas.Where(t => t.Id.Equals(tarefaModel.Id)).FirstOrDefault();

                if (tarefa != null)
                {
                    tarefa.Titulo = tarefaModel.Titulo;
                    tarefa.Descricao = tarefaModel.Descricao;
                    _dbContext.SaveChanges();
                    updated = true;
                }
                
            }
            return updated;
        }

        public bool UpdateEstadoTarefa(TarefaModel tarefaModel)
        {
            bool updated = false;

            if (tarefaModel.Id > 0)
            {
                var tarefa = _dbContext.Tarefas.Where(t => t.Id.Equals(tarefaModel.Id)).FirstOrDefault();

                if (tarefa != null && AbleToUpdate(tarefa.Estado, tarefaModel.Estado))
                {
                    tarefa.Estado = tarefaModel.Estado;
                    _dbContext.SaveChanges();
                    updated = true;
                }

            }
            return updated;
        }

        public bool AbleToUpdate(Estados actualState, Estados toState)
        {
            bool canUpdate = false;

            switch (actualState)
            {
                case Estados.NAO_INICIADA:
                    if (toState == Estados.EM_PROGRESSO || toState == Estados.FINALIZADA || toState == Estados.ARQUIVADA) canUpdate = true;
                    break;
                case Estados.EM_PROGRESSO:
                    if (toState == Estados.NAO_INICIADA || toState == Estados.FINALIZADA || toState == Estados.ARQUIVADA) canUpdate = true;
                    break;
                case Estados.FINALIZADA:
                    if (toState == Estados.ARQUIVADA) canUpdate = true;
                    break;
                case Estados.ARQUIVADA:
                    canUpdate = false;
                    break;
            }

            return canUpdate;
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
