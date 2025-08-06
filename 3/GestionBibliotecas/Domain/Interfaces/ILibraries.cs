using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Library;

namespace GestionBibliotecas.Domain.Interfaces
{
    public interface ILibraries
    {
        /// <summary>
        /// Obtiene una biblioteca por su identificador.
        /// </summary>
        Task<LibraryResponse?> GetLibrary(LibraryRequest libraryRequest);
        /// <summary>
        /// Obtiene todas las bibliotecas.
        /// </summary>
        Task<IEnumerable<LibraryResponse>> GetAllLibrerires();
        /// <summary>
        /// Adiciona una nueva biblioteca.
        /// </summary>
        Task<int> AddLibrary(LibraryRequest libraryRequest);
        /// <summary>
        /// Actualiza la información de la biblioteca
        /// </summary>
        bool UpdateLibrary(LibraryRequest libraryRequest);
        /// <summary>
        /// Elimina una biblioteca.
        /// </summary>
        bool DeleteLibrary(int libraryId);
    }
}
