namespace GestionBibliotecas.Application.DTOs.Books
{
    /// <summary>
    /// Representa una solicitud para buscar un libro en la biblioteca.
    /// </summary>
    public class BookRequest
    {
        /// <summary>
        /// Identifiacdor del libro.
        /// </summary>
        public int? BookId { get; set; }
        
        /// <summary>
        /// Título del libro
        /// </summary>
        public string? Title { get; set; }
        
        /// <summary>
        /// Autor del libro.
        /// </summary>
        public string? Author { get; set; }
        
        /// <summary>
        /// ISBN del libro.
        /// </summary>
        public string? ISBN { get; set; }
        
        /// <summary>
        /// Año de publicación.
        /// </summary>
        public int? PublishedYear { get; set; }
        
        /// <summary>
        /// Identificador de la biblioteca donde está.
        /// </summary>
        public int? LibraryId { get; set; }

        /// <summary>
        /// Identificador de la persona que tiene el libro.
        /// </summary>
        public int? UserId { get; set; }
    }
}
