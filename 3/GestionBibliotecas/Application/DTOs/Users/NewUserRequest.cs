namespace GestionBibliotecas.Application.DTOs.Users
{
    public class NewUserRequest
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Teléfono del usuario.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Identifiacdor de la biblioteca.
        /// </summary>
        public int LibraryId { get; set; }

    }
}
