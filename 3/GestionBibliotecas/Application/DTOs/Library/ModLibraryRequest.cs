namespace GestionBibliotecas.Application.DTOs.Library
{
    /// <summary>
    /// Clase que representa una solicitud para modificar una biblioteca existente.
    /// </summary>
    public class ModLibraryRequest
    {
        /// <summary>
        /// Identifiacdor de la biblioteca.
        /// </summary>
        public int? LibraryId { get; }

        /// <summary>
        /// Nombre de la biblioteca
        /// </summary>
        public string? LibraryName { get; set; }

        /// <summary>
        /// Dirección de la biblioteca
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra la biblioteca.
        /// </summary>
        public string? City { get; set; }
    }
}
