using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Application.DTOs.Users;
using GestionBibliotecas.Domain.Interfaces;
using GestionBibliotecas.Infrastructure.MemoryData;

namespace GestionBibliotecas.Application.Bussiness
{
    public class UserBO : IUsers
    {
        private LibraryBO _library;
        private InMemoryDataStore _store;

        // Constructor
        public UserBO(InMemoryDataStore store, LibraryBO library)
        {
            _store = store;
            _library = library;

            // Se crea un usuario por defecto para pruebas
            UserResponse newUser = new UserResponse();
            newUser.UserId = 1;
            newUser.FirstName = "Juan";
            newUser.LastName = "Forero";
            newUser.Email = "juandafo31@gmail.com";
            newUser.PhoneNumber = "3165062308";
            newUser.LibraryId = 1; // Asignando a la biblioteca por defecto

            _store.Users.Add(newUser);
        }
        public async Task<int> AddUser(NewUserRequest newUserRequest)
        {
            // Como identificador se esta usando un número random
            Random random = new Random();
            int userId = random.Next();

            // Validaciones si ya existe el usuario
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
                    // Si no existe el usuario se crea
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
                // Si ya existe el usuario, se retorna 0
                userId = 0;
            }
            return await Task.FromResult(userId);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            // Se busca un usuario por su identificador
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

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            // Retorna la lista de usuarios
            return await Task.FromResult(_store.Users);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersByLibrary(int libraryId)
        {

            List<UserResponse> users;

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
                users = _store.Users.Where(us =>
                us.UserId == libraryId
                ).ToList();

            }
            else
            {
                users = [];
            }

            return await Task.FromResult((users));
        }



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

        public async Task<bool> UpdateLibraryUser(int userId, int libraryId)
        {
            bool isModUser = false;

            // Validaciones si ya existe el usuario
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
                // Si existe el usuario se actualiza
                findUser.LibraryId = libraryId;

                isModUser = true;
            }

            return isModUser;
        }

        public async Task<bool> UpdateUser(int userId, ModUserRequest modUserRequest)
        {
            bool isModUser = false;

            // Validaciones si ya existe el usuario
            UserResponse findUser = new UserResponse();
            bool existsUser;
            (findUser, existsUser) = await GetUserById(userId);

            if (existsUser)
            {
                // Si existe el usuario se actualiza
                findUser.FirstName = modUserRequest.FirstName;
                findUser.LastName = modUserRequest.LastName;
                findUser.Email = modUserRequest.Email;
                findUser.PhoneNumber = modUserRequest.PhoneNumber;

                isModUser = true;
            }

            return isModUser;
        }

        private async Task<(UserResponse?, bool)> GetUserById(int userId)
        {
            // Busca un usuario en la lista de usuarios por su identificador
            UserResponse? user = _store.Users.FirstOrDefault(us =>
            us.UserId == userId
            );

            bool existUser = user != null;

            return await Task.FromResult((user, existUser));
        }
    }
}
