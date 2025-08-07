using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Books;
using GestionBibliotecas.Domain.Interfaces;

namespace GestionBibliotecas.Application.Bussiness
{
    public class BooksBO : IBooks
    {
        public Task<int> AddBook(NewBookRequest newBookRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookResponse>> GetAllBooksByLibrary(int LibraryId)
        {
            throw new NotImplementedException();
        }

        public Task<(BookResponse?, bool)> GetBook(BookRequest bookRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RentBook(int bookId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBook(int bookId, ModBookRequest modBookRequest)
        {
            throw new NotImplementedException();
        }
    }
}
