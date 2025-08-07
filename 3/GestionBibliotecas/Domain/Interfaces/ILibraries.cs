using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Library;

namespace GestionBibliotecas.Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones relacionadas con las bibliotecas.
    /// </summary>
    public interface ILibraries
    {
        /// <summary>
        /// Obtiene una biblioteca por su identificador.
        /// </summary>
        Task<(LibraryResponse?, bool)> GetLibrary(LibraryRequest libraryRequest);
        /// <summary>
        /// Obtiene todas las bibliotecas.
        /// </summary>
        Task<IEnumerable<LibraryResponse>> GetAllLibrerires();
        /// <summary>
        /// Adiciona una nueva biblioteca.
        /// </summary>
        Task<int> AddLibrary(NewLibraryRequest NewlibraryRequest);
        /// <summary>
        /// Actualiza la información de la biblioteca
        /// </summary>
        Task<bool> UpdateLibrary(int libraryId,ModLibraryRequest modLibraryRequest);
        /// <summary>
        /// Elimina una biblioteca.
        /// </summary>
        Task<bool> DeleteLibrary(int libraryId);
    }
}
