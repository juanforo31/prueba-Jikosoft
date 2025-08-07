using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Books;

namespace GestionBibliotecas.Domain.Interfaces
{
    public interface IBooks
    {
        /// <summary>
        /// Obtiene un libro dado un filtro.
        /// </summary>
        Task<(BookResponse?,bool)> GetBook(BookRequest bookRequest);
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
        /// Elimina un libro.
        /// </summary>
        Task<bool> DeleteBook(int id);
    }
}
