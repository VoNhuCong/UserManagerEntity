using UserManagerEntity.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// https://hoanguyenit.com/database-relationship-in-aspnet-core21.html

namespace UserManagerEntity.Models
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        #region DbSet
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Blog>()
                .HasOne(s=>s.User)
                .WithMany(s=>s.Blogs)
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
