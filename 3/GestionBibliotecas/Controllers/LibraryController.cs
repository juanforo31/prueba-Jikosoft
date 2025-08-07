using GestionBibliotecas.Application.Bussiness;
using GestionBibliotecas.Application.DTOs.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionBibliotecas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        // Se genera este objeto con su respectivo constructor, ya que para esta ocación es necesario hacer uso de un Singleton para mantener la información en momoria, al no tener DB
        // Se puede ver el Singleton implementado en Program.cs

        private readonly LibraryBO _library;

        public LibraryController(LibraryBO service)
        {
            _library = service;
        }

        /// <summary>
        /// Obtiene todas las bibliotecas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getLibraries")]
        public async Task<IActionResult> GetLibraries()
        {
            try
            {
                var resultado = await _library.GetAllLibrerires();
                if (resultado == null || !resultado.Any())
                {
                    return Ok("No se han encontrado bibliotecas");
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
        /// Obtiene una biblioteca dad un filtro.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getLibrary")]
        public async Task<IActionResult> GetLibrary([FromBody] LibraryRequest libraryRequest)
        {
            try
            {

                var resultado = await _library.GetLibrary(libraryRequest);
                var library = resultado.Item1;
                bool existsLibrary = resultado.Item2;
                if (existsLibrary)
                {
                    return Ok(library);
                }
                else
                {
                    return Ok("No se ha encontrado nínguna biblioteca con los datos proporcionados");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Añade una biblioteca.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> addLibrary([FromBody] NewLibraryRequest newlibraryRequest)
        {
            try
            {
                int resultado = await _library.AddLibrary(newlibraryRequest);

                if (resultado != 0)
                {
                    return Ok($"Biblioteca agregada correctamente con el identificador " + resultado);
                }
                else
                {
                    return Ok("No se pudo crear la biblioteca");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// modifica una biblioteca.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("updateLibrary/{libraryId}")]
        public async Task<IActionResult> UpdateLibrary(int libraryId, [FromBody] ModLibraryRequest modLibraryRequest)
        {
            try
            {
                bool resultado = await _library.UpdateLibrary(libraryId, modLibraryRequest);

                if (resultado)
                {
                    return Ok("La biblioteca se ha modificado con éxito");
                }
                else
                {
                    return Ok("No se pudo modificar la biblioteca");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Eliminar una biblioteca.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteLibrary/{libraryId}")]
        public async Task<IActionResult> DeleteLibrary(int libraryId)
        {
            try
            {
                bool resultado = await _library.DeleteLibrary(libraryId);

                if (resultado)
                {
                    return Ok("La biblioteca se ha eliminado con éxito");
                }
                else
                {
                    return Ok("No se pudo eliminar la biblioteca");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
