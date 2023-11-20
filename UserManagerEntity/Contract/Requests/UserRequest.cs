using System.ComponentModel.DataAnnotations;

namespace UserManagerEntity.Contract.Requests
{
    public class UserRequest
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? FullName { get; set; }

        /// <summary>
        /// Password of the user
        ///
        ///     Password must meet the following requirements:
        ///         - minimum length: 6
        ///         - includes at least one digit [0-9]
        ///         - includes at least one uppercase character
        ///         - includes at least one lowercase character
        ///         - includes at least one non-alphanumeric character
        /// </summary>
        /// <example>Sup3rSt0ngP@$$w0rd</example>
        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

    }
}
