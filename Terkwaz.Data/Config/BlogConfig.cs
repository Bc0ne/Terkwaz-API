namespace Terkwaz.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Terkwaz.Domain.Blog;

    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Title)
                .IsRequired();

            builder
                .Property(x => x.Subtitle)
                .IsRequired();

            builder
                .Property(x => x.ImageUrl)
                .IsRequired();

            builder
                .Property(x => x.Body)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(t => t.Blogs)
                .HasForeignKey(x => x.UserId);
        }
    }
}
