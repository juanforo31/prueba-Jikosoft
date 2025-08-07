using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Application.DTOs.Books;
using GestionBibliotecas.Domain.Interfaces;
using GestionBibliotecas.Infrastructure.MemoryData;

namespace GestionBibliotecas.Application.Bussiness
{
    /// <summary>
    /// Esta clase tiene toda la lógica de negocio para los libros
    /// </summary>
    public class UserBO : IUsers
    {
        private LibraryBO _library;
        private InMemoryDataStore _store;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Clase que permite tener todos los datos de las listas en memoria</param>
        /// <param name="library">Clase para el uso de la lógica de negocio</param>
        public UserBO(InMemoryDataStore store, LibraryBO library)
        {
            _store = store;
            _library = library;

            // Se crea un miembro por defecto para pruebas
            UserResponse newUser = new UserResponse();
            newUser.UserId = 1;
            newUser.FirstName = "Juan";
            newUser.LastName = "Forero";
            newUser.Email = "juandafo31@gmail.com";
            newUser.PhoneNumber = "3165062308";
            newUser.LibraryId = 1; // Asignando a la biblioteca por defecto

            _store.Users.Add(newUser);
        }
        /// <summary>
        /// Añade un miembro a la lista de libros
        /// </summary>
        /// <param name="newUserRequest">Datos del miembro a crear</param>
        /// <returns>Devuelve el identificador del miembro creado</returns>
        public async Task<int> AddUser(NewUserRequest newUserRequest)
        {
            // Cómo identificador se esta usando un número random
            Random random = new Random();
            int userId = random.Next();

            // Validaciones si ya existe el miembro
            var findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await GetUserById(userId);

            if (!existsUser)
            {

                // Validaciones si ya existe la biblioteca
                LibraryResponse findLibrary = new LibraryResponse();
                bool existsLibrary;
                LibraryRequest libraryRequest = new LibraryRequest
                {
                    LibraryId = newUserRequest.LibraryId
                };
                (findLibrary, existsLibrary) = await _library.GetLibrary(libraryRequest);

                if (existsLibrary)
                {
                    // Si no existe, el miembro se crea
                    // Validar que el identificador no sea igual a otro
                    UserResponse newUser = new UserResponse
                    {
                        UserId = userId,
                        FirstName = newUserRequest.FirstName,
                        LastName = newUserRequest.LastName,
                        Email = newUserRequest.Email,
                        PhoneNumber = newUserRequest.PhoneNumber,
                        LibraryId = newUserRequest.LibraryId

                    };

                    _store.Users.Add(newUser);
                }
                else
                {
                    userId = 0;

                }
            }
            else
            {
                // Si ya existe el miembro, se retorna 0
                userId = 0;
            }
            return await Task.FromResult(userId);
        }
        /// <summary>
        /// Elimina un miembro de la lista
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Devuelve una validación de éxito</returns>
        public async Task<bool> DeleteUser(int userId)
        {
            // Se busca un miembro por su identificador
            bool isDelUser = false;
            var findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await GetUserById(userId);

            if (existsUser)
            {
                _store.Users.Remove(findUser);
                isDelUser = true;
            }

            return await Task.FromResult(isDelUser);
        }
        /// <summary>
        /// Trae todos los libros creados
        /// </summary>
        /// <returns>Una lista de todos los libros creados</returns>
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            // Retorna la lista de libros
            return await Task.FromResult(_store.Users);
        }
        /// <summary>
        /// Obtiene todos los libros que tiene una biblioteca
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns>Retorna todos los libros que hay en una biblioteca en especifico</returns>
        public async Task<IEnumerable<UserResponse>> GetAllUsersByLibrary(int libraryId)
        {

            List<UserResponse> Books;

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
                Books = _store.Users.Where(us =>
                us.LibraryId == libraryId
                ).ToList();

            }
            else
            {
                Books = [];
            }

            return await Task.FromResult(Books);
        }
        /// <summary>
        /// Obtener uno o varios libros dado un filtro
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns>Retonra un lista o un miembro debido a un filtro</returns>
        public async Task<(IEnumerable<UserResponse>, bool)> GetUser(UserRequest userRequest)
        {
            var user = _store.Users.Where(us =>
            us.UserId == userRequest.UserId
            || us.FirstName.ToLower() == userRequest.FirstName.ToLower()
            || us.LastName.ToLower() == userRequest.LastName.ToLower()
            || us.Email.ToLower() == userRequest.Email.ToLower()
            || us.PhoneNumber == userRequest.PhoneNumber.ToLower()
            ).ToList();

            bool existUser = user != null;

            return await Task.FromResult((user, existUser));
        }
        /// <summary>
        /// realizar el traslado de un usuario a otra biblioteca
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="libraryId"></param>
        /// <returns>Devuelve un valor de éxito o no</returns>
        public async Task<bool> UpdateLibraryUser(int userId, int libraryId)
        {
            bool isModUser = false;

            // Validaciones si ya existe el miembro
            UserResponse findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await GetUserById(userId);

            // Validaciones si ya existe la biblioteca
            LibraryResponse findLibrary = new LibraryResponse();
            bool existsLibrary;
            LibraryRequest libraryRequest = new LibraryRequest
            {
                LibraryId = libraryId
            };
            (findLibrary, existsLibrary) = await _library.GetLibrary(libraryRequest);

            if (existsUser && existsLibrary)
            {
                // Si existe, el miembro se actualiza
                findUser.LibraryId = libraryId;

                isModUser = true;
            }

            return isModUser;
        }
        /// <summary>
        /// Actualiza la información basica del miembro
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="modUserRequest"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(int userId, ModUserRequest modUserRequest)
        {
            bool isModUser = false;

            // Validaciones si ya existe el miembro
            UserResponse findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await GetUserById(userId);

            if (existsUser)
            {
                // Si existe, el miembro se actualiza
                findUser.FirstName = modUserRequest.FirstName;
                findUser.LastName = modUserRequest.LastName;
                findUser.Email = modUserRequest.Email;
                findUser.PhoneNumber = modUserRequest.PhoneNumber;

                isModUser = true;
            }

            return isModUser;
        }
        /// <summary>
        /// Obtiene un miembro por su identificador
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(UserResponse?, bool)> GetUserById(int userId)
        {
            // Busca un miembro en la lista de libros por su identificador
            UserResponse? user = _store.Users.FirstOrDefault(us =>
            us.UserId == userId
            );

            bool existUser = user != null;

            return await Task.FromResult((user, existUser));
        }
    }
}
