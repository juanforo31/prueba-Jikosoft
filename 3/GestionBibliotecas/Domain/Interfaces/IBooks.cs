using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Books;

namespace GestionBibliotecas.Domain.Interfaces
{
    public interface IBooks
    {
        /// <summary>
        /// Obtiene un libro dado un filtro.
        /// </summary>
        BookResponse GetBook(BookRequest bookRequest);
        /// <summary>
        /// Obtiene todos los libros.
        /// </summary>
        IEnumerable<BookResponse> GetAllBooks();
        /// <summary>
        /// Obtiene todos los libros de una biblioteca en especifico.
        /// </summary>
        IEnumerable<BookResponse> GetAllBooksByLibrary(int LibraryId);
        /// <summary>
        /// Adicioona un nuevo libro.
        /// </summary>
        int AddBook(BookResponse book);
        /// <summary>
        /// Actualiza el estado del libro
        /// </summary>
        void UpdateBook(BookResponse book);
        /// <summary>
        /// Elimina un libro.
        /// </summary>
        void DeleteBook(int id);
    }
}
