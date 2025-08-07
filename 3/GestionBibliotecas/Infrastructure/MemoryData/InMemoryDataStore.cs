using GestionBibliotecas.Application.Bussiness;
using GestionBibliotecas.Application.DTOs;
using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Application.DTOs.Users;

namespace GestionBibliotecas.Infrastructure.MemoryData
{
    public class InMemoryDataStore
    {
        public List<LibraryResponse> Libreries { get; set; } = new();
        public List<UserResponse> Users { get; set; } = new();
        public List<BookResponse> Books { get; set; } = new();

    }
}
