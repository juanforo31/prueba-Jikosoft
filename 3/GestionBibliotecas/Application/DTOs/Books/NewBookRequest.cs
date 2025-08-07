namespace GestionBibliotecas.Application.DTOs.Books
{
    /// <summary>
    /// Representa una solicitud para agregar un nuevo libro a la biblioteca.
    /// </summary>
    public class NewBookRequest
    {
        /// <summary>
        /// Título del libro
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Autor del libro.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// ISBN del libro.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Año de publicación.
        /// </summary>
        public int PublishedYear { get; set; }

        /// <summary>
        /// Indeitificador de la libreria
        /// </summary>
        public int libraryId { get; set; }
    }
}
