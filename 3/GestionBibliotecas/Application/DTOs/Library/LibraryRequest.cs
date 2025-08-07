namespace GestionBibliotecas.Application.DTOs.Library
{
    /// <summary>
    /// Clase que representa una solicitud de biblioteca.
    /// </summary>
    public class LibraryRequest
    {
        /// <summary>
        /// Identifiacdor de la biblioteca.
        /// </summary>
        public int? LibraryId { get; set; }

        /// <summary>
        /// Nombre de la biblioteca
        /// </summary>
        public string? LibraryName { get; set; }

        /// <summary>
        /// Dirección de la biblioteca
        /// </summary>
        public string? Address { get; set; }

    }
}
