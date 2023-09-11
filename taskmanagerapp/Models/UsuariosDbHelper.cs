using Microsoft.EntityFrameworkCore;
using taskmanagerapp.EfCore;
using taskmanagerapp.Utils;

namespace taskmanagerapp.Models
{
    public class UsuariosDbHelper
    {
        private TaskManagerDbContext _dbContext;

        public UsuariosDbHelper(TaskManagerDbContext dbContext) { _dbContext = dbContext; }

        // GET
        public List<UsuarioModel> GetUsuarios()
        {
            List<UsuarioModel> response = new List<UsuarioModel>();
            var dataList = _dbContext.Usuarios.ToList();
            dataList.ForEach(row => response.Add(new UsuarioModel()
            {
                Id = row.Id,
                Nome = row.Nome,
                Email = row.Email,
                Senha = row.Senha
            }));
            return response;
        }

        // GET By Id
        public UsuarioModel GetUsuarioById(int id)
        {
            var data = _dbContext.Usuarios.Where(d => d.Id.Equals(id)).FirstOrDefault();
            if (data == null) return null;

            UsuarioModel response = new UsuarioModel()
            {
                Id = data.Id,
                Nome = data.Nome,
                Email = data.Email,
                Senha = data.Senha
            };
            return response;
        }

        // POST
        public bool CreateUsuario(UsuarioModel usuarioModel)
        {
            Usuario usuario = new Usuario();
            bool created = false;

            if (usuarioModel.Id == 0)
            {
                usuario.Nome = usuarioModel.Nome;
                usuario.Email = usuarioModel.Email;
                usuario.Senha = usuarioModel.Senha;
                
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                created = true;
            }

            return created;
        }

        public bool UpdateUsuario(UsuarioModel usuarioModel)
        {
            bool updated = false;

            if (usuarioModel.Id > 0)
            {
                var usuario = _dbContext.Usuarios.Where(u => u.Id.Equals(usuarioModel.Id)).FirstOrDefault();

                if (usuario != null)
                {
                    usuario.Nome = usuarioModel.Nome;
                    usuario.Email = usuarioModel.Email;
                    usuario.Senha = usuarioModel.Senha;

                    _dbContext.SaveChanges();
                    updated = true;
                }
                
            }
            return updated;
        }

        public bool DeleteUsuario(int id)
        {
            var usuario = _dbContext.Usuarios.Where(u => u.Id.Equals(id)).FirstOrDefault();
            bool deleted = false;

            if (usuario != null)
            {
                _dbContext.Usuarios.Remove(usuario);
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
