using GestionBibliotecas.Application.DTOs.Users;

namespace GestionBibliotecas.Domain.Interfaces
{
    public interface IUsers
    {

        /// <summary>
        /// Obtiene un miembro según filtro.
        /// </summary>
        Task<(IEnumerable<UserResponse>, bool)> GetUser(UserRequest userRequest);

        /// <summary>
        /// Obtiene todos los miembros.
        /// </summary>
        Task<IEnumerable<UserResponse>> GetAllUsers();
        /// <summary>
        /// Obtiene todos los usuarios de una libreria.
        /// </summary>
        Task<IEnumerable<UserResponse>> GetAllUsersByLibrary(int libraryId);
        /// <summary>
        /// Adiciona un miembro.
        /// </summary>
        Task<int> AddUser(NewUserRequest newUserRequest);
        /// <summary>
        /// Actualiza la información del miembro.
        /// </summary>
        Task<bool> UpdateUser(int userId, ModUserRequest modUserRequest);

        /// <summary>
        /// Cambiar usuario de biblioteca.
        /// </summary>
        Task<bool> UpdateLibraryUser(int userId, int libraryId);

        /// <summary>
        /// Elimina un miembro.
        /// </summary>
        Task<bool> DeleteUser(int id);
    }
}
