using GestionBibliotecas.Application.Bussiness;
using GestionBibliotecas.Application.DTOs.Books;
using GestionBibliotecas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionBibliotecas.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los libros de la biblioteca.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    
    public class BookController: ControllerBase
    {
        // Se genera este objeto con su respectivo constructor, ya que para esta ocación es necesario hacer uso de un Singleton para mantener la información en momoria, al no tener DB
        // Se puede ver el Singleton implementado en Program.cs
        private readonly BooksBO _book;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public BookController(BooksBO service)
        {
            _book = service;
        }

        /// <summary>
        /// Obtiene todos los libros de todas las bibliotecas.
        /// </summary>
        [HttpGet]
        [Route("get-books")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var resultado = await _book.GetAllBooks();
                if (resultado == null || !resultado.Any())
                {
                    return Ok("No se han encontrado libros");
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
        /// Obtiene los libros un dado un filtro.
        /// </summary>
        [HttpPost]
        [Route("get-book-filter")]
        public async Task<IActionResult> GetBooksByFilter([FromBody] BookRequest bookRequest)
        {
            try
            {

                var resultado = await _book.GetBook(bookRequest);
                var book = resultado.Item1;
                bool existsBook = resultado.Item2;
                if (existsBook)
                {
                    return Ok(book);
                }
                else
                {
                    return Ok("No se ha encontrado ningún miembro con los datos proporcionados");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los libros una biblioteca en específico.
        /// </summary>
        [HttpGet]
        [Route("get-book-library/{libraryId}")]
        public async Task<IActionResult> GetBooksByLibrary(int libraryId)
        {
            try
            {

                var resultado = await _book.GetAllBooksByLibrary(libraryId);
                if (resultado == null || !resultado.Any())
                {
                    return Ok("Ha ocurrido un error encontrando la biblioteca o el libro");
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
        /// Obtiene el libro dado su id.
        /// </summary>
        /// <param name="bookId">Identificador del libro</param>
        [HttpGet]
        [Route("get-book-id/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            try
            {

                var resultado = await _book.GetBookById(bookId);
                var book = resultado.Item1;
                bool existsBook = resultado.Item2;
                if (!existsBook)
                {
                    return Ok("Error al encontrar el libro");
                }
                else
                {
                    return Ok(book);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Añade un libro.
        /// </summary>
        [HttpPost]
        [Route("add-book")]
        public async Task<IActionResult> addBook([FromBody] NewBookRequest newBookRequest)
        {
            try
            {
                int resultado = await _book.AddBook(newBookRequest);

                if (resultado != 0)
                {
                    return Ok($"El libro ha sido agregado correctamente con el identificador " + resultado);
                }
                else
                {
                    return Ok("No se pudo crear el libro");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// modifica un libro.
        /// </summary>
        [HttpPut]
        [Route("update-book/{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] ModBookRequest modBookRequest)
        {
            try
            {
                bool resultado = await _book.UpdateBook(bookId, modBookRequest);

                if (resultado)
                {
                    return Ok("El libro se ha modificado con éxito");
                }
                else
                {
                    return Ok("No se pudo modificar el libro");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Se renta un libro a un usuario
        /// </summary>
        [HttpPut]
        [Route("rent-book-user/{userId}/{bookId}")]
        public async Task<IActionResult> rentBook(int userId, int bookId)
        {
            try
            {
                bool resultado = await _book.RentBook(userId, bookId);

                if (resultado)
                {
                    return Ok("Se ha realizado la renta del libro de manera exitosa");
                }
                else
                {
                    return Ok($"No se pudo realizar la renta del libro " + bookId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Se devuelve un libro a la biblioteca
        /// </summary>
        [HttpPut]
        [Route("return-book-user/{bookId}")]
        public async Task<IActionResult> returnBook(int userId, int bookId)
        {
            try
            {
                bool resultado = await _book.ReturnBook(bookId);

                if (resultado)
                {
                    return Ok("Se ha realizado devuelto el libro de manera exitosa");
                }
                else
                {
                    return Ok($"No se pudo realizar la devoluci devoluciòn del libro " + bookId);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Eliminar un libro.
        /// </summary>
        [HttpDelete]
        [Route("delete-/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                bool resultado = await _book.DeleteBook(bookId);

                if (resultado)
                {
                    return Ok("El libro se ha eliminado con éxito");
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
