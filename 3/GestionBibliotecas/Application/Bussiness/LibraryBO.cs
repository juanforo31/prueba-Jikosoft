using GestionBibliotecas.Application.DTOs.Library;
using GestionBibliotecas.Domain.Interfaces;
using GestionBibliotecas.Infrastructure.MemoryData;

namespace GestionBibliotecas.Application.Bussiness
{
    /// <summary>
    /// Esta clase tiene toda la lógica de negocio para las bibliotecas.
    /// </summary>
    public class LibraryBO : ILibraries
    {
        private InMemoryDataStore _store;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store"></param>
        public LibraryBO(InMemoryDataStore store)
        {
            _store = store;

            // Se crea una biblioteca por defecto para pruebas
            LibraryResponse newLibrary = new LibraryResponse();
            newLibrary.LibraryId = 1;
            newLibrary.LibraryName = "Uno";
            newLibrary.Address = "";
            newLibrary.City = "";

            _store.Libreries.Add(newLibrary);
        }
        /// <summary>
        /// Obtiene todas las bibliotecas de la lista de bibliotecas.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LibraryResponse>> GetAllLibrerires()
        {
            // Retorna la lista de bibliotecas
            return await Task.FromResult(_store.Libreries);
        }
        /// <summary>
        /// Obtiene una biblioteca por su identificador, nombre o dirección.
        /// </summary>
        /// <param name="libraryRequest"></param>
        /// <returns></returns>
        public async Task<(LibraryResponse?, bool)> GetLibrary(LibraryRequest libraryRequest)
        {
            // Busca una biblioteca en la lista de bibliotecas por su identificador, nombre o dirección
            LibraryResponse? library = _store.Libreries.FirstOrDefault(lib =>
            lib.LibraryId == libraryRequest.LibraryId
            || lib.LibraryName == libraryRequest.LibraryName
            || lib.Address == libraryRequest.Address
            );

            bool existLibrary = library != null;

            return await Task.FromResult((library, existLibrary));
        }
        /// <summary>
        /// Añade una nueva biblioteca a la lista de bibliotecas.
        /// </summary>
        /// <param name="newlibraryRequest"></param>
        /// <returns></returns>
        public async Task<int> AddLibrary(NewLibraryRequest newlibraryRequest)
        {
            // Como identificador se esta usando un número random
            Random random = new Random();
            int libraryId = random.Next();

            LibraryRequest library = new LibraryRequest
            {
                LibraryId = libraryId,
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

                    LibraryId = libraryId,
                    LibraryName = newlibraryRequest.LibraryName,
                    Address = newlibraryRequest.Address,
                    City = newlibraryRequest.City,

                };

                _store.Libreries.Add(newLibrary);


            }
            else
            {
                // Si ya existe la biblioteca, se retorna 0
                libraryId = 0;
            }
            return await Task.FromResult(libraryId);
        }
        /// <summary>
        /// Elimina una biblioteca de la lista de bibliotecas por su identificador.
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLibrary(int libraryId)
        {
            // Se busca la biblioteca por su identificador
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
                _store.Libreries.Remove(findLibrary);
                isDelLibrary = true;
            }

            return await Task.FromResult(isDelLibrary);
        }
        /// <summary>
        /// Actualiza la información de una biblioteca existente por su identificador.
        /// </summary>
        /// <param name="libraryId"></param>
        /// <param name="modLibrary"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLibrary(int libraryId, ModLibraryRequest modLibrary)
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
                // Si existe la biblioteca se actualiza
                findLibrary.LibraryName = modLibrary.LibraryName;
                findLibrary.Address = modLibrary.Address;
                findLibrary.City = modLibrary.City;

                isModLibrary = true;
            }

            return isModLibrary;
        }
    }
}
