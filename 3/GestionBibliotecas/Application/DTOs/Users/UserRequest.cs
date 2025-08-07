namespace GestionBibliotecas.Application.DTOs.Books
{
    /// <summary>
    /// Representa una solicitud de usuario para buscar en el sistema de gestión de bibliotecas.
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Teléfono del usuario.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Identifiacdor de la biblioteca.
        /// </summary>
        public int LibraryId { get; set; }

        /// <summary>
        /// Identifiacdor del libro.
        /// </summary>
        public int BookId { get; set; }
    }
}
