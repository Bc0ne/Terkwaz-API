namespace Terkwaz.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using Terkwaz.Data.Config;
    using Terkwaz.Domain.Blog;
    using Terkwaz.Domain.User;

    public class TerkwazDbContext : DbContext
    {
        public TerkwazDbContext(DbContextOptions<TerkwazDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new BlogConfig());
        }
    }
}
