# Task Manager API Calculator

This is an API for a task manager app. You can use Postman to test and interact with this API. The API was developed using **.NET Core 6** in Visual Studio 2022.

## Make sure you have the server running locally

1. **Install Visual Studio Community 2022**: Download and install [Visual Studio](https://visualstudio.microsoft.com/vs/) on your computer. Check for .NET 6.0 Runtime / ASP.NET and Web Development.

2. **Open Package Manager Console**: Go to Tools -> NuGet PackageManager -> Package Manager Console.

3. **Install NuGet Packages**: Install NuGet Packages with command `nuget install packages.config`.

4. **Setup Connection Strings for Mysql**: Update your  DefaultConnection in appsettings.json (configured for connecting with MySQL DBMS using entity framework, if you want to use other, make sure to install the corresponding packages)
 
5. **Run Initial Migrations**: Run `Add-Migration` and `Update-Database` in the Package Manager Console.

6. **Run the Server Locally**: Look for the port that the server is listening

## Making Requests in Postman

Follow these steps to make requests using Postman, after having the server running locally:

1. **Launch Postman**: If you haven't already, download and install [Postman](https://www.postman.com/) on your computer.

2. **Import Postman Collection**:
   - Open Postman.
   - Click the "Import".
   - Import `taskmanager.postman_collection.json` from `postmancollection` folder on this repository.
   - Adjust the url to the port that the server is running and.

3. **Configure the Request**:
   - Adjust the url from the Postman Collection imports to the port that the server is running. 

4. **Send the Request**:
   - Click the "Send" button to send the request to the API.

5. **View Response**:
   - The API's response will be displayed in the response panel below. You will see the result or an error message if the input is invalid.

## Methods Supported by the API

### Tarefas API (Tasks)

- `GET api/TarefasApi/GetTarefas`: returns all tasks from database.
- `GET api/TarefasApi/GetTarefaById/{id}`: searches for specific task by id.
- `GET api/TarefasApi/GetTarefaByUsuarioId/{idUsuario}`: serchs for specific task by idUsuario (tasks from User).
- `POST api/TarefasApi/CreateTarefa`: creates task with the data in body (see postman collection).
- `PUT api/TarefasApi/UpdateTarefa`: updates task with the data in body (see postman collection).
- `PUT api/TarefasApi/UpdateEstadoTarefa`: updates task's state with the data in body (see postman collection).
- `DELETE api/TarefasApi/DeleteTarefa/{id}`: deletes task by id.

### Usuarios API (Users)

- `GET api/UsuariosApi/GetUsuarios`: returns all users from database.
- `GET api/UsuariosApi/GetUsuarioById/{id}`: searches for specific user by id.
- `POST api/UsuariosApi/CreateUsuario`: creates user with the data in body (see postman collection).
- `PUT api/UsuariosApi/UpdateUsuario`: updates user with the data in body (see postman collection).
- `DELETE api/UsuariosApi/DeleteUsuario/{id}`: deletes user by id. 

## Updating Tasks State
- `Estado` is an enum:

 ```csharp
enum Estado
{
    NAO_INICIADA,
    EM_PROGRESSO,
    FINALIZADA,
    ARQUIVADA
}
```

- From the "NAO_INICIADA" (NOT STARTED) state, the user can change the task's state directly to either "EM_PROGRESSO" (IN PROGRESS) or "FINALIZADA" (COMPLETED).
- From the "EM_PROGRESSO" (IN PROGRESS) state, the user can change the task's state directly to either "NAO_INICIADA" (NOT STARTED) or FINALIZADA" (COMPLETED).
- From the "FINALIZADA" (COMPLETED) state, the user cannot change the task's state.
- In any of the "NAO_INICIADA" (NOT STARTED), "EM_PROGRESSO" (IN PROGRESS), or "FINALIZADA" (COMPLETED) states, the user can change the state to "ARQUIVADA" (ARCHIVED) using the "UpdateEstadoTarefa" API method.





