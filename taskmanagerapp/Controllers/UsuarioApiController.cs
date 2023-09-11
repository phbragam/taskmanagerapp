using Microsoft.AspNetCore.Mvc;
using taskmanagerapp.EfCore;
using taskmanagerapp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace taskmanagerapp.Controllers
{

    [ApiController]
    public class UsuariosApiController : ControllerBase
    {
        public readonly UsuariosDbHelper _dbHelper;
        public UsuariosApiController(TaskManagerDbContext taskManagerDbContext)
        {
            _dbHelper = new UsuariosDbHelper(taskManagerDbContext);
        }

        
        [HttpGet]
        [Route("api/[controller]/GetUsuarios")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<UsuarioModel> data = _dbHelper.GetUsuarios();
                if (!data.Any()) type = ResponseType.Empty;
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        
        [HttpGet]
        [Route("api/[controller]/GetUsuarioById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _dbHelper.GetUsuarioById(id);
                if (data == null) type = ResponseType.Empty;
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        
        [HttpPost]
        [Route("api/[controller]/CreateUsuario")]
        public IActionResult Post([FromBody] UsuarioModel usuarioModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool created = _dbHelper.CreateUsuario(usuarioModel);
                if (!created)
                {
                    type = ResponseType.BadRequest;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "Erro ao atualizar"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, usuarioModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        
        [HttpPut("{id}")]
        [Route("api/[controller]/UpdateUsuario")]
        public IActionResult Put([FromBody] UsuarioModel usuarioModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool updated = _dbHelper.UpdateUsuario(usuarioModel);
                if (!updated)
                {
                    type = ResponseType.BadRequest;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "Erro ao atualizar"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, usuarioModel));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        
        [HttpDelete]
        [Route("api/[controller]/DeleteUsuario/{id}")]
        public IActionResult Delete(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool deleted = _dbHelper.DeleteUsuario(id);
                if (!deleted)
                {
                    type = ResponseType.Empty;
                    return Ok(ResponseHandler.GetAppResponse(type, "Usuario não encontrado"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, "Deletado com sucesso"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
