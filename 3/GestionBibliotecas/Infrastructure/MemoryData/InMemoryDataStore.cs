using GestionBibliotecas.Application.Bussiness;
using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Application.DTOs.Books;

namespace GestionBibliotecas.Infrastructure.MemoryData
{
    /// <summary>
    /// Clase referente para almacenar datos en memoria.
    /// </summary>
    public class InMemoryDataStore
    {
        public List<LibraryResponse> Libreries { get; set; } = new();
        public List<UserResponse> Users { get; set; } = new();
        public List<BookResponse> Books { get; set; } = new();

    }
}
