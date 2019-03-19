namespace Terkwaz.Domain.Blog
{
    using System;
    using User;

    public class Blog
    {
        public long Id { get; private set; }

        public long UserId { get; private set; }

        public virtual User User { get; private set; }

        public string Title { get; private set; }

        public string Subtitle { get; private set; }

        public string ImageUrl { get; private set; }

        public string Body { get; private set; }

        public DateTime CreationDate { get; private set; }

        public static Blog New(User user, string title, string subtitle, string imageUrl, string body)
        {
            return new Blog
            {
                User = user,
                Title = title,
                Subtitle = subtitle,
                ImageUrl = imageUrl,
                Body = body,
                CreationDate = DateTime.UtcNow
            };
        }
    }
}
