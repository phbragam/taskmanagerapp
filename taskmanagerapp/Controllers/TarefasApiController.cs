using Microsoft.AspNetCore.Mvc;
using taskmanagerapp.EfCore;
using taskmanagerapp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace taskmanagerapp.Controllers
{

    [ApiController]
    public class TarefasApiController : ControllerBase
    {
        public readonly TarefasDbHelper _dbHelper;
        public TarefasApiController(TaskManagerDbContext taskManagerDbContext)
        {
            _dbHelper = new TarefasDbHelper(taskManagerDbContext);
        }

        [HttpGet]
        [Route("api/[controller]/GetTarefas")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<TarefaModel> data = _dbHelper.GetTarefas();
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
        [Route("api/[controller]/GetTarefaById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _dbHelper.GetTarefaById(id);
                if (data == null) type = ResponseType.Empty;
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetTarefaByUsuarioId/{idUsuario}")]
        public IActionResult GetByUsuario(int idUsuario)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<TarefaModel> data = _dbHelper.GetTarefaByUsuarioId(idUsuario);
                if (!data.Any()) type = ResponseType.Empty;
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/CreateTarefa")]
        public IActionResult Post([FromBody] TarefaModel tarefaModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool created = _dbHelper.CreateTarefa(tarefaModel);
                if (!created)
                {
                    type = ResponseType.BadRequest;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "Erro ao criar"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, "Tarefa criada com sucesso"));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("{id}")]
        [Route("api/[controller]/UpdateTarefa")]
        public IActionResult Put([FromBody] TarefaModel tarefaModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool updated = _dbHelper.UpdateTarefa(tarefaModel);
                if (!updated)
                {
                    type = ResponseType.BadRequest;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "Erro ao atualizar"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, "Tarefa atualizada com sucesso"));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("{id}")]
        [Route("api/[controller]/UpdateEstadoTarefa")]
        public IActionResult PutEstadoTarefa([FromBody] TarefaModel tarefaModel)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool updated = _dbHelper.UpdateEstadoTarefa(tarefaModel);
                if (!updated)
                {
                    type = ResponseType.BadRequest;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "Erro ao atualizar"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, "Estado da tarefa atualizado com sucesso"));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteTarefa/{id}")]
        public IActionResult Delete(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                bool deleted = _dbHelper.DeleteTarefa(id);
                if (!deleted)
                {
                    type = ResponseType.Empty;
                    return Ok(ResponseHandler.GetAppResponse(type, "Tarefa não encontrada"));
                }
                return Ok(ResponseHandler.GetAppResponse(type, "Tarefa deletada com sucesso"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
