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

            LibraryResponse newLibrary = new LibraryResponse();
            newLibrary.LibraryId = 1;
            newLibrary.LibraryName = "Uno";
            newLibrary.Address = "";
            newLibrary.City = "";

            LibrariesList.Add(newLibrary);
        }

        public async Task<IEnumerable<LibraryResponse>> GetAllLibrerires()
        {
            return await Task.FromResult(LibrariesList);
        }

        public async Task<(LibraryResponse?, bool)> GetLibrary(LibraryRequest libraryRequest)
        {

            LibraryResponse? library = LibrariesList.FirstOrDefault(lib =>
            lib.LibraryId == libraryRequest.LibraryId
            || lib.LibraryName == libraryRequest.LibraryName
            || lib.Address == libraryRequest.Address
            );

            bool existLibrary = library != null;

            return await Task.FromResult((library, existLibrary));
        }
        public async Task<int> AddLibrary(NewLibraryRequest newlibraryRequest)
        {
            // Como identificador se esta usando un número random
            Random random = new Random();
            int id = random.Next();

            LibraryRequest library = new LibraryRequest
            {
                LibraryId = id,
                LibraryName = newlibraryRequest.LibraryName,
                Address = newlibraryRequest.Address,
            };

            // Validaciones si ya existe la biblioteca
            LibraryResponse findLibrary = new LibraryResponse();
            bool existsLibrary;
            (findLibrary, existsLibrary) = await GetLibrary(library);

            if (!existsLibrary)
            {
                // Si no existe la biblioteca se crea
                // Validar que el identificador no sea igual a otro
                LibraryResponse newLibrary = new LibraryResponse
                {

                    LibraryId = id,
                    LibraryName = newlibraryRequest.LibraryName,
                    Address = newlibraryRequest.Address,
                    City = newlibraryRequest.City,

                };

                LibrariesList.Add(newLibrary);


            }
            else
            {
                id = 0;
            }
            return await Task.FromResult(id);
        }

        public async Task<bool> DeleteLibrary(int libraryId)
        {
            bool isDelLibrary = false;
            LibraryRequest library = new LibraryRequest
            {
                LibraryId = libraryId,
            };

            // Validaciones si ya existe la biblioteca
            LibraryResponse findLibrary = new LibraryResponse();
            bool existsLibrary;
            (findLibrary, existsLibrary) = await GetLibrary(library);

            if (existsLibrary)
            {
                // Si existe la biblioteca se elimina
                LibrariesList.Remove(findLibrary);
                isDelLibrary = true;
            }

            return await Task.FromResult(isDelLibrary);
        }

        public async Task<bool> UpdateLibrary(int libraryId,ModLibraryRequest modLibrary)
        {
            bool isModLibrary = false;
            LibraryRequest library = new LibraryRequest
            {
                LibraryId = libraryId,
            };

            // Validaciones si ya existe la biblioteca
            LibraryResponse findLibrary = new LibraryResponse();
            bool existsLibrary;
            (findLibrary, existsLibrary) = await GetLibrary(library);

            if (existsLibrary)
            {
                findLibrary.LibraryName = modLibrary.LibraryName;
                findLibrary.Address = modLibrary.Address;
                findLibrary.City = modLibrary.City;

                isModLibrary = true;
            }
            return isModLibrary;
        }
    }
}
