namespace taskmanagerapp.Models
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception exception)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "1";
            response.Message = exception.Message;
            return response;
        }

        public static ApiResponse GetAppResponse(ResponseType type, object? contract)
        {
            ApiResponse response;

            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "0";
                    response.Message = "Successo";
                    break;
                case ResponseType.Empty:
                    response.Code = "2";
                    response.Message = "Não há registro disponível";
                    break;
                case ResponseType.BadRequest:
                    response.Code = "3";
                    response.Message = "Verifique os dados enviados para a API";
                    break;
            }
            return response;
        }
    }
}
