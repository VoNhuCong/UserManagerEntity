using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace UserManagerEntity.Models.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }

    }
}
