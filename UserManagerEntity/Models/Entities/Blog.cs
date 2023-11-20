using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserManagerEntity.Models.Entities
{
    [Table("Blog")]
    public class Blog
    {
        
        [Key]
        public Guid BlogID { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string? BlogName { get; set;}
        
        [Required]
        public string? BlogDescription { get; set;}
        
        /// <summary>
        /// Đánh giá bài viết 
        /// </summary>
        [Range(0, 5)]
        public int? BlogStar { get; set; }

        [Required]
        public string? Id { get; set;}

        [ForeignKey("Id")]
        [JsonIgnore]
        public User? User { get; set; }
    }
}
