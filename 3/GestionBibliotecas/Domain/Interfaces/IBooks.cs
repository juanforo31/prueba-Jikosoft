using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Books;

namespace GestionBibliotecas.Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones relacionadas con los libros en la biblioteca.
    /// </summary>
    public interface IBooks
    {
        /// <summary>
        /// Obtiene un libro dado un filtro.
        /// </summary>
        Task<(IEnumerable<BookResponse?>,bool)> GetBook(BookRequest bookRequest);
        /// <summary>
        /// Obtiene todos los libros.
        /// </summary>
        Task<IEnumerable<BookResponse>> GetAllBooks();
        /// <summary>
        /// Obtiene todos los libros de una biblioteca en especifico.
        /// </summary>
        Task<IEnumerable<BookResponse>> GetAllBooksByLibrary(int LibraryId);
        /// <summary>
        /// Adicioona un nuevo libro.
        /// </summary>
        Task<int> AddBook(NewBookRequest newBookRequest);
        /// <summary>
        /// Actualiza el estado del libro
        /// </summary>
        Task<bool> UpdateBook(int bookId,ModBookRequest modBookRequest);

        /// <summary>
        /// Vincular el libro a un usuario.
        /// </summary>
        Task<bool> RentBook(int bookId, int userId);

        /// <summary>
        /// Hacer ela devolución del libro a la biblioteca.
        /// </summary>
        Task<bool> ReturnBook(int bookId);

        /// <summary>
        /// Elimina un libro.
        /// </summary>
        Task<bool> DeleteBook(int id);
    }
}
