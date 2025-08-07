using GestionBibliotecas.Application.Bussiness;
using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Application.DTOs.Books;
using Microsoft.AspNetCore.Mvc;

namespace GestionBibliotecas.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los miembros de la biblioteca.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : ControllerBase
    {
        // Se genera este objeto con su respectivo constructor, ya que para esta ocación es necesario hacer uso de un Singleton para mantener la información en momoria, al no tener DB
        // Se puede ver el Singleton implementado en Program.cs
        private readonly UserBO _user;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>

        public UserController(UserBO user)
        {
            _user = user;
        }

        /// <summary>
        /// Obtiene todos los miembros de todas las bibliotecas.
        /// </summary>
        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var resultado = await _user.GetAllUsers();
                if (resultado == null || !resultado.Any())
                {
                    return Ok("No se han encontrado miembros");
                }
                else
                {

                    return Ok(resultado);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los miembros un dado un filtro.
        /// </summary>
        [HttpPost]
        [Route("get-user-filter")]
        public async Task<IActionResult> GetUsersByFilter([FromBody] UserRequest userRequest)
        {
            try
            {

                var resultado = await _user.GetUser(userRequest);
                var user = resultado.Item1;
                bool existsUser = resultado.Item2;
                if (existsUser)
                {
                    return Ok(user);
                }
                else
                {
                    return Ok("No se ha encontrado ningun mienbro con los datos proporcionados");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los miembros una biblioteca en específico.
        /// </summary>
        [HttpGet]
        [Route("get-user-library/{libraryId}")]
        public async Task<IActionResult> GetUsersByLibrary(int libraryId)
        {
            try
            {

                var resultado = await _user.GetAllUsersByLibrary(libraryId);
                if (resultado == null || !resultado.Any())
                {
                    return Ok("Ha ocurrido un error encontrando la biblioteca o el miembro");
                }
                else
                {
                    return Ok(resultado);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el usuario dado su id.
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        [HttpGet]
        [Route("get-user-id/{userId}")]
        public async Task<IActionResult> GetUsersById(int userId)
        {
            try
            {

                var resultado = await _user.GetUserById(userId);
                var user = resultado.Item1;
                bool existsUser = resultado.Item2;
                if (!existsUser)
                {
                    return Ok("Error al encontrar el miembro");
                }
                else
                {
                    return Ok(user);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Añade un miembro.
        /// </summary>
        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> addUser([FromBody] NewUserRequest newUserRequest)
        {
            try
            {
                int resultado = await _user.AddUser(newUserRequest);

                if (resultado != 0)
                {
                    return Ok($"Miembro agregado correctamente con el identificador " + resultado);
                }
                else
                {
                    return Ok("No se pudo crear el miembro");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// modifica un miembro.
        /// </summary>
        [HttpPut]
        [Route("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] ModUserRequest modUserRequest)
        {
            try
            {
                bool resultado = await _user.UpdateUser(userId, modUserRequest);

                if (resultado)
                {
                    return Ok("El miembro se ha modificado con éxito");
                }
                else
                {
                    return Ok("No se pudo modificar el miembro");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// modifica un la biblioteca de un miembro.
        /// </summary>
        [HttpPut]
        [Route("update-user-library/{userId}/{libraryId}")]
        public async Task<IActionResult> UpdateUserLibrary(int userId, int libraryId)
        {
            try
            {
                bool resultado = await _user.UpdateLibraryUser(userId, libraryId);

                if (resultado)
                {
                    return Ok("Se ha realizado el tralado del miembro satisfactoriamente");
                }
                else
                {
                    return Ok($"No se pudo trasladar el usuario a la biblioteca con identificación" + libraryId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Eliminar un miembro.
        /// </summary>
        [HttpDelete]
        [Route("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                bool resultado = await _user.DeleteUser(userId);

                if (resultado)
                {
                    return Ok("El miembro se ha eliminado con éxito");
                }
                else
                {
                    return Ok("No se pudo eliminar el mimebro");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
