using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserManagerEntity.Models.Entities;

namespace UserManagerEntity.Contract.Requests
{
    public class BlogRequest
    {
        [Required]
        [MaxLength(255)]
        public string? BlogName { get; set; }

        [Required]
        public string? BlogDescription { get; set; }

        [Required]
        public string? Id { get; set; }

    }
}
