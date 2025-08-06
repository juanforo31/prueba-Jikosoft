using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Domain.Interfaces;

namespace GestionBibliotecas.Application.Bussiness
{
    public class LibraryBO : ILibraries
    {
        private List<LibraryResponse> LibrariesList = new List<LibraryResponse>();

        public LibraryBO()
        {
            LibrariesList = new List<LibraryResponse>();

            LibraryResponse a = new LibraryResponse();
            a.LibraryId = 1;
            a.LibraryName = "Uno";
            a.Address = "";
            a.City = "";

            LibrariesList.Add(a);
        }

        public async Task<IEnumerable<LibraryResponse>> GetAllLibrerires()
        {

            return await Task.FromResult(LibrariesList);
        }

        public async Task<LibraryResponse?> GetLibrary(LibraryRequest libraryRequest)
        {
            LibraryResponse? library = LibrariesList.FirstOrDefault(lib =>
            lib.LibraryId == libraryRequest.LibraryId
            || lib.LibraryName == libraryRequest.LibraryName
            || lib.Address == libraryRequest.Address
            || lib.City == libraryRequest.City);

            return await Task.FromResult(library);
        }
        public async Task<int> AddLibrary(LibraryRequest libraryRequest)
        {
            Random random = new Random();
            int id = random.Next();

            try
            {
                LibraryResponse newLibrary = new LibraryResponse
                {

                    LibraryId = id,
                    LibraryName = libraryRequest.LibraryName,
                    Address = libraryRequest.Address,
                    City = libraryRequest.City,

                };

                LibrariesList.Add(newLibrary);

                return await Task.FromResult(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteLibrary(int libraryId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLibrary(LibraryRequest libraryRequest)
        {
            throw new NotImplementedException();
        }
    }
}
