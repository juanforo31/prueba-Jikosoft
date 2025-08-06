using GestionBibliotecas.Application.DTOs.Users;

namespace GestionBibliotecas.Domain.Interfaces
{
    public interface IUsers
    {
        /// <summary>
        /// Obtiene un miembro según filtro.
        /// </summary>
        UserResponse GetUser(UserRequest userRequest);
        /// <summary>
        /// Obtiene todos los miembros.
        /// </summary>
        IEnumerable<UserResponse> GetAllUsers();
        /// <summary>
        /// Obtiene todos los usuarios de una libreria.
        /// </summary>
        IEnumerable<UserResponse> GetAllUsersByLibrary(int libraryId);
        /// <summary>
        /// Adiciona un miembro.
        /// </summary>
        int AddUser(UserResponse book);
        /// <summary>
        /// Actualiza la información del miembro.
        /// </summary>
        void UpdateUser(UserResponse book);
        /// <summary>
        /// Elimina un miembro.
        /// </summary>
        void DeleteUser(int id);
    }
}
