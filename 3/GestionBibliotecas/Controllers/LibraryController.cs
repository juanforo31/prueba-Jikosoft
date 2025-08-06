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

        private readonly LibraryBO _library;

        public LibraryController(LibraryBO service)
        {
            _library = service;
        }

        /// <summary>
        /// Añade una biblioteca.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> addLibrary([FromBody] LibraryRequest libraryRequest)
        {
            try
            {
                int resultado = await _library.AddLibrary(libraryRequest);
                return Ok($"Biblioteca agregada correctamente con el identificador " + resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las bibliotecas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetLibraries()
        {
            try
            {
                var resultado = await _library.GetAllLibrerires();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
