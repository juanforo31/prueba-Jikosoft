using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Books;
using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Domain.Interfaces;
using GestionBibliotecas.Infrastructure.MemoryData;

namespace GestionBibliotecas.Application.Bussiness
{
    /// <summary>
    /// Clase que tiene toda la logica de negocio de los libtos
    /// </summary>
    public class BooksBO : IBooks
    {
        private LibraryBO _library;
        private UserBO _book;
        private InMemoryDataStore _store;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Clase que permite tener todos los datos de las listas en memoria</param>
        /// <param name="library">Clase para el uso de la lógica de negociod e las librerias</param>
        /// <param name="user">Clase para el uso de la lógica de negocio de los usuarios</param>
        public BooksBO(InMemoryDataStore store, LibraryBO library, UserBO user)
        {
            _store = store;
            _library = library;
            _book = user;

            // Se crea un libro por defecto para pruebas
            BookResponse newBook = new BookResponse();
            newBook.BookId = 1;
            newBook.Title = "Origen";
            newBook.Author = "Dan Brown";
            newBook.ISBN = "9787020138746";
            newBook.PublishedYear = 2017;
            newBook.LibraryId = 1; // Asignando a la biblioteca por defecto

            _store.Books.Add(newBook);
        }
        /// <summary>
        /// Se añade un libro a la lista
        /// </summary>
        /// <param name="newBookRequest">Datos del libro a crear</param>
        /// <returns>Devuelve el indentificador del libro creado</returns>
        public async Task<int> AddBook(NewBookRequest newBookRequest)
        {
            // Cómo identificador se esta usando un número random
            Random random = new Random();
            int bookId = random.Next();

            // Validaciones si ya existe el libro
            var findBook = new BookResponse();
            bool existsBook;
            (findBook, existsBook) = await GetBookById(bookId);

            if (!existsBook)
            {

                // Validaciones si ya existe la biblioteca
                LibraryResponse findLibrary = new LibraryResponse();
                bool existsLibrary;
                LibraryRequest libraryRequest = new LibraryRequest
                {
                    LibraryId = newBookRequest.libraryId
                };
                (findLibrary, existsLibrary) = await _library.GetLibrary(libraryRequest);

                if (existsLibrary)
                {
                    // Si no existe, el libro se crea
                    // Validar que el identificador no sea igual a otro
                    BookResponse newBook = new BookResponse
                    {
                        BookId = bookId,
                        Title = newBookRequest.Title,
                        Author = newBookRequest.Author,
                        ISBN = newBookRequest.ISBN,
                        PublishedYear = newBookRequest.PublishedYear,
                        LibraryId = newBookRequest.libraryId

                    };

                    _store.Books.Add(newBook);
                }
                else
                {
                    bookId = 0;

                }
            }
            else
            {
                // Si ya existe el libro, se retorna 0
                bookId = 0;
            }
            return await Task.FromResult(bookId);
        }
        /// <summary>
        /// Elimina un libro de la lista
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns>Devuelve una validación de éxito</returns>
        public async Task<bool> DeleteBook(int bookid)
        {
            // Se busca un libro por su identificador
            bool isDelBook = false;
            var findBook = new BookResponse();
            bool existsBook;
            (findBook, existsBook) = await GetBookById(bookid);

            if (existsBook)
            {
                _store.Books.Remove(findBook);
                isDelBook = true;
            }

            return await Task.FromResult(isDelBook);
        }
        /// <summary>
        /// Trae todos los libros creados
        /// </summary>
        /// <returns>Una lista de todos los libros creados</returns>
        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            // Retorna la lista de libros
            return await Task.FromResult(_store.Books);
        }
        /// <summary>
        /// Obtiene todos los libros que tiene una biblioteca
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns>Retorna todos los libros que hay en una biblioteca en especifico</returns>
        public async Task<IEnumerable<BookResponse>> GetAllBooksByLibrary(int libraryId)
        {
            List<BookResponse> books;

            // Validaciones si ya existe la biblioteca
            LibraryResponse findLibrary = new LibraryResponse();
            bool existsLibrary;
            LibraryRequest libraryRequest = new LibraryRequest
            {
                LibraryId = libraryId
            };

            (findLibrary, existsLibrary) = await _library.GetLibrary(libraryRequest);

            if (existsLibrary)
            {
                books = _store.Books.Where(bo =>
                bo.LibraryId == libraryId
                ).ToList();

            }
            else
            {
                books = [];
            }

            return await Task.FromResult(books);
        }
        /// <summary>
        /// Obtener un o varios libros dado un filtro
        /// </summary>
        /// <param name="bookRequest"></param>
        /// <returns>Retonra un lista o un libro debido a un filtro</returns>
        public async Task<(IEnumerable<BookResponse?>, bool)> GetBook(BookRequest bookRequest)
        {
            var book = _store.Books.Where(bo =>
            bo.BookId == bookRequest.BookId
            || bo.Title.ToLower() == bookRequest.Title.ToLower()
            || bo.Author.ToLower() == bookRequest.Author.ToLower()
            || bo.ISBN.ToLower() == bookRequest.ISBN.ToLower()
            ).ToList();

            bool existBook = book != null;

            return await Task.FromResult((book, existBook));
        }
        /// <summary>
        /// realizar la reta de un libro, siempre y cuando este disposible 
        /// </summary>
        /// <param name="bookId">Identificador del libro</param>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Devuelve un valor de éxito o no</returns>
        public async Task<bool> RentBook(int bookId, int userId)
        {
            bool isModStatus = false;

            // Validaciones si ya existe el miembro
            UserResponse findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await _book.GetUserById(userId);

            // Validaciones información del libro
            BookResponse findBook = new BookResponse();
            bool existsBook;
            (findBook, existsBook) = await GetBookById(bookId);

            if (existsUser && findBook.UserId ==  0)
            {
                // Si existe el miembro se actualiza
                findBook.UserId = userId;

                isModStatus = true;
            }

            return isModStatus;
        }
        /// <summary>
        /// Se devuelve el libro a la biblioteca
        /// </summary>
        /// <param name="bookId">Indeitficador del libro a devolver</param>
        /// <returns>Validacion de si fue exitoso</returns>
        public async Task<bool> ReturnBook(int bookId)
        {
            bool isModStatus = false;

            // Validaciones si el libro esta disponible
            BookResponse findBook = new BookResponse();
            bool existsBook;
            (findBook, existsBook) = await GetBookById(bookId);

            if ( findBook.UserId != 0)
            {
                // Si existe, el miembro se actualiza
                findBook.UserId = 0;

                isModStatus = true;
            }

            return isModStatus;
        }
        /// <summary>
        /// Actualiza la información del libro
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="modBookRequest"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBook(int bookId, ModBookRequest modBookRequest)
        {
            bool isModBook = false;

            // Validaciones si ya existe el miembro
            BookResponse findBook = new BookResponse();
            bool existsBook;
            (findBook, existsBook) = await GetBookById(bookId);

            if (existsBook)
            {
                // Si existe, el miembro se actualiza
                findBook.Title = modBookRequest.Title;
                findBook.Author = modBookRequest.Author;
                findBook.ISBN = modBookRequest.ISBN;
                findBook.PublishedYear = modBookRequest.PublishedYear;

                isModBook = true;
            }

            return isModBook;
        }
        /// <summary>
        /// Obtiene un libro por su identificador
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<(BookResponse?, bool)> GetBookById(int bookId)
        {
            // Busca un libro en la lista de libros por su identificador
            BookResponse? book = _store.Books.FirstOrDefault(bo =>
            bo.BookId == bookId
            );

            bool existBook = book != null;

            return await Task.FromResult((book, existBook));
        }
    }
}
